using AutoMapper;
using News_List.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using News_List.Models;

namespace News_List.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<News, NewsItem>();

                cfg.CreateMap<News, NewsModel>()
                    .ForMember("ThemeName", x => x.MapFrom(news => news.Theme.Name))
                    .ForMember("SourceName", x => x.MapFrom(news => news.Source.Name));

                cfg.CreateMap<NewsModel, News>();

                cfg.CreateMap<NewsModel, News>()
                    .ForMember("CreateTime", x => x.MapFrom(news => DateTime.Now));

                
            });
        }
    }
}