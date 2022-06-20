using RentACar.BL.Abstract;
using RentACar.Dal.Abstract.EF;
using RentACar.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RentACar.BL.Concrete
{
    public class CarManager : ManagerBase<Car>, ICarService
    {
        private ICarDal carDal;
        public CarManager(ICarDal carDal)
        {
            this.carDal = carDal;

        }

        public Car GetAllInfosWithCar(Expression<Func<Car, bool>> filter = null)
        {
            return carDal.GetAllInclude(filter, x => x.Brand, x => x.CarColor, x => x.GearBox, x => x.Fuel, x => x.Photos).FirstOrDefault();
        }

        public List<Car> GetAllInfosWithAllCars()
        {
            return carDal.GetAllInclude(x => x.For_Rent == true, x => x.Brand, x => x.CarColor, x => x.GearBox, x => x.Fuel, x => x.Photos).ToList();
        }


    }
}
