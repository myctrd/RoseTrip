using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;

public class DataManager : MonoBehaviour {

    public static DataManager m_instance;

    void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this;
        }
    }

    public const string dataPath = "/Resources/Xmls/";
    

    public void DeleteAll()
    {
        
    }

    void DeleteXML(string fileName)
    {
        if (File.Exists(Application.dataPath + dataPath + fileName))
        {
            File.Delete(Application.dataPath + dataPath + fileName);
        }
    }


    public XmlDocument CreateXML()
    {
        XmlDocument xml = new XmlDocument();
        xml.AppendChild(xml.CreateXmlDeclaration("1.0", "UTF-8", null));
        xml.AppendChild(xml.CreateElement("Root"));
        return xml;
    }

    void AddNodeToXML(XmlDocument xml, string title_1, string value_1, string title_2, string value_2)
    {
        XmlNode root = xml.SelectSingleNode("Root");

        foreach (XmlElement node in root)
        {
            if(node.ChildNodes[0].InnerText == value_1)
            {
                node.ChildNodes[1].InnerText = value_2;
                return;
            }
        }

        XmlElement element = xml.CreateElement("Node");
        element.SetAttribute("Type", "string");

        XmlElement titleElelment = xml.CreateElement(title_1);
        titleElelment.InnerText = value_1;

        XmlElement infoElement = xml.CreateElement(title_2);
        infoElement.InnerText = value_2;

        element.AppendChild(titleElelment);
        element.AppendChild(infoElement);
        root.AppendChild(element);
    }

    void UpdateNodeToXML(string fileName)
    {
        string filepath = Application.dataPath + dataPath + fileName + ".XML";
        if (File.Exists(filepath))
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(filepath);
            XmlNodeList nodeList = xmldoc.SelectSingleNode("Root").ChildNodes;
            foreach (XmlElement xe in nodeList)
            {
                if (xe.GetAttribute("Type") == "string")
                {
                    xe.SetAttribute("type", "text");
                    foreach (XmlElement xelement in xe.ChildNodes)
                    {
                        if (xelement.Name == "TitleNode")
                        {
                            xelement.InnerText = fileName;
                        }
                    }
                    break;
                }
            }
            xmldoc.Save(filepath);
        }
    }


    void SaveXML(XmlDocument xml, string fileName)
    {
        xml.Save(Application.dataPath + dataPath + fileName + ".XML");
    }
}
