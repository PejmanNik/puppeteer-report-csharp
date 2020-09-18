using PuppeteerReportCsharp;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace examples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var example = "basicWithJs";
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"../../../../js-dependency/examples/${example}/index.html");

            var puppeteer = new PuppeteerReport();
            await puppeteer.PDF(filePath,
                "Result.pdf", new PuppeteerSharp.PdfOptions
                {
                    Format = PuppeteerSharp.Media.PaperFormat.A4,
                    PreferCSSPageSize = true,
                    MarginOptions = new PuppeteerSharp.Media.MarginOptions
                    {
                        Top= "10mm",
                        Left = "10mm",
                        Right = "10mm",
                        Bottom = "10mm"
                    }
                });
        }
    }
}
