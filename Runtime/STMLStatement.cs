using PlasticGui.WorkspaceWindow.Diff;
using System.Collections.Generic;
using UnityEngine;

public struct STMLStatement
{
    public string Speaker { get; private set; }
    public List<(string StartTag, string Content, string EndTag)> Sections { get; private set; }

    public STMLStatement(string speaker, string text)
    {
        Speaker = speaker;
        Sections = new();
        int index = 0;
        int startTagStart = 0;
        while (startTagStart != -1)
        {
            startTagStart = text.IndexOf("<", index);
            if (startTagStart != -1)
            {
                if (startTagStart != 0)
                {
                    Sections.Add(("", text.Substring(index, startTagStart - 1), ""));
                }

                int startTagEnd = text.IndexOf(">", startTagStart);
                int endTagStart = text.IndexOf("</", startTagEnd);
                int startTagLength = startTagEnd - startTagStart;
                int endTagEnd = text.LastIndexOf(">", endTagStart);
                int endTagLength = endTagEnd - endTagStart;

                Sections.Add((text.Substring(startTagStart, startTagLength), text.Substring(startTagEnd + 1, endTagStart - startTagEnd - 2), text.Substring(endTagStart, endTagLength)));
                index = endTagEnd + 1;
            }
            else
            {
                Sections.Add(("", text.Substring(index, text.Length - 1), ""));
            }
        }

        Debug.Log(Sections.Count);
        Debug.Log(Sections[0].Content);
        Debug.Log(Sections[1].Content);
        Debug.Log(Sections[2].Content);
    }

    public string GetSubstring(int length)
    {
        string substring = "";
        int restLength = length;
        int sectionNr = 0;

        while (sectionNr < Sections.Count && restLength > 0)
        {
            var section = Sections[sectionNr];
            string content = section.Content;
            string partlyContent = content.Substring(0, restLength);
            string partlyContentWithTags = section.StartTag + partlyContent + section.EndTag;

            Debug.Log(partlyContentWithTags);

            substring += partlyContentWithTags;
            sectionNr++;
            restLength -= content.Length;
        }

        return substring;
    }
}