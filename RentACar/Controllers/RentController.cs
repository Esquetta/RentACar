using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentACar.BL.Abstract;
using RentACar.Entities.Concrete;
using RentACar.UI.Models;
using RentACar.UI.Models.Dtos;
using RentACar.UI.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.UI.Controllers
{
    [Authorize]
    public class RentController : Controller
    {

        private IRentService rentService;
        private IRentCookieeService CookieeService;
        private ICarCookieeService carCookiee;
        private ICarService carService;
        private UserManager<Customer> userManager;
        private IRentDetailService rentDetailService;
        private IMapper mapper;
        private ISmtpService smtpService;
        public RentController(IRentService rentService,
                                    IRentCookieeService CookieeService,
                                    ICarCookieeService carCookiee,
                                    ICarService carService,
                                    UserManager<Customer> userManager,
                                    IMapper mapper,
                                    IRentDetailService rentDetailService,
                                    ISmtpService smtpService)
        {
            this.rentService = rentService;
            this.CookieeService = CookieeService;
            this.carCookiee = carCookiee;
            this.carService = carService;
            this.userManager = userManager;
            this.mapper = mapper;
            this.rentDetailService = rentDetailService;
            this.smtpService = smtpService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {

            var user = CookieeService.GetUserName();
            var userCheck = await userManager.FindByNameAsync(user);
            if (userCheck == null)
            {
                return RedirectToAction("Cars", "Car");
            }
            CookieeService.SetValue("Rent", new Rent { Customer = userCheck, CreateDate = DateTime.Now });
            HttpContext.Response.Cookies.Append("Car", id.ToString());


            return RedirectToAction("RentConfirm", "Rent");
        }

        [HttpGet]
        public IActionResult RentConfirm()
        {
            var id = HttpContext.Request.Cookies["Car"];
            var rent = CookieeService.GetObject("Rent");
            var car = carService.GetAllInfosWithCar(x => x.Id == int.Parse(id));
            var vehicle = mapper.Map<CarForListDto>(car);
            var model = new RentCreationDto
            {
                Rent = rent,
                RentDetail = new RentDetail(),
                Car = vehicle

            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RentConfirm(RentCreationDto rentCreationDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var car = carService.GetAllInfosWithCar(x => x.Id == rentCreationDto.Car.Car_id);
            var user = await userManager.FindByNameAsync(rentCreationDto.Rent.Customer.UserName);
            Rent rent = new Rent
            {
                CustomerId = user.Id,
                CreateDate = DateTime.Now,
                DateOfIssue = rentCreationDto.TakenDate,
                ReturnDate = rentCreationDto.ReturnDate,
                IsFinished = false

            };
            rentService.Add(rent);
            var addedRent = rentService.Get(x => x.CustomerId == user.Id && x.DateOfIssue == rentCreationDto.TakenDate && x.IsFinished == false && x.CreateDate.Minute == DateTime.Now.Minute);
            rentDetailService.Add(new RentDetail { RentId = addedRent.Id, CarId = rentCreationDto.Car.Car_id, Price = car.Price });

            car.For_Rent = false;
            carService.Update(car);
            smtpService.PaymentReceipit(user.Email, user.UserName, car, rentCreationDto.TakenDate, rentCreationDto.ReturnDate);
            TempData.Add("RentSuccess", "Rent order successfully Finished");



            return RedirectToAction("Cars", "Car");
        }
        [HttpGet]
        [Authorize]
        public IActionResult GetCustomersRent(int page=1)
        {
            int pageSize = 6;
            var username = CookieeService.GetUserName();
            var rents = rentDetailService.GetAllRentsInfo(x => x.Rent.Customer.UserName == username);
            List<CarForListDto> carForListDto = new List<CarForListDto>();
            foreach (var item in rents)
            {
                carForListDto.Add(mapper.Map<CarForListDto>(item.Car));
            }
            var model = mapper.Map<List<RentedCarsWithCustomerListDto>>(rents).Skip((page-1)*pageSize).Take(pageSize).ToList();
            model[0].PageCount = (int)Math.Ceiling(rents.Count / (double)pageSize);
            model[0].CurrentPage = page;
            int i = 0;
            foreach (var item in model)
            {
                item.CarForList = carForListDto[i];
                i++;
            }
            return View(model);
        }
    }
}
