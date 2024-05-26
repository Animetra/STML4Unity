public struct STMLStatement
{
    public string Speaker { get; private set; }
    public string Text { get; private set; }

    public STMLStatement(string speaker, string text)
    {
        Speaker = speaker;
        Text = text;
    }
}