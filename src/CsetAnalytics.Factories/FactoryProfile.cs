using System;
using System.Collections.Generic;
using AutoMapper;
using CsetAnalytics.DomainModels.Models;
using CsetAnalytics.ViewModels;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace CsetAnalytics.Factories
{
    public class FactoryProfile : Profile
    {
        public FactoryProfile()
        {
            RegisterMappings();
        }

        private void RegisterMappings()
        {
            CreateMap<AnalyticQuestionAnswer, AnalyticQuestionViewModel>();
            CreateMap<AnalyticQuestionViewModel, AnalyticQuestionAnswer>()
                .ForMember(dest => dest.Question_Or_Requirement_Id,
                    opt=>opt.MapFrom(
                        src => src.QuestionId));
            CreateMap<AnalyticAssessmentViewModel, Assessment>();

        }
    }
}
