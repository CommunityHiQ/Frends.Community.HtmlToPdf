namespace Frends.Community.HTMLToPdf.Tests;

using Frends.Community.HTMLToPdf.Definitions;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection.Metadata;

[TestFixture]
internal class UnitTests
{
    private static readonly string _folder = Path.Combine(Path.GetTempPath(), "HtmlToPdfTests");
    private static readonly string _filename = @"foo.pdf";

    private Input _input;
    private Options _options;

    [SetUp]
    public void TestSetup()
    {
        if (!Directory.Exists(_folder))
        {
            Directory.CreateDirectory(_folder);
        }

        _input = new Input
        {
            HtmlContent = @"<html><head></head><body bgcolor='green'>foobar</body></html>",
            OutputPath = Path.Combine(_folder, _filename)
        };

        _options = new Options
        {
            PaperSize = PaperSize.A4,
            PrintLandscape = false,
            UseGrayscale = false
        };
    }
 
    // To examine the test results locally, put TearDown in comments and run tests one by one.
    /*[TearDown]
    public void TestTearDown()
    {
        Directory.Delete(_folder, true);
    }*/

    [Test]
    public void CreatePdfAsFileTest()
    {
        var ret = ConvertHTMLToPdf.Convert(_input, _options, default);

        Assert.IsTrue(ret.Success);
        Assert.AreEqual(Path.Combine(_folder, _filename), ret.FilePath);
        Assert.IsTrue(File.Exists(ret.FilePath));
    }

    [Test]
    public void CreatePdfAsBytesTest()
    {
        _input.OutputPath = null;

        var ret = ConvertHTMLToPdf.Convert(_input, _options, default);

        Assert.IsTrue(ret.Success);
        Assert.IsNotNull(ret.ResultBytes);
        Assert.IsTrue(ret.ResultBytes.Length > 0);
    }

    // For manual testing: examine results in local folder.
    [Test]
    public void PdfOptionsTest()
    {
        _options.UseGrayscale = true;
        _options.PrintLandscape = true;
        _options.PaperSize = PaperSize.Ledger;

        var ret = ConvertHTMLToPdf.Convert(_input, _options, default);

        Assert.IsTrue(ret.Success);
    }

    [Test]
    public void SpecialCharactersTest()
    {
        _input.HtmlContent = @"<html><head><title>Test</title></head><body>ÅÄÖ 1<2 & 4+1>3 %!$</body></html>";

        var ret = ConvertHTMLToPdf.Convert(_input, _options, default);

        Assert.IsTrue(ret.Success);
    }

    [Test]
    public void ThrowDirectoryNotExistsTest()
    {
        _input.OutputPath = @"\not\exists.pdf";

        var ret = ConvertHTMLToPdf.Convert(_input, _options, default);

        Assert.IsFalse(ret.Success);
        Assert.IsNull(ret.FilePath);
        Assert.IsNull(ret.ResultBytes);
        StringAssert.Contains($"The output directory does not exist", ret.Error);
    }

    [Test]
    public void ThrowEmptyHtmlInputTest()
    {
        _input.HtmlContent = null;

        var ret = ConvertHTMLToPdf.Convert(_input, _options, default);

        Assert.IsFalse(ret.Success);
        Assert.IsNull(ret.FilePath);
        Assert.IsNull(ret.ResultBytes);
        Assert.AreEqual(ret.Error, "HTML content cannot be null or empty.");
    }
}