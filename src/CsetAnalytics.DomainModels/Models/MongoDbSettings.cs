using System;
using System.Collections.Generic;
using System.Text;

namespace CsetAnalytics.DomainModels.Models
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string AssessmentCollectionName { get; set; }
        public string AnalyticQuestionAnswerCollectionName { get; set; }
        public string SectorIndustriesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMongoDbSettings
    {
        string AssessmentCollectionName { get; set; }
        string AnalyticQuestionAnswerCollectionName { get; set; }
        string SectorIndustriesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
