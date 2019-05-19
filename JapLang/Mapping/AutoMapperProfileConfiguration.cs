using AutoMapper;
using DAL.Entity;
using JapLang.BLL.DTO;
using JapLang.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JCL.Mapping
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        : this("MyProfile")
        {
        }

        protected AutoMapperProfileConfiguration(string profileName)
        : base(profileName)
        {
            CreateMap<WordJapan, WordDTO>().ForMember(c => c.Category, k => k.MapFrom(p => p.WordJapanCategory));
            CreateMap<WordRus, WordRusDTO>();

            CreateMap<WordRusDTO, WordRus>();
            CreateMap<WordV2DTO, WordJapan>();

            CreateMap<News, NewsFullDTO>();
            CreateMap<NewsFullV2DTO, News>();
            CreateMap<News, NewsFullDTO>();

            CreateMap<WordJapanCategory, CategoryDTO>();

            CreateMap<WordJapan, TestJapDTO>().ForMember(c => c.WordJap, k => k.MapFrom(p => p.Word))
                .ForMember(c => c.Answer, k => k.MapFrom(p => p.WordRus));
        }
    }
}
