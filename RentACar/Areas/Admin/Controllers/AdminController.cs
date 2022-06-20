using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using RentACar.BL.Abstract;
using RentACar.Entities.Concrete;
using RentACar.UI.Dtos;
using RentACar.UI.Helpers;
using RentACar.UI.Models;
using RentACar.UI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentACar.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
    [Authorize(Roles = "Manager")]
    public class AdminController : Controller
    {
        private ICarService carService;
        private IMapper mapper;
        private IBrandService brandService;
        private IColorService colorService;
        private IGearBoxService gearBoxService;
        private IFuelService fuelService;

        private IRentDetailService rentDetailService;
        private IOptions<CloudinarySettings> options;
        private IPhotoService photoService;
        private Cloudinary cloudinary;

        public AdminController(ICarService carService, IMapper mapper, IBrandService brandService, IColorService colorService, IGearBoxService gearBoxService, IFuelService fuelService, IOptions<CloudinarySettings> cloudinaryOptions, IPhotoService photoService, IRentDetailService rentDetailService)
        {
            this.carService = carService;
            this.mapper = mapper;
            this.brandService = brandService;
            this.fuelService = fuelService;
            this.gearBoxService = gearBoxService;
            this.colorService = colorService;
            this.options = cloudinaryOptions;
            this.photoService = photoService;

            this.rentDetailService = rentDetailService;

            Account account = new Account(options.Value.CloudName, options.Value.ApiKey, options.Value.ApiSecret);
            cloudinary = new Cloudinary(account);

        }
        public IActionResult HomePage(int page = 1, string Brand = null)


        {

            int pageSize = 6;
            var cars = Brand == null ? carService.GetAllInfosWithAllCars() : carService.GetAllInfosWithAllCars().Where(x => x.Brand.BrandName == Brand).ToList();
            var CarForReturn = mapper.Map<List<CarForListDto>>(cars);
            var model = new CarsListViewModel
            {
                CarForListDtos = CarForReturn.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                CurrentCar = Brand,
                CurrentPage = page,
                PageCount = (int)Math.Ceiling(CarForReturn.Count / (double)pageSize),
                PageSize = page
            };

            return View(model);
        }
        public IActionResult GetCarById(int id)
        {
            var car = carService.GetAllInfosWithCar(x => x.Id == id);
            var model = mapper.Map<CarForDetailDto>(car);
            return View(model);
        }
        [HttpGet]
        public IActionResult AddCar()
        {
            SelectList brandList = new SelectList(brandService.GetAll(), "Id", "BrandName");
            SelectList colorList = new SelectList(colorService.GetAll(), "Id", "Color");
            SelectList gearBoxList = new SelectList(gearBoxService.GetAll(), "Id", "GearType");
            SelectList fuelList = new SelectList(fuelService.GetAll(), "Id", "FuelType");
            ViewBag.BrandList = brandList;
            ViewBag.ColorList = colorList;
            ViewBag.GearBoxList = gearBoxList;
            ViewBag.FuelList = fuelList;
            return View();
        }
        [HttpPost]
        public IActionResult AddCar(Car car)
        {
            if (!ModelState.IsValid)
            {
                SelectList brandList = new SelectList(brandService.GetAll(), "Id", "BranName");
                SelectList colorList = new SelectList(colorService.GetAll(), "Id", "Color");
                SelectList gearBoxList = new SelectList(gearBoxService.GetAll(), "Id", "GearType");
                SelectList fuelList = new SelectList(fuelService.GetAll(), "Id", "FuelType");
                ViewBag.BrandList = brandList;
                ViewBag.ColorList = colorList;
                ViewBag.GearBoxList = gearBoxList;
                ViewBag.FuelList = fuelList;

                return View();
            }
            car.For_Rent = true;
            car.CreateDate = DateTime.Now;
            carService.Add(car);
            TempData.Add("Created", "Add created Successfully!");
            return View();
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            SelectList brandList = new SelectList(brandService.GetAll(), "Id", "BrandName");
            SelectList colorList = new SelectList(colorService.GetAll(), "Id", "Color");
            SelectList gearBoxList = new SelectList(gearBoxService.GetAll(), "Id", "GearType");
            SelectList fuelList = new SelectList(fuelService.GetAll(), "Id", "FuelType");
            ViewBag.BrandList = brandList;
            ViewBag.ColorList = colorList;
            ViewBag.GearBoxList = gearBoxList;
            ViewBag.FuelList = fuelList;
            var car = carService.GetAllInfosWithCar(x => x.Id == id);
            var model = mapper.Map<CarForDetailDto>(car);
            return View(car);

        }
        [HttpPost]
        public IActionResult Update(int id, Car car)
        {
            if (!ModelState.IsValid)
            {
                SelectList brandList = new SelectList(brandService.GetAll(), "Id", "BrandName");
                SelectList colorList = new SelectList(colorService.GetAll(), "Id", "Color");
                SelectList gearBoxList = new SelectList(gearBoxService.GetAll(), "Id", "GearType");
                SelectList fuelList = new SelectList(fuelService.GetAll(), "Id", "FuelType");
                ViewBag.BrandList = brandList;
                ViewBag.ColorList = colorList;
                ViewBag.GearBoxList = gearBoxList;
                ViewBag.FuelList = fuelList;
                return View(car);
            }
            car.UpdateDate = DateTime.Now;
            carService.Update(car);
            TempData.Add("Updated", "Add updated Successfully!");
            return RedirectToAction("HomePage", "Admin");
        }
        [HttpGet]
        public IActionResult AddPhoto(int Id)
        {
            if (Id == null)
            {
                return View();
            }
            ViewBag.unit = Id;
            var model = new PhotoForCreationDto
            {

                CarId = Id

            };
            return View(model);
        }
        [HttpPost]
        public IActionResult AddPhoto(PhotoForCreationDto photoForCreationDto)
        {
            var file = photoForCreationDto.File;
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                foreach (var item in file)
                {
                    using (var stream = item.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams { File = new FileDescription(item.Name, stream) };
                        uploadResult = cloudinary.Upload(uploadParams);
                    }
                    photoForCreationDto.Url = uploadResult.Uri.ToString();
                    photoForCreationDto.PublicId = uploadResult.PublicId;
                    var photo = mapper.Map<Photo>(photoForCreationDto);
                    photo.IsMain = true;
                    photoService.Add(photo);

                }

            }
            else
            {
                TempData.Add("NoPhoto", "You must add photo you can upload it.");
                return View();
            }

            return RedirectToAction("Homepage", "Admin");
        }
        public IActionResult Delete(int id)
        {
            var car = carService.Get(x => x.Id == id);
            if (car == null)
            {
                TempData.Add("DeleteError", "An error accured.");
                return RedirectToAction("HomePage", "Admin");
            }
            carService.Delete(car);
            TempData.Add("DeleteSuccess", "Car add successfully deleted");
            return RedirectToAction("HomePage", "Admin");
        }
        [HttpGet]
        public IActionResult Brand()
        {
            var brands = brandService.GetAll();
            var model = new BrandsListViewModel
            {
                Brands = brands
            };
            return View(model);

        }
        //[HttpPost]
        //public IActionResult UpdateBrand(int Id, string BrandName)
        //{
        //    if (brand == null)
        //    {
        //        return RedirectToAction("Brand", "Admin");
        //    }
        //    brandService.Update(new Brand { Id = Id, BrandName = BrandName });
        //    TempData.Add("UpdateSuccess", "Item successfully updated!");
        //    return RedirectToAction("Brand", "Admin");
        //}
        public IActionResult DeleteBrand(int Id)
        {
            if (Id == null)
            {
                return RedirectToAction("Brand", "Admin");
            }
            brandService.Delete(brandService.Get(x => x.Id == Id));
            TempData.Add("BrandDeleteSuccess", "Brand successfully deleted");
            return RedirectToAction("HomePage", "Admin");

        }
        [HttpGet]
        public IActionResult AddBrand()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBrand(Brand brand)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            brand.CreateDate = DateTime.Now;
            brandService.Add(brand);
            TempData.Add("BrandAddSuccess", "Brand successfully added!");
            return RedirectToAction("Brand", "Admin");
        }
        [HttpGet]
        public IActionResult RentedCars(int page = 1)
        {
            int PageSize = 6;
            var rents = rentDetailService.GetAllRentsInfo();
            List<CarForListDto> carForListDto = new List<CarForListDto>();
            foreach (var item in rents)
            {
                carForListDto.Add(mapper.Map<CarForListDto>(item.Car));
            }
            var model = mapper.Map<List<RentedCarsWithCustomerListDto>>(rents).Skip((page - 1) * PageSize).Take(PageSize).ToList();
            model[0].CurrentPage = page;
            model[0].PageCount = (int)Math.Ceiling(rents.Count / (double)PageSize);
            int i = 0;
            foreach (var item in model)
            {
                item.CarForList = carForListDto[i];
                i++;
            }


            return View(model);
        }
        [HttpGet]
        public IActionResult CompleteRent(int id)
        {
            if (id != null)
            {
                var rent = rentDetailService.GetAllRentInfo(x => x.CarId == id && x.Rent.IsFinished == false);

                rent.Car.For_Rent = true;
                rent.Rent.IsFinished = true;
                rentDetailService.Update(rent);
                TempData.Add("RentFinished", "Rent Successfully completed.Car is in our garage");

            }
            return RedirectToAction("HomePage", "Admin", new { area = "admin" });
        }


    }
}
