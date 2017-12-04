using Dunfoss.Models;
using System.Linq;


namespace Dunfoss.Data
{
    public interface IReportRepository
    {
        IQueryable<Report> Reports { get; }
        Report GetReportById(int id);
        Report CreateReport(Report report);
    }
}
