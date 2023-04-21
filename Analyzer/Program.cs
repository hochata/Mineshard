// Workaroud for https://github.com/dotnet/roslyn-analyzers/issues/6141
#pragma warning disable CA1852

using Mineshard.Analyzer;

using Mineshard.Analyzer.Core;

using (var analysis = new Analysis("https://github.com/dotnet/roslyn-analyzers"))
{
    Console.WriteLine(analysis.Status);
    Console.WriteLine(analysis.NumCommits);
    foreach (var b in analysis.Branches)
    {
        Console.WriteLine(b);
    }
    foreach (var c in analysis.CommitsPerMonth)
    {
        Console.WriteLine($"{c.Code}: {c.NumCommits}");
    }
}

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
