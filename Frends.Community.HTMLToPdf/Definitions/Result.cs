﻿using System.Dynamic;

namespace Frends.Community.HTMLToPdf.Definitions;

/// <summary>
/// Result class for the task.
/// </summary>
public class Result
{
    internal Result(bool success, string filePath = null, byte[] resultBytes = null)
    {
        this.Success = success;
        this.FilePath = filePath;
        this.ResultBytes = resultBytes;
    }

    /// <summary>
    /// Indicates if the operation was successful.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; private set; }

    /// <summary>
    /// Path to the created file.
    /// </summary>
    /// <example>C:\tmp\example_file.pdf</example>
    public string FilePath { get; private set; }

    /// <summary>
    /// Result as byte[].
    /// </summary>
    /// <example>abcdefghijkl123456789</example>
    public byte[] ResultBytes { get; private set; }
}