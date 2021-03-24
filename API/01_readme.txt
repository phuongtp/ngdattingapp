How to start

API>dotnet watch run

2.
>dotnet ef migrations add IdentityAdded

>dotnet ef database update

3. Rebuild new database
   >dotnet ef database drop
   >yes
   >dotnet watch run

4. Call Seed.cs to generate new Users.   


5. [Authorize(Roles="Admin")]
