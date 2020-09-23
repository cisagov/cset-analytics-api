﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsetAnalytics.ViewModels
{
    public class AnalyticQuestionViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
        public int CategoryId { get; set; }
        public string CategoryText { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryText { get; set; }
        public string SetName { get; set; }
        public bool IsRequirement { get; set; }
        public bool IsComponent { get; set; }
    }
}
