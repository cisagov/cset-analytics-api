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
using MongoDB.Driver;

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
                join q in _context.Questions.AsQueryable() on a.Assessment_Id equals q.Assessment_Id
                //where a.SectorId == sector_id
                select q).ToList();

            var tempQuery = (from q in query.AsQueryable()
                group new {q.Assessment_Id, q.Answer_Text} by new
                {
                    q.Assessment_Id,
                    q.Answer_Text
                }
                into g
                select new
                {
                    g.Key.Assessment_Id,
                    g.Key.Answer_Text,
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
                if (sums.TryGetValue(a.Assessment_Id, out quickSum))
                {
                    quickSum.TotalCount += a.Count;
                    if (a.Answer_Text == "Y" || a.Answer_Text == "A")
                    {
                        quickSum.YesAltCount += a.Count;
                    }
                }
                else
                {
                    int yaltCount = 0;
                    if (a.Answer_Text == "Y" || a.Answer_Text == "A")
                    {
                        yaltCount = a.Count;
                    }
                    sums.Add(a.Assessment_Id, new QuickSum() { assesment_id = a.Assessment_Id, TotalCount = a.Count, YesAltCount = yaltCount });

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

        private Series GetIndustryAnalytics(int sector_id, int industry_id)
        {
            var assessment = (from a in _context.Assessments.AsQueryable()
                where a.IndustryId == industry_id && a.SectorId == sector_id
                         select a).ToList();
            var questions = (from a in assessment.AsQueryable()
                join q in _context.Questions.AsQueryable() on a.Assessment_Id equals q.Assessment_Id
                select q);
            var query = (from q in questions.AsQueryable()
            group new { q.Assessment_Id, q.Answer_Text } by new
                {
                    q.Assessment_Id,
                    q.Answer_Text
                }
                into g
                select new
                {
                    g.Key.Assessment_Id,
                    g.Key.Answer_Text,
                    Count = g.Count()
                }).ToList();

            ////go through get the sum total of all
            ////get the sum of yes and alts
            ////then calc the percent of each assessment and
            ////sector average.

            Dictionary<string, QuickSum> sums = new Dictionary<string, QuickSum>();
            foreach (var a in query.ToList())
            {
                QuickSum quickSum;
                if (sums.TryGetValue(a.Assessment_Id, out quickSum))
                {
                    quickSum.TotalCount += a.Count;
                    if (a.Answer_Text == "Y" || a.Answer_Text == "A")
                    {
                        quickSum.YesAltCount += a.Count;
                    }
                }
                else
                {
                    int yaltCount = 0;
                    if (a.Answer_Text == "Y" || a.Answer_Text == "A")
                    {
                        yaltCount = a.Count;
                    }
                    sums.Add(a.Assessment_Id, new QuickSum() { assesment_id = a.Assessment_Id, TotalCount = a.Count, YesAltCount = yaltCount });

                }
            }

            //calculate the average percentage of all the assessments
            double average = 0;
            foreach (QuickSum s in sums.Values)
            {
                average += s.Percentage;
            }
            average = average / (sums.Values.Count() == 0 ? 1 : sums.Values.Count());

            return new Series() { name = IndustryAverageName, value = average * 100 };

        }

        private Series GetMyAnalytics(string myAssessment_Id)
        {
            var assessments = (from a in _context.Assessments.AsQueryable()
                where a.Assessment_Id == myAssessment_Id
                select a).ToList();
            var questions = (from a in assessments.AsQueryable()
                join q in _context.Questions.AsQueryable() on a.Assessment_Id equals q.Assessment_Id
                select q).ToList();
            var query = (from q in questions.AsQueryable()
                group new { q.Assessment_Id, q.Answer_Text } by new
                {
                    q.Assessment_Id,
                    q.Answer_Text
                }
                into g
                select new
                {
                    g.Key.Assessment_Id,
                    g.Key.Answer_Text,
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
                if (sums.TryGetValue(a.Assessment_Id, out quickSum))
                {
                    quickSum.TotalCount += a.Count;
                    if (a.Answer_Text == "Y" || a.Answer_Text == "A")
                    {
                        quickSum.YesAltCount += a.Count;
                    }
                }
                else
                {
                    int yaltCount = 0;
                    if (a.Answer_Text == "Y" || a.Answer_Text == "A")
                    {
                        yaltCount = a.Count;
                    }
                    sums.Add(a.Assessment_Id, new QuickSum() { assesment_id = a.Assessment_Id, TotalCount = a.Count, YesAltCount = yaltCount });

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

        public async Task<List<Series>> GetAverages(string assessment_id)
        {
            //var assessment =  _context.Assessments.Where(x => x.Assessment_Id == assessment_id).FirstOrDefault();
            var assessment = await _context.Assessments.FindAsync(a => a.Assessment_Id == assessment_id).Result.FirstOrDefaultAsync();

            if(assessment != null)
            {

                var sectionAnalytics = GetSectorAnalytics(assessment.SectorId);
                var industryAnalytics = GetIndustryAnalytics(assessment.SectorId,assessment.IndustryId);
                var myAnalytics = GetMyAnalytics(assessment_id);
                List<Series> listseries = new List<Series>();
                listseries.Add(sectionAnalytics);
                listseries.Add(industryAnalytics);
                listseries.Add(myAnalytics);
                return listseries;
            }
            else
            {
                return new List<Series>();
            }
        }

        public async Task<List<Assessment>> GetUserAssessments(string userId = "0")
        {
            var assessments = await _context.Assessments.Find(a => true).ToListAsync();
            return assessments;
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
