namespace Frends.Community.HTMLToPdf;

using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using WkHtmlToPdfDotNet;
using Frends.Community.HTMLToPdf.Definitions;

/// <summary>
/// Main class of the Task.
/// </summary>
public static class ConvertHTMLToPdf
{
    /// <summary>
    /// Create a PDF document from given HTML. The result is returned as a byte array or written into a file.
    /// If given directory does not exist, an error message is returned. If the file already exists, it will be overwritten.
    /// [Documentation](https://github.com/CommunityHiQ/Frends.Community.HtmlToPdf).
    /// </summary>
    /// <param name="input">Input parameters.</param>
    /// <param name="options">Options parameters.</param>
    /// <param name="cancellationToken">Cancellation token given by Frends.</param>
    /// <returns>Object { bool Success, string FilePath, byte[] ResultBytes, string Error }</returns>
    public static Result Convert([PropertyTab] Input input, [PropertyTab] Options options, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(input.HtmlContent))
        {
            return new Result(false, null, null, "HTML content cannot be null or empty.");
        }

        var contentToWrite = input.HtmlContent;

        bool outputToByteArray = string.IsNullOrWhiteSpace(input.OutputPath);

        GlobalSettings settings = new GlobalSettings();
        settings.ColorMode = options.UseGrayscale ? ColorMode.Grayscale : ColorMode.Color;
        settings.Orientation = options.PrintLandscape ? Orientation.Landscape : Orientation.Portrait;
        settings.PaperSize = (PaperKind)Enum.Parse(typeof(PaperKind), options.PaperSize.ToString());

        if (!outputToByteArray)
        {
            string dir = Path.GetDirectoryName(input.OutputPath);
            if (!Directory.Exists(dir))
            {
                return new Result(false, null, null, $"The output directory does not exist: {dir}");
            }

            settings.Out = input.OutputPath;
        }

        var doc = new HtmlToPdfDocument()
        {
            GlobalSettings = settings,
            Objects =
            {
                new ObjectSettings()
                {
                    HtmlContent = contentToWrite,
                    WebSettings = { DefaultEncoding = "utf-8" },
                },
            },
        };

        var converter = new SynchronizedConverter(new PdfTools());
        try
        {
            var conversionResult = converter.Convert(doc);

            if (outputToByteArray)
            {
                return new Result(true, null, conversionResult);
            }

            return new Result(true, input.OutputPath, null);
        }
        catch (Exception ex)
        {
            return new Result(false, null, null, ex.Message);
        }
        finally
        {
            if (converter != null)
            {
                converter.Dispose();
            }
        }
    }
    
}