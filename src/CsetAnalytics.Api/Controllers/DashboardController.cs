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
        public async Task<IActionResult> GetDashBoardChart(string industry, string assessment_id)
        {
            try
            {
                var dashboardChartData = await _dashboardBusiness.GetDashboardData(industry, assessment_id);

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
                List<Assessment> assessmentData = await _dashboardBusiness.GetUserAssessments("0");
                var assessment_count = assessmentData.Count();
                return Ok(new AssessmentData { Items = assessmentData, Total_count = assessment_count});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getSectors")]
        public async Task<IActionResult> GetSectors()
        {
            var sectors = await _dashboardBusiness.GetSectors();
            var flattenSectors = sectors.Select(x => new TreeView
            {
                Name = x.SectorName, Children = x.Industries?.Select(y => new TreeView {Name = y.IndustryName}).ToList()
            }).ToList();
            flattenSectors.Insert(0, new TreeView { Name= "All Sectors", Children= null});
            return Ok(flattenSectors);
        }
    }
}
