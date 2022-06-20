using Microsoft.AspNetCore.Http;
using RentACar.Entities.Concrete;
using RentACar.UI.Services.Abstract;
using System.Text.Json;

namespace RentACar.UI.Services.Concrete
{
    public class CarCookieeService : ICarCookieeService
    {
        private IHttpContextAccessor httpContext;
        public CarCookieeService(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
        }
        public Car GetObject(string key)
        {
            var item = httpContext.HttpContext.Request.Cookies[key];
            var result = JsonSerializer.Deserialize<Car>(item);
            return result;
        }

        public void SetValue(string key, Car value)
        {
            var item = JsonSerializer.Serialize<Car>(value);
            httpContext.HttpContext.Response.Cookies.Append(key, item);
        }
    }
}
