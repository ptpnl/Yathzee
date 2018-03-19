using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Yahtzee.Controller
{
	public class cXmlParser
	{
		XmlDocument doc;
		XmlNode parrent;
		string filePath;

		public cXmlParser()
		{
			doc = new XmlDocument();
			parrent = null;
			filePath = string.Empty;
		}

		public void SetInputFile(string _file)
		{
			filePath = _file;
			doc.Load(_file);
		}

		#region Read XML
		public XmlNode GetSingleXamlNode(string _nodePath)
		{
			return doc.DocumentElement.SelectSingleNode(_nodePath);
		}

		public string GetXamlNodeValue(XmlNode _node)
		{
			return _node.InnerText;
		}

		public string GetXamlNodeValue(string _nodePath)
		{
			return GetSingleXamlNode(_nodePath).InnerText;
		}

		public string GetXmlNodeAttributeValue(string _nodePath, string _attribute)
		{
			try
			{
				return GetSingleXamlNode(_nodePath).Attributes[_attribute].InnerText;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public string GetXamlNodeAttributeValue(XmlNode _node, string _attribute)
		{
			try
			{
				return _node.Attributes[_attribute].InnerText;
			}
			catch (Exception)
			{
				return null;
			}
		}
		#endregion

		#region Write XML
		public void CreateXmlDeclaration(string _version = "1.0", string _encoding = "utf-8")
		{
			XmlDeclaration declaration = doc.CreateXmlDeclaration(_version, _encoding, null);
			doc.AppendChild(declaration);
		}

		public void CreateParrentNode(string _nodeName)
		{
			XmlNode temp =  doc.CreateNode(XmlNodeType.Element, _nodeName, "");

			AddXmlElement(ref temp);
		}

		public void CloseParrentNode()
		{
			if (parrent.ParentNode != null)
				parrent = parrent.ParentNode;
			else
				parrent = null;
		}

		public void CreateElement(string _name, string _attribute, string _attibuteValue, string _innerText)
		{
			XmlElement temp = doc.CreateElement(_name);

			if (_attribute != null)
			{
				// ensures the tag is a self closing
				if (_innerText == null)
					temp.IsEmpty = true;

				temp.SetAttribute(_attribute, _attibuteValue);
			}

			if (_innerText != null)
				temp.InnerText = _innerText;

			AddXmlElement(ref temp);
		}

		public void CreateSlefClosedElement(string _name, string _attribute, string _attibuteValue)
		{
			XmlElement temp = doc.CreateElement(_name);
			temp.IsEmpty = true;
			if (_attribute != null)
			{
				temp.SetAttribute(_attribute, _attibuteValue);
			}

			AddXmlElement(ref temp);
		}

		private void AddXmlElement(ref XmlNode _node)
		{
			if (parrent == null)
				doc.AppendChild(_node);
			else
				parrent.AppendChild(_node);
		}

		private void AddXmlElement(ref XmlElement _element)
		{
			if (parrent == null)
				doc.AppendChild(_element);
			else
				parrent.AppendChild(_element);
		}

		public void SaveFile()
		{
			doc.Save(filePath);
		}
		#endregion

		#region Edit Xml
		public XmlNode GetNodeByPath(string _path)
		{
			return GetSingleXamlNode(_path);
		}

		public XmlAttribute GetXmlNodeAttribute(string _nodePath, string _attribute)
		{
			try
			{
				return GetSingleXamlNode(_nodePath).Attributes[_attribute];
			}
			catch (Exception)
			{
				return null;
			}
		}
		#endregion
	}
}
