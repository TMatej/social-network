# Social network

## Overview
A semestral project for PV179 course at FI MUNI. The application is divided in two parts: [.Net Core Api server](https://github.com/TMatej/social-network/tree/main/server) and [React client](https://github.com/TMatej/social-network/tree/main/client).

## Deployment
Server part is deployed on kubernetes using cerit [rancher](https://docs.cerit.io/docs/rancher.html).
Client part of the app is available through [vercel](https://vercel.com/). Below you can find links that will allow you to interact with the deployed app.

- [api server link](https://social-network-application-server.dyn.cloud.e-infra.cz/) - build from image [social-network-server-mssql](https://github.com/TMatej/social-network/pkgs/container/social-network-server-mssql)
- [react client link](https://social-network-lilac.vercel.app/) - build from image [social-network-client](https://github.com/TMatej/social-network/pkgs/container/social-network-client) 


## Login name and credentials 
These are predefined user entities that have User and Admin rights.  

### **Basic user**
```
{
  username: "user",
  email: "user@gmail.com",
  password: "user"
}
```

### **Admin**
```
{
  username: "admin",
  email: "admin@gmail.com",
  password: "admin"
}
```
## GitHub workflows
Stored in [https://github.com/TMatej/social-network/tree/main/.github/workflows](https://github.com/TMatej/social-network/tree/main/.github/workflows)

### [CI - server - mssql](https://github.com/TMatej/social-network/blob/main/.github/workflows/server_ci_mssql.yml) 
Runs after each push or pull request on branch _main_.  
#### Jobs:
- build
- test

### [CI - server - mssql - build & push image](https://github.com/TMatej/social-network/blob/main/.github/workflows/server_image_mssql.yml) 
Runs after each manual request on branch _main_.
Builds image of the server side of the app and exposes it in package registry.  
#### Jobs:
- build-image

### [CI - client](https://github.com/TMatej/social-network/blob/main/.github/workflows/client_ci.yml) 
Runs after each push or pull request on branch _main_.  
#### Jobs:
- build
- test

### [CI - client - build & push image](https://github.com/TMatej/social-network/blob/main/.github/workflows/client_image.yml) 
Runs after each manual request on branch _main_. Builds image of the client side of the app and exposes it in package registry.  
#### Jobs:
- build-image

## Application requirements

### Use cases
- USER & ADMIN & GROUP CEO
  - authentication
  - role
- profile (public information)
  - CRUD
  - friends management
  - adding post to profiles
  - controlling visibility of profile status (private/friends only/public)
- gallery (banch of photos)
  - CRUD
  - uploading pictures to profile-gallery
  - adding comments to photos
- post (only text)
  - CRUD (by owner/D by admin)
  - adding comments to posts
- sending personal messages to other users
- groups
  - CRUD 
  - group posts
- events
  - CRUD 
  - sign up for a event
- user search/filter (diff parameters such as name/age/sex/common friends)
- private chat
- optional - group chats

### Conceptual ERD diagram
![Conceptual erd - Chans notation](diagrams/social_network_diagrams-ERD_conceptual.jpg)

### Physical ERD diagram
![Physical erd - Crows notation](diagrams/social_network_diagrams-ERD_physical.jpg)

## Useful code snippets
To export SQL scheme from model:  
- Be sure to have included package  *Microsoft.EntityFrameworkCore.Design* in your DAL. If not go onto the DAL root and run:
  - `dotnet add package Microsoft.EntityFrameworkCore.Design` 
  - `dotnet restore`  
- Test dotnet ef installation
  - `dotnet ef` - if you dont see a unicorn, something is wrong and you need to install dotnet ef :).
- And apply 
  - `dotnet ef migrations script --output "script.sql" --context <MY_DBCONTEXT>`, where `<MY_DBCONTEXT>` is your custom DBContext.cs

## Team members (UCO, name, email):
- 484946, Michal Cizek, -
- 485522, Daniel Pivonka, -
- 469107, Matej Turek, 469107@mail.muni.cz
