using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace RentACar.UI.TagHelpers
{
    [HtmlTargetElement("page-list")]
    public class PaggingTagHelper : TagHelper
    {
        [HtmlAttributeName("page-size")]
        public int PageSize { get; set; }
        [HtmlAttributeName("page-count")]
        public int PageCount { get; set; }
        [HtmlAttributeName("page-currentbrand")]
        public string CurrentBrand { get; set; }
        [HtmlAttributeName("page-currentpage")]
        public int CurrentPage { get; set; }
        [HtmlAttributeName("page-Controller")]
        public string Controller { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<ul class='pagination justify-content-center'>");
            for (int i = 1; i <= PageCount; i++)
            {

                stringBuilder.AppendFormat("<li class='page-item {0}'>", i == CurrentPage ? "active" : "");
                stringBuilder.AppendFormat("<a class='page-link' href='/{0}?page={1}&Brand={2}'>{3}</a>", Controller, i, CurrentBrand, i);
                stringBuilder.AppendFormat("</li>");
            }
            output.Content.SetHtmlContent(stringBuilder.ToString());
            base.Process(context, output);
        }


    }
}
