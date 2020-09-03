using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CsetAnalytics.DomainModels;
using CsetAnalytics.DomainModels.Models;
using CsetAnalytics.Interfaces.Dashboard;
using CsetAnalytics.ViewModels.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CsetAnalytics.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {

        private readonly IConfiguration config;
        private readonly IDashboardBusiness _dashboardBusiness;
        private readonly CsetContext context;



        public DashboardController(IConfiguration config, IDashboardBusiness dashboardBusiness)
        {
            this.config = config;
            this._dashboardBusiness = dashboardBusiness;
        }

        [Authorize]
        [HttpGet]
        [Route("GetDashboardChart")]
        public async Task<IActionResult> GetDashBoardChart(string assessment_id)
        {
            try
            {
                //string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                List<Series> dashboardChartData = await _dashboardBusiness.GetAverages(assessment_id);

                return Ok(dashboardChartData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetAssessmentList")]
        public async Task<IActionResult> GetAssessmentList()
        {
            try
            {
                //string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                //return Ok(context.Assessments.Where(x => x.AssessmentCreatorId == userId).ToList());
                List<Assessment> assessmentData = await _dashboardBusiness.GetUserAssessments("0");

                return Ok(assessmentData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
