using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CsetAnalytics.DomainModels.Models
{

    public class AnalyticQuestionAnswer
    {
        public AnalyticQuestionAnswer() { }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AnalyticQuestionId { get; set; }

        [Required]
        public string AssessmentId { get; set; }

        public int QuestionId { get; set; }

        [Required]
        public string AnswerText { get; set; }

        [Required]
        public string QuestionText { get; set; }

        public Guid? ComponentGuid { get; set; }

        public string CustomQuestionGuid { get; set; }

        public bool IsRequirement { get; set; }

        public bool IsComponent { get; set; }

        public bool Is_Framework { get; set; }
        public int CategoryId { get; set; }
        public string CategoryText { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryText { get; set; }
        public string SetName { get; set; }
    }
}
