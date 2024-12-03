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
    /// Optional: if set, the output will be to the given file, otherwise the output will be byte array.
    /// </summary>
    /// <example>C:\temp</example>
    [DisplayFormat(DataFormatString = "Text")]
    [DefaultValue(@"C:\temp\foo.pdf")]
    public string OutputPath { get; set; }

}