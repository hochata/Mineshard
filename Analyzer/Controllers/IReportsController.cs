using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mineshard.Analyzer.Controllers
{
    public interface IReportsController
    {
        Task RunAnalysisAync(Guid id);
        void RunAnalysis(Guid id);
    }
}
