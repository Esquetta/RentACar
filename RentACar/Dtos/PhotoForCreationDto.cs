using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace RentACar.UI.Dtos
{
    public class PhotoForCreationDto
    {
        public int CarId { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public IFormFile[] File { get; set; }
    }
}
