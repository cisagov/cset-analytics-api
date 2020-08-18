using System.Collections.Generic;

namespace CsetAnalytics.ViewModels.Dashboard
{
    public class DashboardChartData
    {
        public string name { get; set; }
        public List<Series> series { get; set; }
    }

    public class Series
    {
        public string name { get; set; }
        public double value { get; set; }
    }
}