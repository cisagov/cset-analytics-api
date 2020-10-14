using System;
using System.Collections.Generic;
using System.Text;

namespace CsetAnalytics.ViewModels.Dashboard
{
    public class DashboardGraphData
    {
        public List<ScatterPlot> Min { get; set; }
        public List<MedianScatterPlot> Median { get; set; }
        public List<ScatterPlot> Max { get; set; }
        public BarChart BarData { get; set; }
        public int sampleSize { get; set; }
    }
}
