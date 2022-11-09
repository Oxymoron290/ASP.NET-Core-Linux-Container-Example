# Example of ASP.NET Core running on a Linux Container with SQL Server backend

## Overview

This repository serves as an example and proof of concept for an ASP.NET Core v5.0+ WebAPI running on a Linux Container and connecting to an external SQL Server using windows authentication. The current state of the application does not demonstrate windows authentication passthrough and is developed with the assumption that the SQL Server connection will be established using static Windows Credentials, such as a service account.

The majority of the information in this repository was pulled from the two following articles and their respective repositories:

- [Connect Azure Kubernetes Java Applications to SQL with Kerberos Integrated Authentication](https://medium.com/microsoftazure/connect-azure-kubernetes-java-applications-to-sql-with-kerberos-integrated-authentication-88dfa3fa382c)
    - https://github.com/lenisha/jdbc-kerberos
- [Kerberos Sidecar Container](https://cloud.redhat.com/blog/kerberos-sidecar-container)
    - https://github.com/edseymour/kinit-sidecar

## Building a pushing docker images

```
az login
az acr login --name contosoacrpoc
cd ./kerberos-refresh
docker build -t kerberos-sidecar .
docker tag kerberos-sidecar contosoacrpoc.azurecr.io/coho-winery/kerberos-sidecar
docker push contosoacrpoc.azurecr.io/coho-winery/kerberos-sidecar
cd ../CohoWineryAPI
dotnet build
docker build -t coho-test .
docker tag coho-test contosoacrpoc.azurecr.io/coho-winery/coho-test
docker push contosoacrpoc.azurecr.io/coho-winery/coho-test
```

## License

See [License](./LICENSE).
