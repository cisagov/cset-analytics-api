using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CsetAnalytics.DomainModels.Models;
using CsetAnalytics.ViewModels;

namespace CsetAnalytics.Interfaces.Analytics
{
    public interface IAnalyticBusiness
    {
        Task SaveAnalyticQuestions(List<AnalyticQuestionAnswer> questions);
        Task<Assessment> SaveAssessment(Assessment assessment);
    }
}
