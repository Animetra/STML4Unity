using System.Data;

public readonly struct STMLHeader
{
    public string Title { get; }
    public string Description { get; }
    public string LanguageCode { get; } // ISO-639-1, two letter language code (e.g. "en" or "de")
    public string Author { get; }
    public string Version { get; }

    public STMLHeader(string title, string description, string languageCode, string author, string version)
    {
        Title = title;
        Description = description;
        LanguageCode = languageCode;
        Author = author;
        Version = version;
    }
}