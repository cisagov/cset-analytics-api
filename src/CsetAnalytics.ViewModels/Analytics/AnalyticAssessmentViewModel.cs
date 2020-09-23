using System;
using System.Collections.Generic;
using System.Text;

namespace CsetAnalytics.ViewModels
{
    public class AnalyticAssessmentViewModel
    {
        public DateTime AssessmentCreatedDate { get; set; }
        public string AssessmentCreatorId { get; set; }
        public DateTime? LastAccessedDate { get; set; }
        public string Alias { get; set; }
        public string Assessment_GUID { get; set; }
        public DateTime Assessment_Date { get; set; }
        public int SectorId { get; set; }
        public string SectorName { get; set; }
        public int IndustryId { get; set; }
        public string IndustryName { get; set; }
        public string Assets { get; set; }
        public string Size { get; set; }
    }
}
