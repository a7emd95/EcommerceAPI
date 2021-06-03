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
                  //  cfg.CreateMap<Product, ProductForCartDto>().ReverseMap();
                    cfg.CreateMap<Cart, CartDto>().ReverseMap();

                    #region Test
                    // cfg.CreateMap<Product, ProductForCartDto>().ForMember(d => d.ProductID, opt => opt.MapFrom(src => src.ID))
                    //.ForMember(d => d.Quantity, opt => opt.Ignore())
                    //.ReverseMap();

                    //  cfg.CreateMap<ProductCart, ProductForCartDto>().ForMember(d => d.Quantity, opt => opt.MapFrom(s => s.Quantity))
                    //  .ForMember(d => d.ID, opt => opt.MapFrom(s => s.ID))
                    //  .ForMember(d => d.TotalPrice, opt => opt.MapFrom)
                    #endregion
                }
                );

            Mapper = config.CreateMapper();
        }



    }
}
