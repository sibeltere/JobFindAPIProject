﻿using AutoMapper;
using JobFind.DataLayer.DTOModels;
using JobFind.DataLayer.DTOModels.Request;
using JobFind.DataLayer.DTOModels.Response;
using JobFind.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFind.Helpers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            #region UserMap
            CreateMap<User, UserDTO>()
                .ReverseMap();
            CreateMap<User, ResponseUserDTO>()
                .ForMember(x => x.CVDTO, cd => cd.MapFrom(map => map.CV))
                .ReverseMap();
            #endregion

            #region FirmMap
            CreateMap<Firm, FirmDTO>().ReverseMap();
            CreateMap<Firm, ResponseFirmDTO>().ReverseMap();
            #endregion

            #region EducationMap
            CreateMap<Education, EducationDTO>().ReverseMap();
            CreateMap<Education, ResponseEducationDTO>().ReverseMap();
            #endregion

            #region ExperienceMap
            CreateMap<Experience, ExperienceDTO>().ReverseMap();
            CreateMap<Experience, ResponseExperienceDTO>().ReverseMap();
            #endregion

            #region CVMap
            CreateMap<CV, CVDTO>()
                .ForMember(x => x.EducationInformationsDTO, cd => cd.MapFrom(map => map.EducationInformations))
                .ForMember(x => x.ExperienceInformationsDTO, cd => cd.MapFrom(map => map.ExperienceInformations))
                .ReverseMap();
            CreateMap<CV, ResponseCVDTO>()
              .ForMember(x => x.ResponseEducationInformationsDTO, cd => cd.MapFrom(map => map.EducationInformations))
              .ForMember(x => x.ResponseExperienceInformationsDTO, cd => cd.MapFrom(map => map.ExperienceInformations))
              .ReverseMap();
            #endregion



            #region JobPostMap
            CreateMap<JobPost, JobPostDTO>().ReverseMap();
            #endregion


        }
    }
}