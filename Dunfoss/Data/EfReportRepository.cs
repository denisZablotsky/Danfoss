using System.Linq;
using Dunfoss.Models;

namespace Dunfoss.Data
{
    public class EfReportRepository : IReportRepository
    {
        EfDbContext Context;
        public EfReportRepository()
        {
            Context = new EfDbContext();
        }
        public IQueryable<Report> Reports
        {
            get
            {
                return Context.Reports;
            }
        }

        public Report CreateReport(Report report)
        {
            Report newReport = Context.Reports.Add(report);
            Context.SaveChanges();
            return newReport;
        }

        public Report GetReportById(int id)
        {
            Report report = Context.Reports.Find(id);
            return report;
        }
    }
}