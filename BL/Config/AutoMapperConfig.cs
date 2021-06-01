using AutoMapper;
using BL.DTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Config
{
    public static class AutoMapperConfig
    {
        public static IMapper Mapper { get; set; }

        static AutoMapperConfig()
        {
            var config = new MapperConfiguration(

                cfg =>
                {
                    cfg.CreateMap<Category, CategroyDto>().ReverseMap();
                    cfg.CreateMap<Category, CategroyWithProductsDto>().ReverseMap();
                    cfg.CreateMap<Product, ProductDto>().ReverseMap();
                }
                );

            Mapper = config.CreateMapper();
        }



    }
}
