# Migrations

To add a new migration, open the console and issue the following command:
```
dotnet ef migrations add <migration_name> --project NewsAPI.DataContext
```

To apply the migration to the database, issue the following command:
```
dotnet ef database update
```