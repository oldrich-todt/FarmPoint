[![Build Status](https://github.com/oldrich-todt/FarmPoint/workflows/Build/badge.svg)](https://github.com/oldrich-todt/FarmPoint/actions)

# FarmPoint
FarmPoint is a project dedicated to the exploration of clean architecture. The project uses a [clean architecture template](https://github.com/jasontaylordev/CleanArchitecture) created by [jasontaylordev](https://github.com/jasontaylordev).

## Migrations
To create migrations using `dotnet ef migrations` run this command in project's root folder.

```powershell
dotnet ef migrations add AddFarmEntity --project .\src\Infrastructure\ --startup-project .\src\WebUI\ --output-dir .\Persistence\Migrations\
```

## Development JWT token
When developing locally use `dotnet user-jwts` command to generate development JWT token.

```powershell
cd .\src\WebUI\
dotnet user-jwts -p .\WebUI.csproj create --scheme IdentityServerJwtBearer --name administrator@localhost
```