using AutoMapper;
using RentACar.Entities.Concrete;
using RentACar.UI.Dtos;
using RentACar.UI.Models;
using RentACar.UI.Models.Dtos;
using System;
using System.Linq;

namespace RentACar.UI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Car, CarForListDto>().ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.BrandName))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.CarColor.Color))
                .ForMember(dest => dest.Fuel_Type, opt => opt.MapFrom(src => src.Fuel.FuelType))
                .ForMember(dest => dest.Gearbox, opt => opt.MapFrom(src => src.GearBox.GearType))
                .ForMember(dest => dest.Car_id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTime.Now.Year - src.ProductionDate.Year))
                .ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url);
                });
            CreateMap<Car, CarForDetailDto>().ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.BrandName))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.CarColor.Color))
                .ForMember(dest => dest.Fuel_Type, opt => opt.MapFrom(src => src.Fuel.FuelType))
                .ForMember(dest => dest.Gearbox, opt => opt.MapFrom(src => src.GearBox.GearType))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTime.Now.Year - src.ProductionDate.Year))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));


            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<RentDetail, RentedCarsWithCustomerListDto>().ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Rent.Customer.FirstName))
                .ForMember(dest => dest.CustomerUserName, opt => opt.MapFrom(src => src.Rent.Customer.UserName))
                .ForMember(dest => dest.CustomerSurName, opt => opt.MapFrom(src => src.Rent.Customer.LastName))
                .ForMember(dest => dest.GivenDate, opt => opt.MapFrom(src => src.Rent.DateOfIssue))
                .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.Rent.ReturnDate))
                .ForMember(dest => dest.IsFinished, opt => opt.MapFrom(src => src.Rent.IsFinished));









        }
    }
}
