using System.IO;
using UnityEngine;
using TMPro;

public class Tester : MonoBehaviour
{
    [SerializeField] private string _filePath;
    STMLParser parser = new();
    STMLScript script;
    public TextMeshProUGUI text;
    
    public void Awake()
    {
        script = parser.LoadFile(_filePath);
        script.StartStatement(0);
    }

    public void Update()
    {
        string screentext = script.GetFormattedStatement(Time.deltaTime);
        text.text = screentext;
        //Debug.Log(screentext);
    }


}