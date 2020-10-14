﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CsetAnalytics.DomainModels.Models
{
    public class CategoryStatistics
    {
        public string AssessmentId { get; set; }
        public string CategoryName { get; set; }
        public int AnsweredYes { get; set; }
        public double NormalizedYes { get; set; }
        public int Total { get; set; }
    }
}
