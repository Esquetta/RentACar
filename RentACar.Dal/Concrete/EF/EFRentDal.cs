using RentACar.Dal.Abstract.EF;
using RentACar.Dal.Contexts;
using RentACar.Entities.Concrete;

namespace RentACar.Dal.Concrete.EF
{
    public class EFRentDal : RepositoryBase<Rent, AppIdentityDbContext>, IRentDal
    {
    }
}
