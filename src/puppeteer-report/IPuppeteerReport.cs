using PuppeteerSharp;
using System.Threading.Tasks;

namespace PuppeteerReportCsharp
{
    public interface IPuppeteerReport
    {
        Task<byte[]> PDF(string file, PdfOptions? options);
        Task PDF(string file, string outputFile, PdfOptions? options);

        Task<byte[]> PDFPage(Page page, PdfOptions? options);
        Task PDFPage(Page page, string outputFile, PdfOptions? options);
    }
}