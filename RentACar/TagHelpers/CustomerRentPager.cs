using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.UI.TagHelpers
{
    [HtmlTargetElement("RentPager")]
    public class CustomerRentPager : TagHelper
    {
        [HtmlAttributeName("rentpager-controller")]
        public string Controller { get; set; }
        [HtmlAttributeName("rentpager-Action")]
        public string Action { get; set; }
        [HtmlAttributeName("rentpager-CurrentPage")]
        public int CurrentPage { get; set; }
        [HtmlAttributeName("rentpager-PageCount")]
        public int PageCount { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<ul class='pagination justify-content-center'>");
            for (int i = 1; i <= PageCount; i++)
            {

                stringBuilder.AppendFormat("<li class='page-item {0}'>", i == CurrentPage ? "active" : "");
                stringBuilder.AppendFormat("<a class='page-link' href='/{0}/{1}?page={2}'>{3}</a>", Controller, Action, i, i);
                stringBuilder.AppendFormat("</li>");
            }
            output.Content.SetHtmlContent(stringBuilder.ToString());
            base.Process(context, output);
        }
    }
}
