namespace Frends.Community.HTMLToPdf.Definitions;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Options class for the task.
/// </summary>
public class Options
{
    /// <summary>
    /// Defines the paper size for the PDF.
    /// </summary>
    /// <example>A4</example>
    [DefaultValue(PaperSize.A4)]
    public PaperSize PaperSize { get; set; }

    /// <summary>
    /// Defines whether the PDF is created without colors.
    /// </summary>
    /// <example>False</example>
    [DefaultValue(false)]
    public bool UseGrayscale { get; set; }

    /// <summary>
    /// Defines whether the PDF's orientation is set to landscape.
    /// </summary>
    /// <example>False</example>
    [DefaultValue(false)]
    public bool PrintLandscape { get; set; }
}