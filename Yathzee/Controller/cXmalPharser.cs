using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Yahtzee.Controller
{
	public class cXmalPharser
	{
		XmlDocument xaml;

		public cXmalPharser()
		{
			xaml = new XmlDocument();
		}

		public void SetInputFile(string _file)
		{
			xaml.Load(_file);
		}

		public XmlNode GetSingleXamlNode(string _nodePath)
		{
			return xaml.DocumentElement.SelectSingleNode(_nodePath);
		}

		public string GetXamlNodeValue(XmlNode _node)
		{
			return _node.InnerText;
		}

		public string GetXamlNodeValue(string _nodePath)
		{
			return GetSingleXamlNode(_nodePath).InnerText;
		}

		public string GetXamlNodeAttributeValue(string _nodePath, string _attribute)
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
	}
}
