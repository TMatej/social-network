# Social network
A semestral project for PV179 course at FI MUNI.

## Useful code snippets
To export SQL scheme from model:  
- Be sure to have included package  *Microsoft.EntityFrameworkCore.Design* in your DAL. If not go onto the DAL root and run:
  - `dotnet add package Microsoft.EntityFrameworkCore.Design` 
  - `dotnet restore`  
- Test dotnet ef installation
  - `dotnet ef` - if you dont see a unicorn, something is wrong and you need to install dotnet ef :).
- And apply 
  - `dotnet ef migrations script --output "script.sql" --context <MY_DBCONTEXT>`, where `<MY_DBCONTEXT>` is your custom DBContext.cs

## GitHub workflows
#### [CI - server](https://github.com/TMatej/social-network/blob/main/.github/workflows/server_ci.yml) 
- On each push or pull request on branch _main_ 

#### B  
#### C  

## USE CASES
- USER & ADMIN & GROUP CEO 
- - authentication
- - role
- profile (user/admin public information)
- - CRUD
- - friends management
- - adding post to profiles
- - controlling visibility of profile status (private/friends only/public)
- gallery (banch of photos)
- - CRUD
- - uploading pictures to profile-gallery
- - adding comments to photos
- post (only text)
- - CRUD (by owner/D by admin)
- - adding comments to posts
- sending personal messages to other users
- groups
- - CRUD 
- - group posts
- events
- - CRUD 
- - sign up for a event
- user search/filter (diff parameters such as name/age/sex/common friends)
- private chat
- group chats

## Conceptual ERD diagram
![Conceptual erd - Chans notation](diagrams/social_network_diagrams-ERD_conceptual.jpg)

## Physical ERD diagram
![Physical erd - Crows notation](diagrams/social_network_diagrams-ERD_physical.jpg)

## Team members (UCO, name, email):
- 484946, Michal Cizek, -
- 485522, Daniel Pivonka, -
- 469107, Matej Turek, 469107@mail.muni.cz
