using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;


public class STMLScript
{
    private STMLStatement[] _statements;
    private XElement _styleSheet;
    private int _currentStatementIndex;
    private float _progressInCharacters;

    public STMLScript(XElement[] statements)
    {
        _statements = statements.Select(x => new STMLStatement(x.Attribute("speaker").Value, x.Value)).ToArray();
    }

    public void SetStyleSheet(XElement styleSheet)
    {
        _styleSheet = styleSheet;
    }


    public void StartNextStatement()
    {
        _currentStatementIndex++;
        StartStatement();
    }

    public void StartPreviousStatement()
    {
        _currentStatementIndex--;
        StartStatement();
    }

    public void StartStatement(int index)
    {
        _currentStatementIndex = index;
        StartStatement();
    }

    public void StartStatement()
    {
        _progressInCharacters = 0;
    }

    public string GetFormattedStatement(float deltaTime)
    {
        float speed = /*_statements[_currentStatementIndex].Attribute("speed") is XAttribute speedAttribute ? float.Parse(speedAttribute.Value) : */10;

        _progressInCharacters += deltaTime * speed;
        return _statements[_currentStatementIndex].GetSubstring((int)_progressInCharacters);
    }
}