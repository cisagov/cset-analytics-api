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
        Task<List<Series>> GetAverages(string assessment_id);
        Task<List<Assessment>> GetUserAssessments(string userId);
    }
}
