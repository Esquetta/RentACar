using RentACar.Dal.Abstract.EF;
using RentACar.Dal.Contexts;
using RentACar.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Dal.Concrete.EF
{
    public class EFGearBoxDal:RepositoryBase<GearBox,AppIdentityDbContext>,IGearBoxDal
    {
    }
}
