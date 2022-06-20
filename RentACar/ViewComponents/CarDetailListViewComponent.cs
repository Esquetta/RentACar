using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using RentACar.BL.Abstract;
using RentACar.UI.Models.Dtos;

namespace RentACar.UI.ViewComponents
{
    public class CarDetailListViewComponent : ViewComponent
    {
        private ICarService carService;
        private IMapper mapper;
        public CarDetailListViewComponent(ICarService carService, IMapper mapper)
        {
            this.carService = carService;
            this.mapper = mapper;
        }

        public ViewViewComponentResult Invoke(int carId)
        {
            var car = carService.GetAllInfosWithCar(x => x.Id == carId);
            var model = mapper.Map<CarForDetailDto>(car);
            return View(model);
        }
    }
}
