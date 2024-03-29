#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CohoWineryAPI.csproj", "."]
RUN dotnet restore "./CohoWineryAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "CohoWineryAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CohoWineryAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN apk -qq update && apk -yqq add krb5 icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# configure the exported volumes
 # /krb5 - default keytab location
 # /dev/shm - shared memory location used to write token cache
 # /etc/krb5.conf.d - directory for additional kerberos configuration
 VOLUME ["/krb5", "/dev/shm", "/etc/krb5.conf.d"]

ENTRYPOINT ["dotnet", "CohoWineryAPI.dll"]