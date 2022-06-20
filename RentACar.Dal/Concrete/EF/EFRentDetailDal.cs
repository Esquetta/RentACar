using RentACar.Dal.Abstract.EF;
using RentACar.Dal.Contexts;
using RentACar.Entities.Concrete;

namespace RentACar.Dal.Concrete.EF
{
    public class EFRentDetailDal : RepositoryBase<RentDetail, AppIdentityDbContext>, IRentDetailDal
    {
    }
}
