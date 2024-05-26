using System.IO;
using UnityEngine;
using TMPro;

public class Tester : MonoBehaviour
{
    STMLParser parser = new();
    STMLScript script;
    public TextMeshProUGUI text;
    
    public void Awake()
    {
        script = parser.LoadFile();
        script.StartStatement(0);
    }

    public void Update()
    {
        script.UpdateProgress(Time.deltaTime);
        
        text.text = script.GetFormattedStatement();
    }


}