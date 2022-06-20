using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentACar.BL.Abstract;
using RentACar.UI.Models;
using RentACar.UI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentACar.UI.Controllers
{

    public class CarController : Controller
    {
        private ICarService carService;
        private IMapper mapper;

        public CarController(ICarService carService, IMapper mapper)
        {
            this.carService = carService;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {

            return View();

        }
        public IActionResult Cars(int page = 1, string Brand = null)
        {
            int pageSize = 6;
            var cars = Brand==null? carService.GetAllInfosWithAllCars() : carService.GetAllInfosWithAllCars().Where(x=>x.Brand.BrandName==Brand).ToList();
            var CarForReturn = mapper.Map<List<CarForListDto>>(cars);

            var model = new CarsListViewModel
            {
                CarForListDtos = CarForReturn.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                CurrentCar = Brand,
                CurrentPage = page,
                PageCount = (int)Math.Ceiling(CarForReturn.Count / (double)pageSize),
                PageSize = pageSize
            };
            return View(model);
        }
        public IActionResult GetCarById(int id)
        {
            var car = carService.GetAllInfosWithCar(x => x.Id == id);
            var model = mapper.Map<CarForDetailDto>(car);
            return View(model);
        }

    }
}
