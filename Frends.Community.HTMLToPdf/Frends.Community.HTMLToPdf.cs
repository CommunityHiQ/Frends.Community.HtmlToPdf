namespace Frends.Community.HTMLToPdf;

using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using WkHtmlToPdfDotNet;
using Frends.Community.HTMLToPdf.Definitions;
using Microsoft.Extensions.DependencyInjection;
using WkHtmlToPdfDotNet.Contracts;

/// <summary>
/// Main class of the Task.
/// </summary>
public static class ConvertHTMLToPdf
{
    private static readonly Lazy<IConverter> _converter = new Lazy<IConverter>(() =>
    {
        var serviceProvider = new ServiceCollection()
                                    .AddSingleton<IConverter>(new SynchronizedConverter(new PdfTools()))
                                    .BuildServiceProvider();

        return serviceProvider.GetService<IConverter>();
    });

    /// <summary>
    /// Create a PDF document from given HTML.
    /// [Documentation](https://github.com/CommunityHiQ/Frends.Community.HtmlToPdf).
    /// </summary>
    /// <param name="input">Input parameters.</param>
    /// <param name="options">Options parameters.</param>
    /// <param name="cancellationToken">Cancellation token given by Frends.</param>
    /// <returns>Object { bool Success, string FilePath, byte[] ResultBytes }</returns>
    public static Result Convert([PropertyTab] Input input, [PropertyTab] Options options, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(input.HtmlContent))
        {
            throw new ArgumentException("HTML content cannot be null or empty.");
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
                throw new ArgumentException($"The output directory does not exist: {dir}");
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

        try
        {
            var conversionResult = _converter.Value.Convert(doc);

            if (outputToByteArray)
            {
                return new Result(true, null, conversionResult);
            }

            return new Result(true, input.OutputPath, null);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error during document conversion.", ex);
        }
    }
}