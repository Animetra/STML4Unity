using Codice.Client.BaseCommands.BranchExplorer;
using Codice.CM.Common.Serialization.Replication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEditor.SearchService;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
public class STMLParser
{
    public STMLScript LoadFile(string filePath)
    {
        if (Path.GetExtension(filePath) is not ".stml")
        { 
            throw new ArgumentException("file has to be in screentext markup language format (.stml).");
        }
        else
        {
            string document;

            using (var reader = new StreamReader(filePath))
            {
                document = reader.ReadToEnd();
            }

            string statementStartTag = "<statement";
            string statementEndTag = "</statement";

            int index = 0;
            int foundStatement = 0;

            while (foundStatement != -1 && index < document.Length)
            {
                foundStatement = document.IndexOf(statementStartTag, index);

                if (foundStatement != -1)
                {
                    int statementContentStart = document.IndexOf(">", foundStatement) + 1;
                    int statementContentEnd = document.IndexOf(statementEndTag, statementContentStart) - 1;
                    int length = statementContentEnd - statementContentStart;

                    string statementContent = document
                                                .Substring(statementContentStart, length)
                                                .Replace("<", "&lt;")
                                                .Replace(">", "&gt;");
                    document = document
                                .Remove(statementContentStart, length)
                                .Insert(statementContentStart, statementContent);

                    index = statementContentStart + statementContent.Length + 12;
                }
            }

            XDocument stmlDoc = XDocument.Parse(document);
            XElement header = stmlDoc.Root.Element("header");
            XElement screenText = stmlDoc.Root.Element("screentext");
            return new(screenText.Elements().ToArray());
        }
    }
}