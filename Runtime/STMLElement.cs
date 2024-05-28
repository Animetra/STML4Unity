
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Animations;

public abstract class STMLElement
{
    protected readonly XElement _content;
    protected readonly STMLDocument _parent;
    protected List<STMLDocument> _references = null;
    public List<STMLDocument> References
    {
        get
        {
            _references = _parent?.References ?? _references;
            return _references;
        }
    }

    public List<(STMLDependencyType Type, string ID)> Dependencies { get; protected set; }
    public string ID { get; }

    public STMLElement(XElement content, STMLDocument parentDocument)
    {
        _content = content;
        _parent = parentDocument;

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

        var refs = _content.Descendants("ref");

        foreach (var reference in refs)
        {
            string documentID = reference.Attribute("document")?.Value;
            if (documentID == "this")
            {
                documentID = _parent.ID;
                Debug.Log(documentID);
            }

            string sectionID = reference.Attribute("section")?.Value;
            if (sectionID == "this")
            {
                sectionID = _parent.GetSection(_content.AncestorsAndSelf("section").First().Attribute("id").Value).ID;
                Debug.Log(sectionID);
            }

            string term = reference.Attribute("term")?.Value;

            // do NOT implement expressions here.. but..:
            // TODO: performance, refactorize
            replacements.Add(reference.ToString(), References.First(x => x.ID == documentID).GetSection(sectionID).GetTerm(term).Value);
        }

        return replacements;
    }

    protected Dictionary<string, string> GetVariableReplacements()
    {
        Dictionary<string, string> replacements = new();

        var variablesToReplace = _content.Descendants("var");

        foreach (var variable in variablesToReplace)
        {
            string variableID = variable.Attribute("resource")?.Value;

            replacements.Add(variable.ToString(), _parent.Variables[variableID]);
        }

        return replacements;
    }
}