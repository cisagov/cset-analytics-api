using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using CsetAnalytics.DomainModels.Models;
using CsetAnalytics.ViewModels.Dashboard;

namespace CsetAnalytics.Interfaces.Dashboard
{
    public interface IDashboardBusiness
    {
        Task<List<Assessment>> GetUserAssessments(string userId);
        Task<List<Sector>> GetSectors();
        Task<DashboardGraphData> GetDashboardData(string industry, string assessmentId);
    }
}
