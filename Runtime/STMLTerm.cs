using System.Collections.Generic;
using System.Xml.Linq;
using static Codice.CM.WorkspaceServer.WorkspaceTreeDataStore;

public class STMLTerm : STMLElement
{
    public string Value { get; private set; }
    public STMLTerm(XElement term, List<STMLDocument> references = null) : base(term, references)
    {
        string content = term.Value.Trim();
        foreach (var replacement in GetRefReplacements())
        {
            content = content.Replace(replacement.Key, replacement.Value);
        }

        Value = content;
    }

    public override void SetReferences(List<STMLDocument> references)
    {
        _references = references;
    }
}