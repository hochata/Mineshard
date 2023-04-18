# Mineshard

## Setup
Install tools with
```sh
dotnet tool restore
```

Modify the `appsettings.json` at the root. The field `MineshardDb` should have the correct database connection string. Then to create the database run
```sh
cd Persistence
dotnet ef database update
cd ..
```

Check that the project builds with
```sh
dotnet build
```

## Run
```sh
dotnet run --project Api/Mineshard.Api.csproj
```

## Code quality checks

To check if the code is formatted run
```sh
dotnet csharpier --check
```

And to run the static analyzers run
```sh
dotnet format --verify-no-changes
```

Check code coverage with
```sh
dotnet test -p:CollectCoverage=true -p:Threshold=80
```
