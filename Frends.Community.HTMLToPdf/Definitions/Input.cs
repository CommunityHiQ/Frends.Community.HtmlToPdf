namespace Frends.Community.HTMLToPdf.Definitions;

using WkHtmlToPdfDotNet;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Input class for the task.
/// </summary>
public class Input
{
    /// <summary>
    /// The input HTML to be converted.
    /// </summary>
    /// <example>&lt;html&gt;content&lt;/html&gt;</example>
    [DisplayFormat(DataFormatString = "Text")]
    [DefaultValue("<html>content</html>")]
    public string HtmlContent { get; set; }

    /// <summary>
    /// Specifies the type of output.
    /// </summary>
    /// <example>FilePath</example>
    [DefaultValue(OutputType.Bytes)]
    public OutputType OutputType { get; set; }

    /// <summary>
    /// Output file path.
    /// </summary>
    /// <example>C:\temp\foo.pdf"</example>
    [DisplayFormat(DataFormatString = "Text")]
    [DefaultValue(@"C:\temp\foo.pdf")]
    [UIHint(nameof(OutputType), "", OutputType.File)]
    public string OutputPath { get; set; }

}