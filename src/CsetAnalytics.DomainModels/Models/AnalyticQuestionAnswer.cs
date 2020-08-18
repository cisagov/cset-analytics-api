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
        public string Assessment_Id { get; set; }

        public int Question_Or_Requirement_Id { get; set; }

        [Required]
        public string Answer_Text { get; set; }

        [Required]
        public string QuestionText { get; set; }

        public Guid? Component_Guid { get; set; }

        public string Custom_Question_Guid { get; set; }

        public bool Is_Requirement { get; set; }

        public bool Is_Component { get; set; }

        public bool Is_Framework { get; set; }
    }
}
