using System.Xml.Linq;
using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Codice.CM.WorkspaceServer.WorkspaceTreeDataStore;

/// <summary>
/// A Section of a STMLDocument
/// </summary>
public class STMLSection : STMLElement
{

    private readonly Dictionary<string, STMLExpression> _expressions = new();
    private readonly Dictionary<string, STMLTerm> _terms = new();

    public STMLSection(XElement section, STMLDocument parentDocument) : base(section, parentDocument)
    {
        _expressions = section.Elements().Where(x => x.Name == "expression").ToDictionary(x => x.Attribute("id").Value, x => new STMLExpression(x, parentDocument));

        _terms = section.Elements().Where(x => x.Name == "term").ToDictionary(x => x.Attribute("id").Value, x => new STMLTerm(x, parentDocument));
    }


    /// <summary>
    /// Returns a specific expression
    /// </summary>
    /// <param name="id">The id of the desired expression</param>
    /// <returns>The expression with the id <paramref name="id"/></returns>
    public STMLExpression GetExpression(string id)
    {
        return _expressions[id];
    }

    /// <summary>
    /// Returns a specific term
    /// </summary>
    /// <param name="id">The id of the desired term</param>
    /// <returns>The term with the id <paramref name="id"/></returns>
    public STMLTerm GetTerm(string id)
    {
        return _terms[id];
    }
}