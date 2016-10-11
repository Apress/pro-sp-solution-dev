using System;

namespace FPRPC
{
	/// <summary>
	/// Maintains a collection of DocumentProperty objects.
	/// <seealso cref="FPRPC.DocumentProperty"/>
	/// </summary>
	public class DocumentPropertyCollection: System.Collections.DictionaryBase
	{		
		public DocumentPropertyCollection()
		{
		}

		public void Add(DocumentProperty prop)
		{
			base.InnerHashtable.Add(prop.PropertyName, prop);
		}

		public void Add(DocumentPropertyCollection properties)
		{
			if (null != properties) 
			{
				foreach(DocumentProperty prop in properties) 
				{
					Add((DocumentProperty)prop.Clone());
				}
			}
		}

		public void Remove(DocumentProperty prop) 
		{
			base.InnerHashtable.Remove(prop.PropertyName);
		}

		public void Remove(string propertyName) 
		{
			base.InnerHashtable.Remove(propertyName);
		}

		public DocumentProperty this[string propertyName] 
		{
			get 
			{
				return (DocumentProperty)base.InnerHashtable[propertyName];
			}

			set 
			{
				base.InnerHashtable[propertyName] = value;
			}
		}

		public new System.Collections.IEnumerator GetEnumerator() 
		{
			return base.InnerHashtable.Values.GetEnumerator();
		}

		
	}
}
