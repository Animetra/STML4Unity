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
    private readonly STMLExpression[] _expressionsByIndex;
    private readonly Dictionary<string, STMLExpression> _expressionsByID = new();
    private readonly Dictionary<string, STMLTerm> _terms = new();
    private int _currentExpressionIndex;

    /// <summary>
    /// The index of the current expression to show
    /// </summary>
    public int CurrentExpressionIndex
    {
        get { return _currentExpressionIndex; }
        private set { _currentExpressionIndex = value; _currentExpressionIndex = Math.Clamp(_currentExpressionIndex, 0, _expressionsByIndex.Length); }
    }

    public STMLSection(XElement section, STMLDocument parentDocument) : base(section, parentDocument)
    {
        _expressionsByIndex = section.Elements().Where(x => x.Name == "expression").Select(x => new STMLExpression(x, parentDocument)).ToArray();

        foreach(var expression in _expressionsByIndex)
        {
            if (expression.ID is not ("" or null))
            { 
                _expressionsByID.Add(expression.ID, expression);
            }
        }

        _terms = section.Elements().Where(x => x.Name == "term").ToDictionary(x => x.Attribute("id").Value, x => new STMLTerm(x, parentDocument));

        CurrentExpressionIndex = 0;
    }

    /// <summary>
    /// Returns the previous expression
    /// </summary>
    /// <returns>The previous expression</returns>
    public STMLExpression GetPreviousExpression()
    {
        CurrentExpressionIndex--;
        return GetExpression(CurrentExpressionIndex);
    }

    /// <summary>
    /// Returns the next expression
    /// </summary>
    /// <returns>The next expression</returns>
    public STMLExpression GetNextExpression()
    {
        CurrentExpressionIndex++;
        return GetExpression(CurrentExpressionIndex);
    }

    /// <summary>
    /// Returns a specific expression
    /// </summary>
    /// <param name="index">The index of the desired expression</param>
    /// <returns>The expression with index <paramref name="index"/></returns>
    public STMLExpression GetExpression(int index)
    {
        return _expressionsByIndex[index];
    }

    /// <summary>
    /// Returns a specific expression
    /// </summary>
    /// <param name="id">The id of the desired expression</param>
    /// <returns>The expression with the id <paramref name="id"/></returns>
    public STMLExpression GetExpression(string id)
    {
        return _expressionsByID[id];
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