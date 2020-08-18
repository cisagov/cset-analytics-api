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
    public class AnalyticQuestionModelFactory : IBaseFactory<AnalyticQuestionViewModel, AnalyticQuestionAnswer>
    {
        private readonly IMapper _mapper;

        public AnalyticQuestionModelFactory(IMapper mapper)
        {
            _mapper = mapper;
        }

        public AnalyticQuestionAnswer Create(AnalyticQuestionViewModel question)
        {
            return _mapper.Map<AnalyticQuestionAnswer>(question);
        }

        public IQueryable<AnalyticQuestionAnswer> Create(IQueryable<AnalyticQuestionViewModel> questions)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AnalyticQuestionViewModel, AnalyticQuestionAnswer>());
            return questions.ProjectTo<AnalyticQuestionAnswer>(config);
        }
    }
}
