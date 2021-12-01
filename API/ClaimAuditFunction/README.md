### Project

Created to Audit all changes to Claims and storing the data in Azure SQL Database.

### Run initial migration

```
    dotnet ef migrations add InitialCreate
```

### Create database

To create a local database for development.

```
    dotnet ef database update
```

### Clustered Indexing

To turn on Clustered Indexing set the value to "true"

```
    builder.Entity<ClaimAudit>(entity =>
    {
        entity.HasIndex(e => e.ClaimId).IsClustered(false);
    });
```
