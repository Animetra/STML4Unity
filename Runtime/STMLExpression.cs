public class STMLExpression : STMLElement
{
    public string Narrator { get; private set; }
    public string Style { get; private set; }

    public STMLExpression(XElement expression, STMLDocument parentDocument) : base(expression, parentDocument)
    {
        Narrator = expression.Attribute("narrator")?.Value;
        Style = expression.Attribute("style")?.Value;
    }

    /// <summary>
    /// Get the content as RTF-formatted text
    /// </summary>
    /// <param name="useNarratorStyle">Define if the text should be formatted in narrator style (needs a style sheet class in the narrator's name)</param>
    /// <returns>The content of the expression as RTF-formatted text</returns>
    public string GetFormattedText(bool useNarratorStyle = false)
    {
        string content = _content.ToString();


        int tagStart = content.IndexOf("<expression", 0);
        int tagEnd = content.IndexOf(">", tagStart);
        content = content
                    .Remove(tagStart, tagEnd - tagStart + 1)
                    .Replace("</expression>", "");

        content = content
                    .Trim()
                    .Replace("<style class=", "<style=")
                    .Replace("<color value=", "<color=")
                    .Replace("<size value=", "<size=")
                    .Replace("<material value=", "<material=")
                    .Replace("<quad value=", "<quad");


        foreach (var replacement in GetVariableReplacements())
        {
            content = content.Replace(replacement.Key, replacement.Value);
        }

        foreach (var replacement in GetRefReplacements())
        {
            content = content.Replace(replacement.Key, replacement.Value);
        }

        content = NestInStyle(content, Style);

        if (useNarratorStyle)
        {
            content = NestInStyle(content, Narrator);
        }

        return content;
    }

    // It's a pity this can't be made a private extension class...
    private string NestInStyle(string value, string style)
    {
        return style is "" or null
            ? value
            : $"<style=\"{style}\">{value}</style>";
    }

    /// <summary>
    /// Get the content as plain text
    /// </summary>
    /// <returns>The content of the expression as a plain unformatted string</returns>
    public string GetPlainText()
    {
        return _content.Value.Trim();
    }
}