﻿using AutoMapper;
using FitDiary.Contracts.DTOs.Diet;
using FitDiary.Contracts.DTOs.User;
using FitDiary.SecuredApi.Models.Diet;
using FitDiary.SecuredApi.Models.User;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(FitDiary.SecuredApi.Startup))]

namespace FitDiary.SecuredApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureAuth(app);

            Mapper.Initialize(cfg => {
                cfg.CreateMap<FoodProduct, FoodProductDTO>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
                cfg.CreateMap<BodyGoals, BodyGoalsDTO>();
            });
        }
    }
}
