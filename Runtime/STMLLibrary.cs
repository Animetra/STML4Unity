using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

public class STMLLibrary
{
	private readonly Dictionary<string, STMLDocument> _documents;
	public void LoadFolder(string filePath)
	{
		var paths = Directory.GetFiles(filePath);

		STMLReader reader = new();
		foreach (var path in paths)
		{
			STMLDocument doc = reader.ReadFile(path);
            _documents.Add(doc.ID, doc);
		}

		ResolveDependencies();
	}

	public void AddDocument(string path)
	{
        STMLReader reader = new();
        STMLDocument doc = reader.ReadFile(path);
        _documents.Add(doc.ID, doc);

        ResolveDependencies();
    }

	public void RemoveDocument(string id)
	{
		_documents.Remove(id);
	}

	public void Reset()
	{
		_documents.Clear();
	}

	private void ResolveDependencies()
	{
		foreach (var document in _documents.Values)
		{
			var dependencies = document.Dependencies;
			List<STMLDocument> references = new();

			foreach(var dependency in dependencies.Where(x => x.Type == STMLDependencyType.Document))
			{
				references.Add(_documents[dependency.ID]);
			}

			document.SetReferences(references);
		}
	}
}