using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CsetAnalytics.DomainModels.Models
{
    [Table("Configuration", Schema = "public")]
    public class Configuration
    {
        public Configuration()
        { }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int ConfigurationId { get; set; }
        public string ConfigurationKey { get; set; }
        public string ConfigurationValue { get; set; }
}
}
