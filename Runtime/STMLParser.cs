using Codice.CM.Common.Serialization.Replication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using UnityEditor.SearchService;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
public class STMLParser
{
    public STMLScript LoadFile(string filePath = "")
    {
        filePath = "C:/Users/PC/Projekte/Libraries/UnityPackages/PackageDevelopment/Packages/ScreenTexter/Runtime/Test/TestText.stml";
        if (Path.GetExtension(filePath) is not ".stml")
        { 
            throw new ArgumentException("file has to be in screentext markup language format (.stml).");
        }
        else
        {
            XDocument stmlDoc;
            using (var reader = new StreamReader(filePath))
            {
                stmlDoc = XDocument.Load(reader);
            }

            XElement header = stmlDoc.Root.Element("header");
            XElement screenText = stmlDoc.Root.Element("screentext");
            return new(screenText.Elements().ToArray());
        }
    }
}