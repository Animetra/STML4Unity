using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;


public class STMLScript
{
    private XElement[] _statements;
    private XElement _styleSheet;
    private int _currentStatementIndex;
    private float _progressInCharacters;

    public STMLScript(XElement[] statements)
    {
        _statements = statements;
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

    public void UpdateProgress(float deltaTime)
    {
        float speed = _statements[_currentStatementIndex].Attribute("speed") is XAttribute speedAttribute ? float.Parse(speedAttribute.Value) : 14;

        _progressInCharacters += deltaTime * speed;
    }

    public string GetFormattedStatement()
    {
        return _statements[_currentStatementIndex].Value.ToString()[..(int)_progressInCharacters];
    }
}