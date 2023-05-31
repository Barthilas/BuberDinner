# BuberDinner

Based on this playlist: https://www.youtube.com/watch?v=jnutb5Z4wyg&list=PLzYkqgWkHPKBcDIP5gzLfASkQyTdy0t4k&index=13

# Commands

```
dotnet new sln -o BuberDinner
dotnet new webapi -o BuberDinner.Api
dotnet new classlib -o BuberDinner.Contracts
...
dotnet sln add BuberDinner.Api\BuberDinner.Api.csproj
dotnet build

dotnet add .\BuberDinner.Api reference .\BuberDinner.Contracts .\BuberDinner.Application
...
dotnet run --project  BuberDinner.Api


dotnet user-secrets init
//add secret to appSettings section
dotnet user-secrets set "JwtSettings:Secret" "super-secret-key-from-user-secrets"
dotnet user-secrets list 
```
