using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CsetAnalytics.DomainModels;
using CsetAnalytics.DomainModels.Models;
using CsetAnalytics.Interfaces.Analytics;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;


namespace CsetAnalytics.Business.Analytics
{
    public class AnalyticsBusiness : IAnalyticBusiness
    {
        private readonly IMongoCollection<Assessment> _assessment;
        private readonly IMongoCollection<AnalyticQuestionAnswer> _questions;
        private readonly CsetContext _context;

        public AnalyticsBusiness(MongoDbSettings settings)
        {
            Console.WriteLine(settings.ConnectionString);
            var client = new MongoClient(settings.ConnectionString);

            _context = new CsetContext(settings);

        }

        public async Task SaveAnalyticQuestions(List<AnalyticQuestionAnswer> questions)
        {
            await _context.Questions.InsertManyAsync(questions);
        }

        public async Task<Assessment> SaveAssessment(Assessment assessment)
        {
            await _context.Assessments.InsertOneAsync(assessment);
            return assessment;
        }

    }
}
