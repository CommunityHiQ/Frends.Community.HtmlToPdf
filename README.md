# Frends.Community.HtmlToPdf

Frends Community Task for converting HTML to PDF

[![Actions Status](https://github.com/CommunityHiQ/Frends.Community.HtmlToPdf/workflows/PackAndPushAfterMerge/badge.svg)](https://github.com/CommunityHiQ/Frends.Community.HtmlToPdf/actions) ![MyGet](https://img.shields.io/myget/frends-community/v/Frends.Community.HTMLToPdf) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) 

- [Installing](#installing)
- [Tasks](#tasks)
     - [HtmlToPdf](#htmltopdf)
- [Building](#building)
- [Contributing](#contributing)
- [Change Log](#change-log)

# Installing

You can install the Task via frends UI Task View or you can find the NuGet package from the following NuGet feed
https://www.myget.org/F/frends-community/api/v3/index.json and in Gallery view in MyGet https://www.myget.org/feed/frends-community/package/nuget/Frends.Community.HTMLToPdf

# Tasks

## HtmlToPdf

Converts HTML to PDF.

### Properties

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| HtmlContent | `string` | The input HTML to be converted | `<html>content</html>` |
| OutputPath | `string` | Optional: if set, the output will be to the given file, otherwise the output will be byte array. | `C:\temp\foo.pdf` |

### Options

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| PaperSize | `PaperSize` | Paper size for the PDF. | `A4` |
| UseGrayScale | `bool` | Defines whether the PDF is created without colors. | `false` |
| PrintLandscape | `bool` | Defines whether the PDF's orientation is set to landscape. | `false` |

### Returns

A result object with parameters.

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| Success | `bool` | Indicates if the operation was successful. | `true` |
| FilePath | `string` | Path to the created file, if path given as input. | `C:\tmp\example_file.pdf` |
| ResultBytes | `byte[]` | Result as byte[], if path not given as input. | `abcdefghijkl123456789` |
| Error | `string` | Possible error message. | `HTML content cannot be null or empty.` |

# Building

Clone a copy of the repository

`git clone https://github.com/CommunityHiQ/Frends.Community.HtmlToPdf.git`

Rebuild the project

`dotnet build`

Run tests

`dotnet test`

Create a NuGet package

`dotnet pack --configuration Release`

# Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

1. Fork the repository on GitHub
2. Clone the project to your own machine
3. Commit changes to your own branch
4. Push your work back up to your fork
5. Submit a Pull request so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

# Third party licenses

StyleCop.Analyzer version (unmodified version 1.1.118) used to analyze code uses Apache-2.0 license, full text and source code can be found in https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/README.md

# Change Log

| Version | Changes |
| ------- | ------- |
| 1.1.0   | Initial create |
