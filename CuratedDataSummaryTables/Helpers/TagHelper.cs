using Microsoft.AspNetCore.Razor.TagHelpers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CuratedDataSummaryTables.Helpers
{
    [HtmlTargetElement("html-embed", Attributes = "file-path")]
    public class HtmlEmbedTagHelper : TagHelper
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public HtmlEmbedTagHelper(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HtmlAttributeName("file-path")]
        public string FilePath { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Resolve the full file path
            var fullPath = Path.Combine(_hostingEnvironment.WebRootPath, FilePath.TrimStart('~', '/'));

            // Read the HTML content from the file
            var htmlContent = File.Exists(fullPath) ? File.ReadAllText(fullPath) : "File not found.";

            // Output the HTML content
            output.TagName = null; // Remove the <html-embed> tag
            output.Content.SetHtmlContent(htmlContent);
        }
    }
}
