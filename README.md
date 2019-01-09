# IDS Admin

> An Administration suite for IdentityServer 4

## Project Status

Version: 0.0.1
Status: Alpha

This project has been forked from https://github.com/skoruba/IdentityServer4.Admin as I needed to make changes that would be detrimental to the main project. 
Please support the main project!

## Requirements
- [Install](https://www.microsoft.com/net/download/windows#/current) the latest .NET Core 2.x SDK (using older versions may lead to 502.5 errors when hosted on IIS or application exiting immediately after starting when self-hosted)

### How to configure Asp.Net Core Identity - database, primary key data type

- By default, it's used as the primary key `int`, but it's possible to change it:

- [Follow these steps to configure Identity](docs/Configure-To-Existing-Identity.md)

### Template uses following list of nuget packages

- [Available nuget packages](https://www.nuget.org/profiles/skoruba)

### Running in Visual Studio

- Set Startup projects:
  - Code4.IdentityServer4.Admin
  - Code4.IdentityServer4.STS.Identity

## Installation of the Client Libraries

```sh
cd src/Code4.IdentityServer4.Admin
npm install

cd src/Code4.IdentityServer4.STS.Identity
npm install
```

## Bundling and Minification

The following Gulp commands are available:

- `gulp fonts` - copy fonts to the `dist` folder
- `gulp styles` - minify CSS, compile SASS to CSS
- `gulp scripts` - bundle and minify JS
- `gulp clean` - remove the `dist` folder
- `gulp build` - run the `styles` and `scripts` tasks

## Authentication and Authorization

- Change the specific URLs and names for the IdentityServer and Authentication settings in `Constants/AuthenticationConsts` or `appsettings.json`
- `Constants/AuthorizationConsts.cs` contains configuration of constants connected with authorization - definition of the default name of admin policy
- In the controllers is used the policy which name is stored in - `AuthorizationConsts.AdministrationPolicy`. In the policy - `AuthorizationConsts.AdministrationPolicy` is defined required role stored in - `AuthorizationConsts.AdministrationRole`.
- With the default configuration, it is necessary to configure and run instance of IdentityServer4. It is possible to use initial migration for creating the client as it mentioned above

## Localizations - labels, messages

- All labels and messages are stored in the resources `.resx` - locatated in `/Resources`
  - Client label descriptions from - http://docs.identityserver.io/en/release/reference/client.html
  - Api Resource label descriptions from - http://docs.identityserver.io/en/release/reference/api_resource.html
  - Identity Resource label descriptions from - http://docs.identityserver.io/en/release/reference/identity_resource.html
  
## Tests

-  The solution contains unit and integration tests. 
- **Stage environment is used for integration tests**:
  - `DbContext` contains setup for InMemory database
  - `Authentication` is setup for `CookieAuthentication` - with fake login url only for testing purpose
  - `AuthenticatedTestRequestMiddleware` - middleware for testing of authentication.
  
- If you want to use `Stage environment` for deploying - it is necessary to change these settings in `StartupHelpers.cs`.

## Overview

### Solution structure:

- STS:

  - `Code4.IdentityServer4.STS.Identity` - [Quickstart UI for the IdentityServer4 with Asp.Net Core Identity and EF Core storage](https://github.com/IdentityServer/IdentityServer4.Samples/tree/release/Quickstarts/Combined_AspNetIdentity_and_EntityFrameworkStorage)

- Admin UI:

  - `Code4.IdentityServer4.Admin` - ASP.NET Core MVC application that contains Admin UI

  - `Code4.IdentityServer4.Admin.BusinessLogic` - project that contains Dtos, Repositories, Services and Mappers for the IdentityServer4

  - `Code4.IdentityServer4.Admin.BusinessLogic.Identity` - project that contains Dtos, Repositories, Services and Mappers for the Asp.Net Core Identity

  - `Code4.IdentityServer4.Admin.BusinessLogic.Shared` - project that contains shared Dtos and ExceptionHandling for the Business Logic layer of the IdentityServer4 and Asp.Net Core Identity

  - `Code4.IdentityServer4.Admin.EntityFramework` - EF Core data layer that contains Entities for the IdentityServer4

  - `Code4.IdentityServer4.Admin.EntityFramework.Identity` - EF Core data layer that contains Entities for the Asp.Net Core Identity

  - `Code4.IdentityServer4.Admin.EntityFramework.DbContexts` - project that contains AdminDbContext for the administration

- Tests:

  - `Code4.IdentityServer4.Admin.IntegrationTests` - xUnit project that contains the integration tests

  - `Code4.IdentityServer4.Admin.UnitTests` - xUnit project that contains the unit tests

**Clients**

It is possible to define the configuration according the client type - by default the client types are used:

- Empty
- Web Application - Server side - Implicit flow
- Web Application - Server side - Hybrid flow
- Single Page Application - Javascript - Implicit flow
- Native Application - Mobile/Desktop - Hybrid flow
- Machine/Robot - Resource Owner Password and Client Credentials flow

- Actions: Add, Update, Clone, Remove
- Entities:
  - Client Cors Origins
  - Client Grant Types
  - Client IdP Restrictions
  - Client Post Logout Redirect Uris
  - Client Properties
  - Client Redirect Uris
  - Client Scopes
  - Client Secrets

**API Resources**

- Actions: Add, Update, Remove
- Entities:
  - Api Claims
  - Api Scopes
  - Api Scope Claims
  - Api Secrets

**Identity Resources**

- Actions: Add, Update, Remove
- Entities:
  - Identity Claims

## Asp.Net Core Identity

**Users**

- Actions: Add, Update, Delete
- Entities:
  - User Roles
  - User Logins
  - User Claims

**Roles**

- Actions: Add, Update, Delete
- Entities:
  - Role Claims

## Plan & Vision

### 0.0.1:

- [x] Create the Business Logic & EF layers - available as a nuget package

## Licence

This repository is licensed under the terms of the [**MIT license**](LICENSE.md).

## Acknowledgements

This web application is based on these projects:

- ASP.NET Core
- IdentityServer4.EntityFramework
- ASP.NET Core Identity
- XUnit
- Fluent Assertions
- Bogus
- AutoMapper
- Serilog

Thanks to https://github.com/skoruba

Thanks to [Tomáš Hübelbauer](https://github.com/TomasHubelbauer) for the initial code review.

Thanks to [Dominick Baier](https://github.com/leastprivilege) and [Brock Allen](https://github.com/brockallen) - the creators of IdentityServer4.



