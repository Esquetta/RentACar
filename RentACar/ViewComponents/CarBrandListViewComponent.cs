using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using RentACar.Dal.Concrete.EF;
using RentACar.UI.Models;

namespace RentACar.UI.ViewComponents
{
    public class CarBrandListViewComponent : ViewComponent
    {
        private IBrandDal brandDal;
        public CarBrandListViewComponent(IBrandDal brandDal)
        {
            this.brandDal = brandDal;
        }

        public ViewViewComponentResult Invoke(string page)
        {
            var brands = brandDal.GetAll();
            var model = new CarInfoWithPageModel
            {
                Brands = brands,
                Page = page,
                CurrentModel = HttpContext.Request.Query["Brand"]
            };

            return View(model);

        }

    }
}
