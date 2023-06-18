using AutoMapper;
using BusinessObject.Models;
using StoreAPI.DTO;

namespace StoreAPI.Mapper
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Product, ProductCreateDTO>().ReverseMap();
                config.CreateMap<ProductUpdateDTO, Product>().ForMember(dest => dest.Images, act => act.Ignore());
                config.CreateMap<Account, AccountLoginDTO>().ReverseMap();
                config.CreateMap<RegisterDTO, Account>().ForSourceMember(source => source.ConfirmPassword, opt => opt.DoNotValidate());
                config.CreateMap<Order, OrderCreateDTO>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
