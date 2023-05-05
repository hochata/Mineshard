// Workaroud for https://github.com/dotnet/roslyn-analyzers/issues/6141
#pragma warning disable CA1852

using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Mineshard.Analyzer.Broker;
using Mineshard.Analyzer.Controllers;
using Mineshard.Analyzer.Core;
using Mineshard.Persistence.Repos;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var cwd = Directory.GetParent(Directory.GetCurrentDirectory());

IConfigurationBuilder builder = new ConfigurationBuilder()
    .SetBasePath(cwd == null ? Directory.GetCurrentDirectory() : cwd.FullName)
    .AddJsonFile("appsettings.json");

var configuration = builder.Build();

if (configuration == null)
    throw new InvalidDataException("Couldn't find configuration file");

var cancellationTokenSource = new CancellationTokenSource();

var userInputTask = Task.Run(() =>
{
    while (true)
    {
        if (
            Console.ReadKey(true).Modifiers == ConsoleModifiers.Shift
            && Console.ReadKey(true).Key == ConsoleKey.C
        )
        {
            cancellationTokenSource.Cancel();
            break;
        }
    }
});

var workers = new List<Worker>();

var reporter = new ReportsController(
    new ReportsDbRepo(),
    new Reporter((IConfiguration)configuration)
);

var hostName =
    configuration.GetValue<string>("RabbitMQ:HostName")
    ?? throw new InvalidDataException("Missing HostName configuration");
var queueName =
    configuration.GetValue<string>("RabbitMQ:QueueName")
    ?? throw new InvalidDataException("Missing QueueName configuration");

// Create 10 threads to listen messages
for (int i = 1; i <= 10; i++)
{
    var factory = new ConnectionFactory() { HostName = hostName };
    var connection = factory.CreateConnection();
    var worker = new Worker(queueName, reporter, connection);
    workers.Add(worker);
}

foreach (var worker in workers)
{
    worker.StartConsuming();
}

Console.WriteLine("Listening inconming messages...");
Console.WriteLine("Press Shift + C to exit...");

// Wait for cancellation
Task.WaitAny(userInputTask, Task.Delay(Timeout.Infinite, cancellationTokenSource.Token));
