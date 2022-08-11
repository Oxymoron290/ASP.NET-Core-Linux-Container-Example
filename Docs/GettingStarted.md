# Getting Started

## Creating a KeyTab file

1. Create a temporary utility virutal machine Running Ubuntu that has access to the Domain Controller.
1. Install the proper packages on the linux machine
    - `apt-get update && apt-get install -y krb5 azure-cli` 
1. Configure `krb5.conf`
1. Test AD Ports to DC
1. Use kutil utility to add user principal encrypted keys to save keytab to the file
1. Verify and use Keytab file to initialize the token
1. Upload KeyTab as Secret to Azure KeyVault
    - `az login`
    - `az keyvault secret set --name dbuserkt --vault-name <vault-name> --file dbuser.keytab --encoding hex`

