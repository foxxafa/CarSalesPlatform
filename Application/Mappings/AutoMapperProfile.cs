﻿using Application.DTOs.CarDTO.CreateCarDTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateCarDTO, Car>().ReverseMap();
        }
    }

}
