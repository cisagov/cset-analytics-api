using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace CsetAnalytics.DomainModels.Models
{


    [Table("Sector")]
    public partial class Sector
    {

        public Sector()
        {
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SectorId { get; set; }

        [Required]
        public string SectorName { get; set; }

        public List<Industry> Industries { get; set; }

    }

    public partial class Industry
    {

        public Industry()
        {
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int IndustryId { get; set; }

        [Required]
        [StringLength(150)]
        public string IndustryName { get; set; }
    }
}
