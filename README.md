# Example of ASP.NET Core running on a Linux Container with SQL Server backend

## Overview

This repository serves as an example and proof of concept for an ASP.NET Core v5.0+ WebAPI running on a Linux Container and connecting to an external SQL Server using windows authentication. The current state of the application does not demonstrate windows authentication passthrough and is developed with the assumption that the SQL Server connection will be established using static Windows Credentials, such as a service account.

## TODO

- [x] Create project from template
- [x] Create sql database structure using entity framework
- [x] Setup Entity Framework Migrations
- [x] Create Initial Migration seed.
- [x] Create CRUD RESTful endpoints using ASP.NET core
- [x] Create dockerfile
- [ ] Create example configuration

## License

See [License](./LICENSE).
