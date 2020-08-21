using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CsetAnalytics.DomainModels.Models;
using CsetAnalytics.Interfaces.Factories;
using CsetAnalytics.ViewModels;

namespace CsetAnalytics.Factories.Analytics
{
    public class AnalyticAssessmentFactory : IBaseFactory<AnalyticAssessmentViewModel, Assessment>
    {
        private readonly IMapper _mapper;

        public AnalyticAssessmentFactory(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Assessment Create(AnalyticAssessmentViewModel question)
        {
            return _mapper.Map<Assessment>(question);
        }

        public IQueryable<Assessment> Create(IQueryable<AnalyticAssessmentViewModel> questions)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AnalyticAssessmentViewModel, Assessment>());
            return questions.ProjectTo<Assessment>(config);
        }
    }
}
