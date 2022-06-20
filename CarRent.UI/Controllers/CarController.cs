using Microsoft.AspNetCore.Mvc;
using RentACar.BL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.UI.Controllers
{
    public class CarController : Controller
    {
        private ICarService carManager;
        public CarController(ICarService carManager)
        {
            this.carManager = carManager;
        }
        public IActionResult Index()
        {
            var cars = carManager.GetAll();
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
