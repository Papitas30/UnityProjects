using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	[NullableContext(2)]
	[Nullable(0)]
	internal class XObjectWrapper : IXmlNode
	{
		private readonly XObject _xmlObject;

		public object WrappedNode
		{
			get
			{
				return _xmlObject;
			}
		}

		public virtual XmlNodeType NodeType
		{
			get
			{
				XObject xmlObject = _xmlObject;
				if (xmlObject == null)
				{
					return XmlNodeType.None;
				}
				return xmlObject.NodeType;
			}
		}

		public virtual string LocalName
		{
			get
			{
				return null;
			}
		}

		[Nullable(1)]
		public virtual List<IXmlNode> ChildNodes
		{
			[NullableContext(1)]
			get
			{
				return XmlNodeConverter.EmptyChildNodes;
			}
		}

		[Nullable(1)]
		public virtual List<IXmlNode> Attributes
		{
			[NullableContext(1)]
			get
			{
				return XmlNodeConverter.EmptyChildNodes;
			}
		}

		public virtual IXmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		public virtual string Value
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public virtual string NamespaceUri
		{
			get
			{
				return null;
			}
		}

		public XObjectWrapper(XObject xmlObject)
		{
			_xmlObject = xmlObject;
		}

		[NullableContext(1)]
		public virtual IXmlNode AppendChild(IXmlNode newChild)
		{
			throw new InvalidOperationException();
		}
	}
}
