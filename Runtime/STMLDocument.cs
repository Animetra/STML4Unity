using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;

/// <summary>
/// A STML-document as easy-to-use object
/// </summary>
public class STMLDocument : STMLElement
{
    public STMLHeader Header { get; }

    private readonly Dictionary<string, STMLSection> _sections;

    public STMLDocument(XElement content, List<STMLDocument> references = null) : base(content, references)
    {
        XElement header = _content.Element("header");
        Header = new STMLHeader
            (
                title: header.Element("title").Value,
                description: header.Element("description").Value,
                languageCode: header.Element("language").Value,
                author: header.Element("author").Value,
                version: header.Element("version").Value
            );

        _sections = content.Element("screentext").Elements().ToDictionary(x => x.Attribute("id").Value, x => new STMLSection(x, references));
    }

    /// <summary>
    /// Returns a specific section
    /// </summary>
    /// <param name="id">the id of the desired section</param>
    /// <returns>The section as STMLSection</returns>
    public STMLSection GetSection(string id) => _sections[id];

    public override void SetReferences(List<STMLDocument> references)
    {
        _references = references;
        foreach (var section in _sections.Values)
        {
            section.SetReferences(references);
        }
    }
}