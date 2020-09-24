using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;
using CsetAnalytics.DomainModels.Models;
using Microsoft.AspNetCore.Mvc;
using CsetAnalytics.ViewModels;
using CsetAnalytics.Interfaces.Analytics;
using CsetAnalytics.Interfaces.Factories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CsetAnalytics.Api.Controllers
{
    [Route("api/Analytics")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly IBaseFactory<AnalyticQuestionViewModel, AnalyticQuestionAnswer> _questionViewModelFactory;
        private readonly IBaseFactory<AnalyticAssessmentViewModel, Assessment> _assessmentViewModelFactory;
        private readonly IAnalyticBusiness _analyticsBusiness;

        public AnalyticsController(IBaseFactory<AnalyticQuestionViewModel, AnalyticQuestionAnswer> questionViewModelFactory,
            IAnalyticBusiness analyticsBusiness,
            IBaseFactory<AnalyticAssessmentViewModel, Assessment> assessmentViewModelFactory)
        {
            _questionViewModelFactory = questionViewModelFactory;
            _assessmentViewModelFactory = assessmentViewModelFactory;
            _analyticsBusiness = analyticsBusiness;
        }

        //[Authorize]
        [HttpPost]
        [Route("postAnalyticsAnonymously")]
        public async Task<IActionResult> PostAnalyticsAnonymously([FromBody] AnalyticsViewModel analytics)
        {
            try
            {
                Assessment assessment = _assessmentViewModelFactory.Create(analytics.Assessment);
                assessment.AssessmentCreatorId = null;
                assessment = await _analyticsBusiness.SaveAssessment(assessment);

                List<AnalyticQuestionAnswer> questions = (_questionViewModelFactory.Create(analytics.QuestionAnswers.AsQueryable())).ToList();
                questions.ForEach(x => x.AssessmentId = assessment.Assessment_Id);
                questions.Where(x => x.AnswerText == null).ToList().ForEach(x => x.AnswerText = "U");



                await _analyticsBusiness.SaveAnalyticQuestions(questions);
                return Ok(new { message = "Analytics data saved" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Analytic information was not saved: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost]
        [Route("postAnalytics")]
        public async Task<IActionResult> PostAnalytics([FromBody] AnalyticsViewModel analytics)
        {
            try
            {
                //string userId = this.User.FindFirstValue(ClaimTypes.Name);
                var identity = (ClaimsIdentity) this.User.Identity;
                var claims = identity.Claims.ToList();
                var username = claims.FirstOrDefault(x => x.Type == "cognito:username")?.Value;
                var groupQuestions = from q in analytics.QuestionAnswers
                    group q by q.SetName
                    into qGroup
                    select qGroup;

                foreach (var q in groupQuestions)
                {
                    Assessment assessment = _assessmentViewModelFactory.Create(analytics.Assessment);
                    assessment.AssessmentCreatorId = username;
                    assessment.SetName = q.FirstOrDefault()?.SetName;
                    assessment = await _analyticsBusiness.SaveAssessment(assessment);

                    List<AnalyticQuestionAnswer> questions = (_questionViewModelFactory.Create(q.AsQueryable())).ToList();
                    questions.ForEach(x => x.AssessmentId = assessment.Assessment_Id);
                    questions.Where(x => x.AnswerText == null).ToList().ForEach(x => x.AnswerText = "U");

                    await _analyticsBusiness.SaveAnalyticQuestions(questions);
                }
                
                return Ok(new { message = "Analytics data saved" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Analytics information was not saved" });
            }
        }
    }
}
