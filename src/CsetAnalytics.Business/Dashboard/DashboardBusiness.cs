using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsetAnalytics.DomainModels;
using CsetAnalytics.DomainModels.Models;
using CsetAnalytics.Interfaces.Dashboard;
using CsetAnalytics.ViewModels.Dashboard;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MongoDB.Driver;
using MoreLinq;

namespace CsetAnalytics.Business.Dashboard
{
    public class DashboardBusiness : IDashboardBusiness
    {

        private const string IndustryAverageName = "Industry Average";
        private const string SectorAverageName = "Sector Average";
        private const string MyAssesmentAverageName = "Assessment Average";

        private readonly CsetContext _context;

        public DashboardBusiness(MongoDbSettings settings)
        {
            _context = new CsetContext(settings);
        }

        private Series GetSectorAnalytics(int sector_id)
        {
            var assessments = (from a in _context.Assessments.AsQueryable()
                where a.SectorId == sector_id
                select a).ToList();
            var query = (from a in assessments.AsQueryable()
                join q in _context.Questions.AsQueryable() on a.Assessment_Id equals q.AssessmentId
                //where a.SectorId == sector_id
                select q).ToList();

            var tempQuery = (from q in query.AsQueryable()
                group new {q.AssessmentId, q.AnswerText} by new
                {
                    q.AssessmentId,
                    q.AnswerText
                }
                into g
                select new
                {
                    g.Key.AssessmentId,
                    g.Key.AnswerText,
                    Count = g.Count()
                }).ToList();

            //go through get the sum total of all
            //get the sum of yes and alts
            //then calc the percent of each assessment and
            //sector average.

            Dictionary<string, QuickSum> sums = new Dictionary<string, QuickSum>();
            foreach (var a in tempQuery)
            {

                QuickSum quickSum;
                if (sums.TryGetValue(a.AssessmentId, out quickSum))
                {
                    quickSum.TotalCount += a.Count;
                    if (a.AnswerText == "Y" || a.AnswerText == "A")
                    {
                        quickSum.YesAltCount += a.Count;
                    }
                }
                else
                {
                    int yaltCount = 0;
                    if (a.AnswerText == "Y" || a.AnswerText == "A")
                    {
                        yaltCount = a.Count;
                    }
                    sums.Add(a.AssessmentId, new QuickSum() { assesment_id = a.AssessmentId, TotalCount = a.Count, YesAltCount = yaltCount });

                }
            }

            //calculate the average percentage of all the assessments
            double average = 0;
            foreach (QuickSum s in sums.Values)
            {
                average += s.Percentage;
            }
            average = average / (sums.Values.Count() == 0 ? 1 : sums.Values.Count());

            return new Series() { name = SectorAverageName, value = average * 100 };
        }

        private Series GetMyAnalytics(string myAssessment_Id)
        {
            var assessments = (from a in _context.Assessments.AsQueryable()
                where a.Assessment_Id == myAssessment_Id
                select a).ToList();
            var questions = (from a in assessments.AsQueryable()
                join q in _context.Questions.AsQueryable() on a.Assessment_Id equals q.AssessmentId
                select q).ToList();
            var query = (from q in questions.AsQueryable()
                group new { q.AssessmentId, q.AnswerText } by new
                {
                    q.AssessmentId,
                    q.AnswerText
                }
                into g
                select new
                {
                    g.Key.AssessmentId,
                    g.Key.AnswerText,
                    Count = g.Count()
                }).ToList();

            //go through get the sum total of all
            //get the sum of yes and alts
            //then calc the percent of each assessment and
            //sector average.

            Dictionary<string, QuickSum> sums = new Dictionary<string, QuickSum>();
            foreach (var a in query.ToList())
            {
                QuickSum quickSum;
                if (sums.TryGetValue(a.AssessmentId, out quickSum))
                {
                    quickSum.TotalCount += a.Count;
                    if (a.AnswerText == "Y" || a.AnswerText == "A")
                    {
                        quickSum.YesAltCount += a.Count;
                    }
                }
                else
                {
                    int yaltCount = 0;
                    if (a.AnswerText == "Y" || a.AnswerText == "A")
                    {
                        yaltCount = a.Count;
                    }
                    sums.Add(a.AssessmentId, new QuickSum() { assesment_id = a.AssessmentId, TotalCount = a.Count, YesAltCount = yaltCount });

                }
            }

            //calculate the average percentage of all the assessments
            double average = 0;
            foreach (QuickSum s in sums.Values)
            {
                average += s.Percentage;
            }
            average = average / (sums.Values.Count() == 0 ? 1 : sums.Values.Count());

            return new Series() { name = MyAssesmentAverageName, value = average * 100 };
        }

        public async Task<List<Assessment>> GetUserAssessments(string userId)
        {
            var assessments = await _context.Assessments.Find(a => a.AssessmentCreatorId == userId).ToListAsync();
            return assessments;
        }

        public async Task<List<Sector>> GetSectors()
        {
            var sectors =  await _context.Sectors.Find(x => true).ToListAsync();
            return sectors;
        }

        public async Task<DashboardGraphData> GetDashboardData(string industry, string assessmentId)
        {
            var sectorIndustry = industry.Split('|');
            var graphData = new DashboardGraphData();
            var statistics = new List<CategoryStatistics>();
            var categoryList = new List<string>();
            var myQuestions = await _context.Questions.Find(x=>x.AssessmentId == assessmentId).ToListAsync();
            var questions = await _context.Questions.Find(x=>true).ToListAsync();
            if (sectorIndustry.Length > 1 && sectorIndustry[1] != "All Sectors")
                questions = questions.Where(x => (x.Sector == sectorIndustry[0] && x.Industry == sectorIndustry[1]) || x.AssessmentId==assessmentId).ToList();
            var assessments = from q in questions
                group q by q.AssessmentId 
                into assessmentGroup
                from categoryGroup in 
                    (from a in assessmentGroup
                        group a by a.CategoryText
                    )
                group categoryGroup by assessmentGroup.Key;
            graphData.sampleSize = assessments.Select(x => x.Key).Count();
            foreach (var assessment in assessments)
            {
                foreach (var category in assessment)
                {
                    //categoryList.Add(category.Key);
                    var questionList = category.ToList();
                    statistics.Add(new CategoryStatistics
                    {
                        AssessmentId = assessment.Key.ToString(), 
                        CategoryName = category.Key, 
                        AnsweredYes = questionList.Count(x => x.AnswerText == "Y"),
                        NormalizedYes = Math.Round(((double)questionList.Count(x => x.AnswerText == "Y")/questionList.Count())*100, 1),
                        Total = questionList.Count()
                    });
                }
            }
            categoryList = myQuestions.Select(x=>x.CategoryText).Distinct().ToList();
            categoryList.Sort();
            //categoryList = categoryList.Distinct().ToList();
            graphData.BarData = new BarChart { Values = new List<double>(), Labels = new List<string>()};
            graphData.Min = new List<ScatterPlot>();
            graphData.Max = new List<ScatterPlot>();
            graphData.Median = new List<MedianScatterPlot>();
            foreach (var c in categoryList)
            {
                var statByCat = statistics.Where(x => x.CategoryName == c).ToList();
                if (statByCat.Count() > 0)
                {
                    var min = statByCat.MinBy(x => x.NormalizedYes).Take(1).FirstOrDefault();
                    graphData.Min.Add(new ScatterPlot {x = min.NormalizedYes, y = c});
                    var max = statByCat.MaxBy(x => x.NormalizedYes).Take(1).FirstOrDefault();
                    graphData.Max.Add(new ScatterPlot {x = max.NormalizedYes, y = c});
                    graphData.Median.Add(new MedianScatterPlot
                        {x = Math.Round(GetMedian(statByCat.Select(x => x.NormalizedYes).ToList()),1), y = c});
                    var answeredYes = statByCat.FirstOrDefault(x => x.AssessmentId == assessmentId);
                    graphData.BarData.Values.Add(answeredYes.NormalizedYes);
                    graphData.BarData.Labels.Add(c);
                }
            }
            return graphData;
        }

        public double GetMedian(List<double> answers)
        {
            if (answers == null || answers.Count == 0)
                return 0;
            var sortedNumbers = answers;
            answers.Sort();

            int size = sortedNumbers.Count();
            int mid = size / 2;
            double median = (size % 2 != 0) ? (double) sortedNumbers[mid] : ((double)sortedNumbers[mid] + (double)sortedNumbers[mid - 1]) / 2;
            return median;
        }
    }

    internal class QuickSum
    {
        public string assesment_id { get; set; }
        public int TotalCount { get; set; }
        public int YesAltCount { get; set; }
        public double Percentage {
            get
            {
                return (double) YesAltCount / (double) TotalCount;
            }
        }
    }
}
