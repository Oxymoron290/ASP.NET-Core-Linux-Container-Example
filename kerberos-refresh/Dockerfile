FROM alpine

RUN apk -qq update && apk -yqq add krb5 && mkdir /krb5 && chmod 755 /krb5

COPY ["./kerberos-refresh.sh", "/"]

# configure the exported volumes
 # /krb5 - default keytab location
 # /dev/shm - shared memory location used to write token cache
 # /etc/krb5.conf.d - directory for additional kerberos configuration
 VOLUME ["/krb5", "/dev/shm", "/etc/krb5.conf.d"]

 ENTRYPOINT ["sh", "./kerberos-refresh.sh"]