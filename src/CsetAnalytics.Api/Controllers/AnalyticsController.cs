using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CsetAnalytics.DomainModels.Models;
using Microsoft.AspNetCore.Mvc;
using CsetAnalytics.ViewModels;
using CsetAnalytics.Interfaces.Analytics;
using CsetAnalytics.Interfaces.Factories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;

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
        [EnableCors]
        public async Task<IActionResult> PostAnalyticsAnonymously([FromBody] AnalyticsViewModel analytics)
        {
            try
            {
                Assessment assessment = _assessmentViewModelFactory.Create(analytics.Assessment);
                assessment.AssessmentCreatorId = null;
                assessment = await _analyticsBusiness.SaveAssessment(assessment);

                List<AnalyticQuestionAnswer> questions = (_questionViewModelFactory.Create(analytics.QuestionAnswers.AsQueryable())).ToList();
                questions.ForEach(x => x.Assessment_Id = assessment.Assessment_Id);
                questions.Where(x => x.Answer_Text == null).ToList().ForEach(x => x.Answer_Text = "U");



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
        [EnableCors]
        public async Task<IActionResult> PostAnalytics([FromBody] AnalyticsViewModel analytics)
        {
            try
            {
                //string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                //AnalyticDemographic demographic = _demographicViewModelFactory.Create(analytics.Demographics);

                //AnalyticDemographic rDemographic = await _analyticsBusiness.SaveAnalyticDemographic(demographic);
                //Assessment assessment = _assessmentViewModelFactory.Create(analytics.Assessment);
                ////assessment.AnalyticDemographicId = rDemographic.AnalyticDemographicId;
                //assessment.AssessmentCreatorId = userId;
                //assessment = await _analyticsBusiness.SaveAssessment(assessment);

                //List<AnalyticQuestionAnswer> questions = (_questionViewModelFactory.Create(analytics.QuestionAnswers.AsQueryable())).ToList();
                //questions.ForEach(x => x.Assessment_Id = assessment.Assessment_Id);
                //questions.ForEach(x => x.Answer_Text = string.IsNullOrEmpty(x.Answer_Text) ? "U" : x.Answer_Text);
                //await _analyticsBusiness.SaveAnalyticQuestions(questions);
                return Ok(new { message = "Analytics data saved" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Analytics information was not saved" });
            }
        }
    }
}
