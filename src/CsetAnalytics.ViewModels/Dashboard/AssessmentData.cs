using CsetAnalytics.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsetAnalytics.ViewModels.Dashboard
{
    public class AssessmentData
    {
        public List<Assessment> Items { get; set; }
        public int Total_count { get; set; }
    }
}
