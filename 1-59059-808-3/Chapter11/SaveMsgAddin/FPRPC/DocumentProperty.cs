using System;

namespace FPRPC
{
	/// <summary>
	/// Contains a document property.
	/// </summary>
	public class DocumentProperty: ICloneable
	{
		#region constants
		private const string FP_DATE_FORMAT = "dd MMM yyyy HH':'mm':'ss '-0000'";
		#endregion
		
		#region Property Access and Types
		public enum PropertyAccessLevel
		{
			ReadOnly = 0,
			Excluded,
			ReadWrite
		}

		private static string[] _propertyAccessMap = { "R", "X", "W" }; 

		public enum PropertyDataType
		{
			Boolean = 0,
			FileSystemTime,
			Integer,			
			String,
			DateTime,
			StringVector 
		}
		private static string[] _propertyDataTypeMap = { "B", "F", "I", "S", "T", "V" } ;
		#endregion

		#region ..ctors

		private DocumentProperty() 
		{
		}

		internal DocumentProperty(	
			string name,
			string propertyTypeAndAccess,
			string propertyValue)
		{
			PropertyName = name;
			
			PropertyDataType type;
			PropertyAccessLevel level;
			GetAccessAndTypeFromString(propertyTypeAndAccess, out type, out level);
			
			SetPropertyAccess(level);
			SetTypeAppropriateValue(type, propertyValue);
		}	

		internal DocumentProperty(	string name, 
									PropertyAccessLevel propertyAccess, 
									PropertyDataType dataType, 
									object propertyValue)
		{			
			PropertyName = name;
			PropertyValue = propertyValue;
			SetPropertyAccess(propertyAccess);
			SetPropertyDataType(dataType);
		}

		public DocumentProperty(string name, object propertyValue) 
		{
			PropertyName = name;
			PropertyValue = propertyValue;
		}
		#endregion

		#region properties
		private  string _propertyName;

		public string PropertyName
		{
			get
			{
				return _propertyName;
			}
			set
			{
				_propertyName = value;
			}
		}

		private  object _propertyValue;

		public object PropertyValue
		{
			get
			{
				return _propertyValue;
			}
			set
			{
				SetPropertyTypeAndValue(value);
			}
		}

		private  PropertyAccessLevel _propertyAccess;

		public PropertyAccessLevel PropertyAccess
		{
			get
			{
				return _propertyAccess;
			}
		}

		private  PropertyDataType _dataType;
		public PropertyDataType DataType
		{
			get
			{
				return _dataType;
			}
		}
		#endregion
        
		#region methods

		protected string BuildMetaDataEntry(string field, string type, string data, bool encode) 
		{
			string format = "{0};{1}|{2}";
			return string.Format(format, field, type, (encode?System.Web.HttpUtility.UrlEncode(data):data));			
		}

		private void GetAccessAndTypeFromString(string propTypeAndAccess, out PropertyDataType type, out PropertyAccessLevel level) 
		{
			string typeValue = propTypeAndAccess.Substring(0,1);
			string accessValue = propTypeAndAccess.Substring(1,1);

			type = PropertyDataType.String;
			for(int i=0; i < _propertyDataTypeMap.Length; i++) 
			{
				if (typeValue == _propertyDataTypeMap[i]) 
				{
					type = (PropertyDataType)i;
					break;
				}
			}

			level = PropertyAccessLevel.ReadOnly;
			for(int i=0; i < _propertyAccessMap.Length; i++) 
			{
				if (accessValue == _propertyAccessMap[i]) 
				{
					level = (PropertyAccessLevel)i;
					break;
				}
			}
		}

		/// <summary>
		/// Builds the FrontPage RPC appropriate encoding for a property value.
		/// </summary>
		/// <remarks>
		/// Properties are generally encoded as: 
		/// <code>PropertyName;SW|PropertyValue</code>
		/// </remarks>
		/// <param name="encode"></param>
		/// <returns></returns>
		internal string GetPropertyFP(bool encode) 
		{
			string type = _propertyDataTypeMap[(int)DataType] + _propertyAccessMap[(int)PropertyAccess] ;
			string data = string.Empty;

			switch(DataType) 
			{
				case PropertyDataType.DateTime:
				case PropertyDataType.FileSystemTime:
					data = ((DateTime)PropertyValue).ToString(FP_DATE_FORMAT);
					break;

				default:
					if (null == PropertyValue)
                        data = String.Empty;
					else
						data = PropertyValue.ToString();
					break;
			}
            
			return BuildMetaDataEntry(PropertyName, type, data, encode);
		}

		internal string GetPropertyFP() 
		{
			return GetPropertyFP(true);
		}

		
		protected void SetPropertyAccess(PropertyAccessLevel accessLevel) 
		{
			_propertyAccess = accessLevel;
		}

		protected void SetPropertyDataType(PropertyDataType type) 
		{
			_dataType = type;
		}


		protected void SetPropertyTypeAndValue(object value) 
		{
			_propertyValue = value;
			if (value is String || value is string) 
			{
				SetPropertyDataType(PropertyDataType.String);
				return;
			}

			if (value is DateTime) {
				SetPropertyDataType(PropertyDataType.DateTime);
				return;
			}

			if (PropertyValue is Int32 ||
				PropertyValue is Int16)
			{
				SetPropertyDataType(PropertyDataType.Integer);
				return;
			}

			if (PropertyValue is bool ||
				PropertyValue is Boolean)
			{
				SetPropertyDataType(PropertyDataType.Boolean);
				return;
			}

			SetPropertyDataType(PropertyDataType.String);
		}

		


		private void SetTypeAppropriateValue(PropertyDataType type, string value) 
		{
			switch(type) 
			{
				case PropertyDataType.Boolean:
					PropertyValue = bool.Parse(value);
					break;

				case PropertyDataType.DateTime:
				case PropertyDataType.FileSystemTime:
					DateTime dateValue = DateTime.ParseExact(value, FP_DATE_FORMAT, null);
					
					PropertyValue = dateValue;
					break;

				case PropertyDataType.Integer:
					PropertyValue = (int)int.Parse(value);
					break;

				case PropertyDataType.String:
				case PropertyDataType.StringVector:
					PropertyValue = value;
					break;

				default:
					throw new ApplicationException("Unable to parse value " + value + " to a valid type.");
					break;
			}
		}
		#endregion

		#region ICloneable Members

		public object Clone()
		{
			DocumentProperty newProp = new DocumentProperty();

			newProp._dataType = this._dataType;
			newProp._propertyAccess = this._propertyAccess;
			newProp._propertyName = this._propertyName;
			newProp._propertyValue = this._propertyValue;

			return newProp;
		}

		#endregion
	}
}
