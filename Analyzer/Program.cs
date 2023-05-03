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

// Create 10 threads to listen messages
for (int i = 1; i <= 10; i++)
{
    Thread workerThread = new Thread(new ParameterizedThreadStart(Setup));
    workerThread.Start(configuration);
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();

static void Setup(object? config)
{
    if (config is null)
        return;
    var reporter = new ReportsController(new ReportsDbRepo(), new Reporter((IConfiguration)config));
    var worker = new Worker((IConfiguration)config, reporter);
    worker.StartConsuming();
}
