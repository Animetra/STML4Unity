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
    public Dictionary<string, string> Variables { get; } = new();

    private readonly Dictionary<string, STMLSection> _sections;

    public STMLDocument(XElement content, List<STMLDocument> references = null) : base(content, null)
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

        if (content.Element("resources") is XElement resources)
        {
            foreach(var variable in resources.Elements("variable"))
            {
                string variableString = variable.ToString();
                int tagStart = variableString.IndexOf("<variable", 0);
                int tagEnd = variableString.IndexOf(">", tagStart);
                variableString = variableString
                                    .Remove(tagStart, tagEnd - tagStart + 1)
                                    .Replace("</variable>", "");

                Variables.Add(variable.Attribute("id")?.Value, variableString);
            }
        }

        _sections = content.Element("screentext").Elements().ToDictionary(x => x.Attribute("id")?.Value, x => x.Attribute("isConversation")?.Value == "true" ? new STMLConversation(x, this) : new STMLSection(x, this));

        SetReferences(references);

    }

    /// <summary>
    /// Returns a specific section
    /// </summary>
    /// <param name="id">the id of the desired section</param>
    /// <returns>The section as STMLSection</returns>
    public STMLSection GetSection(string id) => _sections[id];

    public void SetReferences(List<STMLDocument> references)
    {
        _references = references is null ? new() : references;

        _references.Add(this);
    }
}