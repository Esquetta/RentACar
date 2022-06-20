using RentACar.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RentACar.BL.Abstract
{
    public interface ICarService : IServiceBase<Car>
    {
        List<Car> GetAllInfosWithAllCars();
        Car GetAllInfosWithCar(Expression<Func<Car, bool>> filter = null);
    }
}
