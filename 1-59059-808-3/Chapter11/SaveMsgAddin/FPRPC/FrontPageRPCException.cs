using System;

namespace FPRPC
{
	/// <summary>
	/// FrontPage RPC exception. 
	/// </summary>
	/// <remarks>Currently this serves as a simple wrapper for an ApplicationException</remarks>
	[Serializable]
	public class FrontPageRPCException: System.ApplicationException
	{
		public string Url;

		public FrontPageRPCException():base()
		{
		}

		public FrontPageRPCException(string message):base(message)
		{
		}

		public FrontPageRPCException(string message, string url):base(message)
		{
			Url = url;
		}

		public FrontPageRPCException(string message, System.Exception innerException):base(message, innerException)
		{
		}

		public FrontPageRPCException(string message, string url, System.Exception innerException):base(message, innerException)
		{
			Url = url;
		}
	}
}
