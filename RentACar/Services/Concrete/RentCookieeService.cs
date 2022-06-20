using Microsoft.AspNetCore.Http;
using RentACar.Entities.Concrete;
using RentACar.UI.Services.Abstract;
using System.Text.Json;

namespace RentACar.UI.Services.Concrete
{
    public class RentCookieeService : IRentCookieeService
    {
        private IHttpContextAccessor httpContext;
        public RentCookieeService(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
        }

        public Rent GetObject(string key)
        {
            var item = httpContext.HttpContext.Request.Cookies[key];
            var result = JsonSerializer.Deserialize<Rent>(item);
            return result;
        }

        public string GetUserName()
        {
            var value = httpContext.HttpContext.User.Identity.Name;
            return value;
        }

        public void SetValue(string key, Rent value)
        {
            var item = JsonSerializer.Serialize<Rent>(value);
            httpContext.HttpContext.Response.Cookies.Append(key, item, new CookieOptions { HttpOnly = true, SameSite = SameSiteMode.Strict });
        }


    }
}
