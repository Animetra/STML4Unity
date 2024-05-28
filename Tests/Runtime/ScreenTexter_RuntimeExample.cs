using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScreenTexter_RuntimeExample : MonoBehaviour
{
    // For using in the Unity inspector
    [SerializeField] private ScreenTexterType _type;
    [SerializeField] private string _id;

    public void Awake()
    {
        // Instantiate a STMLReader
        STMLReader reader = new();

        // Read the file. You get back a STMLDocument, which lets you have comfortable access to your text.
        STMLDocument terms = reader.ReadFile("Packages/com.animetra.screentexter/Tests/Runtime/Terms.stml");

        List<STMLDocument> references = new(); 
        references.Add(terms);
        STMLDocument script = reader.ReadFile("Packages/com.animetra.screentexter/Tests/Runtime/ExampleText.stml", references);

        // Optional: Show all dependencies, so you know, which references you need to give
        script.Dependencies.ForEach(x => Debug.Log($"{x.Type}: {x.ID}"));
        //script.SetReferences(references);

        // Will be filled with the text
        string screentext = "";

        if (_type is ScreenTexterType.Term)
        {
            // Choose a section (via id) and prompt a term (via id)
            screentext = script.GetSection("testSection").GetTerm(_id)?.Value;
        }
        else if (_type is ScreenTexterType.Expression)
        {
            // Choose a section (via id) and prompt an expression (via id or index). Use GetFormattedString() to get a rich text formatted text
            screentext = script.GetSection("testSection").GetExpression(_id)?.GetFormattedText();
        }

        // Display it
        GetComponent<TextMeshProUGUI>().text = screentext;
    }
}

// Only for this test case
public enum ScreenTexterType
{
    Term,
    Expression
}