using RentACar.BL.Abstract;
using RentACar.Dal.Abstract.EF;
using RentACar.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RentACar.BL.Concrete
{
    public class RentDetailManager : ManagerBase<RentDetail>, IRentDetailService
    {
        private IRentDetailDal eFRentDetailDal;

        public RentDetailManager(IRentDetailDal eFRentDetailDal)
        {
            this.eFRentDetailDal = eFRentDetailDal;

        }


        public RentDetail GetAllRentInfo(Expression<Func<RentDetail, bool>> filter = null)
        {
            return eFRentDetailDal.GetAllInclude(filter, x => x.Car, x => x.Rent, x => x.Rent.Customer, x => x.Car.Brand, x => x.Car.CarColor, x => x.Car.GearBox, x => x.Car.Fuel, x => x.Car.Photos).FirstOrDefault();
        }

        public List<RentDetail> GetAllRentsInfo(Expression<Func<RentDetail, bool>> filter = null)
        {
            return eFRentDetailDal.GetAllInclude(filter, x => x.Car, x => x.Rent, x => x.Rent.Customer, x => x.Car.Brand, x => x.Car.CarColor, x => x.Car.GearBox, x => x.Car.Fuel, x => x.Car.Photos).ToList();
        }


    }
}
