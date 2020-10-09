using Newtonsoft.Json.Linq;
using PuppeteerSharp;
using System.IO;
using System.Threading.Tasks;

namespace PuppeteerReportCsharp
{
    public class PuppeteerReport : IPuppeteerReport
    {
        private async Task<string> GetReortCoreJS()
        {
            var assembly = typeof(PuppeteerReport).Assembly;
            var resource = assembly.GetManifestResourceStream("puppeteer_report.bundle.js")!;
            var reader = new StreamReader(resource);

            return await reader.ReadToEndAsync();
        }

        public async Task<byte[]> PDFPage(Page page, PdfOptions? options)
        {
            var coreJS = await GetReortCoreJS();

            var marginTop = options?.MarginOptions?.Top ?? "0px";
            var marginBottom = options?.MarginOptions?.Bottom ?? "0px";

            // inject report coreJS lib to page script
            await page.AddScriptTagAsync(new AddTagOptions { Content = coreJS + "let globalDoc;" });

            var heights = await page.EvaluateFunctionAsync(
                EvaluateFunctionConst.Part1(marginTop, marginBottom)
            );
            var basePdfBuffer = await page.PdfDataAsync(options);

            await page.EvaluateFunctionAsync(EvaluateFunctionConst.Part2, basePdfBuffer);
            var headerPdfBuffer = await page.PdfDataAsync(options);

            var result = await page.EvaluateFunctionAsync<JObject>(
                EvaluateFunctionConst.Part3(heights.Value<string>("headerHeight"), heights.Value<string>("footerHeight")),
                headerPdfBuffer
            );

            // convert serialized JS Uint8Array to C# byte[]
            var resultByte = new byte[result.Count];
            for (int i = 0; i < resultByte.Length; i++)
            {
                resultByte[i] = result.Value<byte>(i.ToString());
            }
            return resultByte;
        }

        public async Task PDFPage(Page page, string outputFile, PdfOptions? options)
        {
            var result = await PDFPage(page, options);
            await File.WriteAllBytesAsync(outputFile, result);
        }

        public async Task<byte[]> PDF(string file, PdfOptions? options)
        {
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Args = new[]
                {
                     "--no-sandbox",
                     "--disable-setuid-sandbox",
                     "--disable-dev-shm-usage",
                }
            });

            var page = await browser.NewPageAsync();
            await page.GoToAsync("file:///" + file);

            var result = await PDFPage(page, options);

            await browser.CloseAsync();

            return result;
        }

        public async Task PDF(string file, string outputFile, PdfOptions? options)
        {
            var result = await PDF(file, options);
            await File.WriteAllBytesAsync(outputFile, result);
        }
    }
}
