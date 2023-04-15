# Mineshard

Build with
```sh
dotnet build
```

## Code quality checks

Install tools with
```sh
dotnet tool restore
```

Then, to check if the code is formatted run
```sh
dotnet csharpier --check
```

And to run the static analyzers run
```sh
dotnet format --verify-no-changes
```
