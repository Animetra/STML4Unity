using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using static Codice.CM.WorkspaceServer.WorkspaceTreeDataStore;
using static System.Collections.Specialized.BitVector32;

public abstract class STMLElement
{
    protected readonly XElement _content;
    protected List<STMLDocument> _references;

    public List<(STMLDependencyType Type, string ID)> Dependencies { get; protected set; }
    public string ID { get; }

    public STMLElement(XElement content, List<STMLDocument> references)
    {
        _content = content;
        _references = references;

        ID = content.Attribute("id")?.Value;

        Dependencies = new();
        
        SaveAsDependency("ref", new[] {"document"});
        SaveAsDependency("expression", new[] { "narrator", "style" });
        SaveAsDependency("style", new[] { "class" });
    }

    private void SaveAsDependency(string name, string[] attributes)
    {
        foreach (var dependency in _content.Descendants(name))
        {
            foreach (var attribute in attributes)
            {
                string value = dependency.Attribute(attribute)?.Value;

                STMLDependencyType type = attribute switch
                {
                    "document" => STMLDependencyType.Document,
                    "narrator" => STMLDependencyType.NarratorStyle,
                    "style" => STMLDependencyType.Style,
                    "class" => STMLDependencyType.Style,
                    _ => STMLDependencyType.Unknown
                };

                if (value is not ("" or null) && !Dependencies.Any(x => x.Type == type && x.ID == value))
                {
                    Dependencies.Add((type, value));
                }
            }
        }
    }

    protected Dictionary<string, string> GetRefReplacements()
    {
        Dictionary<string, string> replacements = new();

        var refs = _content.Elements().Where(x => x.Name == "ref");
        foreach (var reference in refs)
        {
            string document = reference.Attribute("document")?.Value;
            string section = reference.Attribute("section")?.Value;
            string term = reference.Attribute("term")?.Value;

            replacements.Add(reference.ToString(), _references.First(x => x.ID == document).GetSection(section).GetTerm(term).Value);
        }

        return replacements;
    }

    public abstract void SetReferences(List<STMLDocument> references);
}