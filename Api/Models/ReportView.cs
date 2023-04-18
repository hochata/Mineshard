﻿namespace Mineshard.Api.Models;

public class ReportView
{
    public required Guid Id { get; set; }
    public required string RepositoryName { get; set; }
    public required string UserName { get; set; }
    public required string Url { get; set; }
    public int NumCommitsOnMain { get; set; }
    public required List<string> Branches { get; set; }
    public required Dictionary<string, int> Committers { get; set; }
    public required Dictionary<string, int> CommitsPerMonth { get; set; }
}