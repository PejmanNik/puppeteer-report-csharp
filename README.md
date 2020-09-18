# Puppeteer Report

Puppeteer Report is another library for converting HTML to PDF using puppeteer, adding support of custom header, footer, and pagination.

To support headers or footers, Puppeteer Report creates two PDF files. The first one is the HTML content without header and footer. And the second one is header and footer repeated based upon original content PDF pages' number, then it merges them together.

![image](https://raw.githubusercontent.com/PejmanNik/puppeteer-report/master/.attachment/image1.png)

**NOTE**

This package is a port of Puppeteer Report in NodeJs for C#. Please read the original readme before using it:

https://github.com/PejmanNik/puppeteer-report



## How to Install


```
Install-Package puppeteer-report -Version 1.0.0
```

or

```
dotnet add package puppeteer-report --version 1.0.0
```


To create the right HTML template, please follow the original puppeteer-report instructions:
https://github.com/PejmanNik/puppeteer-report


### Convert To PDF

Call the `Pdf` method of `PuppeteerReport` class. The first argument is the path to HTML in the local file system, The second one is the path to save the final PDF file, and the last one is the puppeteer PDF options object. If you don't specify an `outputFile,` the method returns the byte array.

The full documentation of options object is available in the [puppeteer sharp doc](http://www.puppeteersharp.com/api/PuppeteerSharp.PdfOptions.html)

```c#

using PuppeteerReportCsharp;

.
.
.

var puppeteer = new PuppeteerReport();
await puppeteer.PDF(@"D:\index.html",
    "Result.pdf", new PuppeteerSharp.PdfOptions
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

```

### Run Examples

Clone the repo and run `init-js-dependency.sh` file to init git submodule and fetch the original puppeteer-report library, Then restore the solution and run examples project.