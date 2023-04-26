using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	[NullableContext(1)]
	[Nullable(0)]
	internal class XmlDocumentTypeWrapper : XmlNodeWrapper, IXmlDocumentType, IXmlNode
	{
		private readonly XmlDocumentType _documentType;

		public string Name
		{
			get
			{
				return _documentType.Name;
			}
		}

		public string System
		{
			get
			{
				return _documentType.SystemId;
			}
		}

		public string Public
		{
			get
			{
				return _documentType.PublicId;
			}
		}

		public string InternalSubset
		{
			get
			{
				return _documentType.InternalSubset;
			}
		}

		[Nullable(2)]
		public override string LocalName
		{
			[NullableContext(2)]
			get
			{
				return "DOCTYPE";
			}
		}

		public XmlDocumentTypeWrapper(XmlDocumentType documentType)
			: base(documentType)
		{
			_documentType = documentType;
		}
	}
}
