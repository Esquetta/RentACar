using RentACar.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RentACar.BL.Abstract
{
    public interface IRentDetailService : IServiceBase<RentDetail>
    {
        RentDetail GetAllRentInfo(Expression<Func<RentDetail, bool>> filter = null);
        List<RentDetail> GetAllRentsInfo(Expression<Func<RentDetail, bool>> filter = null);
    }
}
