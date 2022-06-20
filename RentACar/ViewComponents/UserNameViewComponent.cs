using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using RentACar.UI.Models;

namespace RentACar.UI.ViewComponents
{
    public class UserNameViewComponent : ViewComponent
    {
        public ViewViewComponentResult Invoke()
        {

            var model = new UserInfoViewModel
            {
                UserName = HttpContext.User.Identity.Name
            };

            return View(model);
        }
    }
}
