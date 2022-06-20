using RentACar.Dal.Concrete.EF;
using System;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            EFCarDal dal = new EFCarDal();
            EFBrandDal brandDal = new EFBrandDal();
            EFColorDal colordal = new EFColorDal();
            EFFuelDal fuelDal = new EFFuelDal();
            EFGearBoxDal vitesDal = new EFGearBoxDal();

            //var vites = vitesDal.Get(p => p.Id == 1);
            //var brand = brandDal.Get(p => p.Id == 1);
            //var color = colordal.Get(p => p.Id == 1);
            //var fuel = fuelDal.Get(p => p.Id == 1);


            //Car car = new Car();
            //car.Model = "muratti";
            //car.Price = 10000;
            //car.HorsePower = "50 hp";
            //car.BrandId = brand.Id;
            //car.CarColorId = color.Id;
            //car.GearBoxId = vites.Id;
            //car.FuelId = fuel.Id;
            //dal.Add(car);
            var car = dal.Get(x => x.Id == 4);
            car.For_Rent = true;
            dal.Update(car);


            Console.WriteLine("Hello World!");
        }
    }
}
