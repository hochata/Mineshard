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


### Configuring the Application

To configure the application, you need to update the appsettings.json file in your dotnet project. This file contains three sections: GitUser, ConnectionStrings, and RabbitMQ.

#### GitUser Section

The GitUser section contains information about the Git user that will be used by the application. To configure this section, you need to update the following fields:

- `Name`: Enter the name of the Git user.
- `Email`: Enter the email address of the Git user.

#### ConnectionStrings Section

The `ConnectionStrings` section contains information about the connection to the Mineshard database. To configure this section, you need to update the MineshardDb field with the connection string for your PostgreSQL database.

#### RabbitMQ Section

The RabbitMQ section contains information about the RabbitMQ message queue used by the application. To configure this section, you need to update the following fields:

- `HostName`: Enter the hostname of the RabbitMQ server.
- `RoutingKey`: Enter the routing key used by the application to send messages to the queue.
- `QueueName`: Enter the name of the queue used by the application to receive messages.
- `ExchangeName`: Enter the name of the exchange used by the application to send messages to the queue.

Once you have updated the appsettings.json file, you can run the application and it will use the new configuration values.

### Analyzer

```sh
dotnet run --project Api/Mineshard.Analyzer.csproj
```

### Api

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
