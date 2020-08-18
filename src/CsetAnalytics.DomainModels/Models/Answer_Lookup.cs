using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;


namespace CsetAnalytics.DomainModels.Models
{
    public partial class Answer_Lookup
    {
        
        public Answer_Lookup() {}

        [BsonId]
        public string Answer_Text { get; set; }

        [Required]
        public string Answer_Full_Name { get; set; }

    }
}
