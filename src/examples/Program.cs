using Konsole;
using PuppeteerReportCsharp;
using PuppeteerSharp;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace examples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var example = "basicWithJs";
            Console.WriteLine($"run example '{example}'");

            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..", "..", "..", "..", "js-dependency", "examples", example, "index.html");

            var fether = new BrowserFetcher();

            var pb = new ProgressBar(100);
            void downloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
            {
                pb.Refresh(e.ProgressPercentage, $"downloading Puppeteer {BrowserFetcher.DefaultChromiumRevision}");
            }
            fether.DownloadProgressChanged += downloadProgressChanged;

            await fether.DownloadAsync();
            pb.Refresh(100, "finished");

            Console.WriteLine($"convert html to pdf");
            var puppeteer = new PuppeteerReport();
            await puppeteer.PDF(filePath,
                "Result.pdf", new PdfOptions
                {
                    Format = PuppeteerSharp.Media.PaperFormat.A4,
                    PreferCSSPageSize = true,
                    MarginOptions = new PuppeteerSharp.Media.MarginOptions
                    {
                        Top = "10mm",
                        Left = "10mm",
                        Right = "10mm",
                        Bottom = "10mm"
                    }
                });

            Console.WriteLine("finished");

            string resultPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Report.pdf");
            Console.WriteLine(resultPath);
            Console.ReadLine();
        }
    }
}
