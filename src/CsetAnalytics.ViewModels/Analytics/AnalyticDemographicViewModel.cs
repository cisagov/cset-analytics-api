using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsetAnalytics.ViewModels
{
    public class AnalyticDemographicViewModel
    {
        public string IndustryName { get; set; }
        public int IndustryId { get; set; }
        public string SectorName { get; set; }
        public int SectorId { get; set; }
        public string Size { get; set; }
        public string AssetValue { get; set; }
        public string Alias { get; set; }
    }
}
