using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	[NullableContext(1)]
	[Nullable(0)]
	internal class XDeclarationWrapper : XObjectWrapper, IXmlDeclaration, IXmlNode
	{
		[CompilerGenerated]
		private readonly XDeclaration _003CDeclaration_003Ek__BackingField;

		internal XDeclaration Declaration
		{
			[CompilerGenerated]
			get
			{
				return _003CDeclaration_003Ek__BackingField;
			}
		}

		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.XmlDeclaration;
			}
		}

		public string Version
		{
			get
			{
				return Declaration.Version;
			}
		}

		public string Encoding
		{
			get
			{
				return Declaration.Encoding;
			}
			set
			{
				Declaration.Encoding = value;
			}
		}

		public string Standalone
		{
			get
			{
				return Declaration.Standalone;
			}
			set
			{
				Declaration.Standalone = value;
			}
		}

		public XDeclarationWrapper(XDeclaration declaration)
			: base(null)
		{
			_003CDeclaration_003Ek__BackingField = declaration;
		}
	}
}
