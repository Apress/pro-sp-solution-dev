using System;

namespace FPRPC
{
	/// <summary>
	/// Contains document information used when putting or getting a document.
	/// </summary>
	public class DocumentInfo
	{
		#region constants
		private const string STR_CRLF = "\r\n";
		private const string STR_NEWLINE = "\n";
		private const string STR_SEMICOLON = ";";
		private const string STR_VLINE = "|";
		#endregion

		#region private members
		private  string _destinationFileName;
		private DocumentPropertyCollection _properties = new DocumentPropertyCollection();
		#endregion

		#region properties
		
		public DocumentPropertyCollection Properties 
		{
			get 
			{
				return _properties;
			}
		}

		public string DestinationFileName
		{
			get
			{
				return _destinationFileName;
			}
			set
			{
				_destinationFileName = value;
			}
		}

		public string ModifiedBy
		{
			get
			{
				return (string) GetPropertyValue("vti_modifiedby", string.Empty);
			}
			set
			{
				SetPropertyValue("vti_modifiedby", value);
			}
		}

		public DateTime ModifiedDate
		{
			get
			{
				return (DateTime) GetPropertyValue("vti_timelastmodified", null);
			}
			set
			{
				SetPropertyValue("vti_timelastmodified", value);
			}
		}

		public string Title
		{
			get
			{
				return (string) GetPropertyValue("vti_title", string.Empty);
			}
			set
			{
				SetPropertyValue("vti_title", value);
			}
		}
		#endregion

		#region ..ctors
		public DocumentInfo() 
		{
		}
		#endregion

		#region public methods
		/// <summary>
		/// Writes document header data to the data stream.
		/// </summary>
		/// <param name="tw"></param>
		public void WriteDocumentData(System.IO.TextWriter tw) 
		{
			tw.Write("&document=[document_name=" + DestinationFileName + STR_SEMICOLON);
			WriteMetaInfo(tw);
			tw.Write("]" + STR_NEWLINE);
		}

		#endregion

		#region non-public methods

		/// <summary>
		/// Builds the the meta info list used for setting properties in various
		/// FP RPC calls.
		/// </summary>
		/// <remarks>
		/// Properties are generally in the form:
		/// <code>
		///		[Myprop1|
		/// </code>
		/// </remarks>
		/// <param name="encode">specifies to URL encode the individual values.</param>
		/// <returns></returns>
		internal string GetMetaInfoList(bool encode) 
		{
			bool first = true;
			System.Text.StringBuilder meta_info = new System.Text.StringBuilder();
			
			meta_info.Append("[");
			foreach(DocumentProperty prop in _properties) 
			{
				if (first) 			
					first = false;
				else
					meta_info.Append(";");
				meta_info.Append(prop.GetPropertyFP(encode));
			}		
			meta_info.Append("]");
			return meta_info.ToString();
		}

		internal string GetMetaInfoList() 
		{
			return GetMetaInfoList(true);
		}

		protected void WriteMetaInfo(System.IO.TextWriter tw)
		{
			tw.Write("meta_info=" + GetMetaInfoList());
		}


		private object GetPropertyValue(string propertyName, object defaultValue) 
		{
			DocumentProperty prop = _properties[propertyName];
			if (null == prop) 
			{
				return  defaultValue;
			}
			else
			{
				return prop.PropertyValue;
			}
		}

		private void SetPropertyValue(string propertyName, string propertyValue) 
		{
			DocumentProperty prop = _properties[propertyName];
			if (null == prop) 
			{
				_properties.Add(new DocumentProperty(propertyName, propertyValue));
			}
			else 
			{
				prop.PropertyValue = propertyValue;
			}
		}
		private void SetPropertyValue(string propertyName, DateTime propertyValue) 
		{
			DocumentProperty prop = _properties[propertyName];
			if (null == prop) 
			{
				_properties.Add(new DocumentProperty(propertyName, propertyValue));
			}
			else 
			{
				prop.PropertyValue = propertyValue;
			}
		}


		#endregion

	}

	
}
