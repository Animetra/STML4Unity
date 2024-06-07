using STML.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public static class STMLManager
{
    public static string STMLProjectFolder { get; set; } = $"Assets/{Filenames.ModuleName}";
    public static STMLProject ActiveProject { get; set; }
    public static STMLLibrary ActiveLibrary { get; set; }

    public static void Initialize(string languageCode)
    {
        STMLReader reader = new STMLReader();

        ActiveProject = reader.ReadProject(STMLProjectFolder);
        LoadLibrary(languageCode);
        ActiveLibrary.ResolveAndFormatContent(ContentFormat.Unity);
    }

    public static void LoadLibrary(string languageCode)
    {

        List<STMLLibrary> libraries = new();
        libraries = ActiveProject.Libraries.Where(x => x.Language == languageCode).ToList();
        if (libraries.Count != 1)
        {
            throw new ArgumentException("No library with specified language found");
        }

        ActiveLibrary = libraries.First();
    }

    public static string GetText(string id)
    {
        STMLElement text = ActiveLibrary.GetDescendant(x => x.Header.ID == id);

        if (text is STMLExpression expression)
        {
            return expression.Content.Formatted;
        }
        else if (text is STMLTerm term)
        {
            return term.Content.Plain;
        }
        else
        {
            return $"{{text {id} not found.}}";
        }
    }
}