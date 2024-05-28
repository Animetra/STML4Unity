using System;
using System.IO;
using System.Xml.Linq;
using System.ComponentModel;
using System.Collections.Generic;

/// <summary>
/// Reader for .STML-Files
/// </summary>
public class STMLReader
{
    /// <summary>
    /// Reads .STML-Files and returns a STMLDocument for further use
    /// </summary>
    /// <param name="filePath">The path of the .STML-document to read</param>
    /// <returns>The text of the document as a comfortable to use STMLDocument, structured in sections</returns>
    /// <exception cref="FileNotFoundException">The file does not exist.</exception>
    /// <exception cref="ArgumentException">The file is not in the right format.</exception>
    public STMLDocument ReadFile(string filePath, List<STMLDocument> references = null)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File not found: {filePath}");
        }

        if (Path.GetExtension(filePath) is not ".stml")
        { 
            throw new ArgumentException("File has to be in screentext markup language format (.stml).");
        }

        XDocument stmlDoc;

        using (var reader = new StreamReader(filePath))
        {
            stmlDoc = XDocument.Load(reader);
        }

        return new(stmlDoc.Root, references);
    }
}