﻿using Mineshard.Persistence.Models;

namespace Mineshard.Persistence.Repos;

public interface IReportsRepo
{
    List<Report> GetAll();
    Report? GetOne(Guid id);
    void Update(Report source, Report dest);
}
