# Database migration

Manual to migrate database:
https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli

dotnet ef migrations list --project .\databasemigrations\databasemigrations.csproj
dotnet ef migrations add vxx --project .\databasemigrations\databasemigrations.csproj
dotnet ef database update --project .\databasemigrations\databasemigrations.csproj
dotnet ef migrations script vxx --project .\databasemigrations\databasemigrations.csproj --output script.sql