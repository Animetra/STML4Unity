using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


public class STMLConversation : STMLSection
{
    private readonly STMLExpression[] _expressionsByIndex;

    private int _currentExpressionIndex;

    /// <summary>
    /// The index of the current expression to show
    /// </summary>
    public int CurrentExpressionIndex
    {
        get { return _currentExpressionIndex; }
        private set { _currentExpressionIndex = value; _currentExpressionIndex = Math.Clamp(_currentExpressionIndex, 0, _expressionsByIndex.Length); }
    }

    public STMLConversation(XElement conversation, STMLDocument parentDocument) : base(conversation, parentDocument)
    {
        _expressionsByIndex = conversation.Elements().Where(x => x.Name == "expression").Select(x => new STMLExpression(x, parentDocument)).ToArray();
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
}

