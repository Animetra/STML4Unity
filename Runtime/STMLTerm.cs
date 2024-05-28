using System.Collections.Generic;
using System.Xml.Linq;
using static Codice.CM.WorkspaceServer.WorkspaceTreeDataStore;

public class STMLTerm : STMLElement
{
    public string Value { get; private set; }
    public STMLTerm(XElement term, STMLDocument parentDocument) : base(term, parentDocument)
    {
        string content = term.Value.Trim();
        foreach (var replacement in GetRefReplacements())
        {
            content = content.Replace(replacement.Key, replacement.Value);
        }

        Value = content;
    }
}