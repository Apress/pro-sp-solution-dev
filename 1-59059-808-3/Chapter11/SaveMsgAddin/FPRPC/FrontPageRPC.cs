using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections.Specialized;


namespace FPRPC
{
	/// <summary>
	/// Client for calling Front Page Server Extensions.
	/// </summary>
	public class FrontPageRPC
	{
		#region static members
		private static string serverExtensionsVersion = null;
		private static System.Text.RegularExpressions.Regex metaInfoMatchRegEx = new System.Text.RegularExpressions.Regex(@"\<li\>meta_info=\n\<ul\>\n(?<metaInfo>.*?)\<\/ul\>", RegexOptions.Compiled | RegexOptions.Singleline);
		private static System.Text.RegularExpressions.Regex proprtyInfoRegEx = new System.Text.RegularExpressions.Regex(@"\<li\>(?<propName>.*?)\n\<li\>(?<propType>.*?)\|(?<propValue>.*?)\n", RegexOptions.Compiled | RegexOptions.Multiline);
		#endregion

		#region enum
		protected enum GetDocumentResponseStatus 
		{
			Success,
			Failed_CheckedOut,
			Failed
		}
		#endregion

		#region constants
		private const string STR_AUTHOR_DLL_PATH = "/_vti_bin/_vti_aut/author.dll";
		private const string STR_VTI_RPC_PATH = "/_vti_bin/shtml.dll/_vti_rpc";
		private const string STR_CRLF = "\r\n";
		private const string STR_NEWLINE = "\n";
		private const string STR_SEMICOLON = ";";
		private const string STR_VLINE = "|";
		private const string STR_LEFT_ANGLE_BRACKET = "<";
		private const string STR_PERIOD = ".";
		private const string CONTENT_TYPE_URLENCODE = "Content-Type: application/x-www-form-urlencoded";
		private const string CONTENT_TYPE_OCTETSTREAM = "Content-Type: application/octet-stream";

		private const string STRING_TYPE = "SW";
		private const string DATETIME_TYPE = "TW";
		private const string FP_DATE_FORMAT = "dd MMM yyyy HH':'mm':'ss '0000'";

		private const int WRITE_BUFFER_SIZE = 2048;

		#endregion

		#region instance members
		private ICredentials _credentials;
		#endregion

		#region ..ctors
		public FrontPageRPC()
		{
		}
		#endregion

		#region properties
		public ICredentials Credentials
		{
			get
			{
				return _credentials;
			}
			set
			{
				_credentials = value;
			}
		}

		#endregion

		#region Front Page RPC Methods
		/// <summary>
		/// Retrieves a document via the FrontPage RPC calls.
		/// </summary>
		/// <returns>A stream to the retrieved document</returns>
		public System.IO.Stream GetDocument(string documentUrl)
		{
			HttpWebResponse response = GetDocumentResponse(documentUrl, "none");

			System.IO.Stream responseStream = response.GetResponseStream();
			GetDocumentResponseStatus status;
			System.IO.Stream documentStream = GetDocumentResponse(responseStream, out status);

			if (GetDocumentResponseStatus.Success == status) 
			{
				return documentStream;
			}

			return null;
		}

		/// <summary>
		/// Retrieves document meta-information.
		/// </summary>
		/// <param name="documentUrl"></param>
		/// <returns></returns>
		public DocumentInfo GetDocumentMetaInfo(string documentUrl) 
		{
			try 
			{
				WebUrl webUrl = UrlToWebUrl(documentUrl);

				System.Collections.Specialized.NameValueCollection methodData = new System.Collections.Specialized.NameValueCollection();

				methodData.Add("method","getDocsMetaInfo:" + GetServerExtensionsVersion(webUrl.SiteUrl));
				methodData.Add("service_name","");
				methodData.Add("listHiddenDocs","true");
				methodData.Add("listLinkInfo","true");
				methodData.Add("url_list","[" + webUrl.FileUrl + "]");

				HttpWebRequest req = StartWebRequest(GetAuthorURL(webUrl.SiteUrl), methodData);
				System.IO.Stream reqStream = req.GetRequestStream();

				reqStream.Flush();
				reqStream.Close();

				HttpWebResponse response = (HttpWebResponse)req.GetResponse();

				System.IO.Stream responseStream = response.GetResponseStream();
				System.IO.StreamReader sr = new StreamReader(responseStream);
				string responseData = sr.ReadToEnd();

				DocumentInfo docInfo = ParseMetaInformationResponse(responseData);
				
				return docInfo;
			}
			catch(Exception e) 
			{
				throw new FrontPageRPCException("SetDocumentMetaInfo failed", documentUrl, e);
			}

		}

		/// <summary>
		/// Retrieves server extensions version from site.
		/// </summary>
		/// <param name="siteUrl"></param>
		/// <returns></returns>
		public string GetServerExtensionsVersion(string siteUrl)
		{

			System.Globalization.CultureInfo format=new System.Globalization.CultureInfo("en-US", true);
			if(null != serverExtensionsVersion) return serverExtensionsVersion ;
			
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GetAuthorURL(siteUrl));



			// FP-RPC Method "server version"
			// We need to tell the server the version of RPC that the client "speaks"
			// The server has an "oldest-compatible-client version", which is 4.0.2.2611
			// (see otools\inc\onet\versbase.h) VVERSION_OLDEST_COMPATIBLE_CLIENT.
			// For future compatibility it is best to use the version that fpwec.dll ships with,
			// which seems to be 6.0.2.5614 ... and that's why it is hardcoded here.
			string postData = "method=server version:6.0.2.5614&service_name=/";
			byte[] postDataBytes = System.Text.Encoding.ASCII.GetBytes(postData);
		
			//Set headers 
			request.Credentials  = CredentialCache.DefaultCredentials;
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded"; 
			request.ContentLength = postDataBytes.Length ;
			request.Headers.Add("X-Vermeer-Content-Type","application/x-www-form-urlencoded"); 
			
			//TODO: This routine uses an older calling format pulled from some example code.  It needs to 
			//		be reworked to the new call-style.

			try
			{
				//Write the post data to request stream
				Stream newStream = request.GetRequestStream() ;
				newStream.Write(postDataBytes,0,postDataBytes.Length);
				newStream.Close();

				//Send the request and get response
				HttpWebResponse response = (HttpWebResponse)request.GetResponse ();
				
				//Read the response
				Stream responseStream = response.GetResponseStream();
				StreamReader reader = new StreamReader(responseStream);
				 
				serverExtensionsVersion = ExtractServerExtensionsVersion(reader.ReadToEnd()); 

				Debug.Assert(null!=serverExtensionsVersion,"FATAL ERROR: ServerExtensionsVersion is null!!"); 
				if(null == serverExtensionsVersion)
				{
					throw new FrontPageRPCException("Could not retrieve the server extension version", siteUrl);
				}
				
				return serverExtensionsVersion;
			}
			catch(FrontPageRPCException)
			{
				throw;
			}
			catch(Exception Ex)
			{
				throw new FrontPageRPCException("Could not retrieve the server extension version.", siteUrl, Ex);
			}
		}

		/// <summary>
		/// Checks a document into the Library supplied in the url;
		/// </summary>
		/// <param name="documentUrl">The full path to the destination document (http://server/site/Folder/Docname.doc)</param>
		/// <returns></returns>
		public bool CheckInDocument(string documentUrl, string comment)
		{
			bool rtn = false;

			WebUrl webUrl = UrlToWebUrl(documentUrl);
			NameValueCollection methodData = new NameValueCollection();
 
			methodData.Add("method", "checkin document: " + GetServerExtensionsVersion(webUrl.SiteUrl));
			methodData.Add("service_name","/"); 
			methodData.Add("document_name", webUrl.FileUrl );
			methodData.Add("comment", comment);
			methodData.Add("keep_checked_out", "false" );
			

			HttpWebRequest request = this.StartWebRequest(GetAuthorURL(webUrl.SiteUrl), methodData);
			System.IO.Stream reqStream = request.GetRequestStream();			

			reqStream.Flush();
			reqStream.Close();

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();	

			Stream stream = response.GetResponseStream();
			StreamReader read = new StreamReader(stream);
			string resp = read.ReadToEnd();
			
			if (! CheckInAndOutDocumentResponseSuccess("method=checkin", resp ))
				throw new ApplicationException("Failed to check out document.");

			
			string innerException = CheckForInternalErrorMessage(resp); 
			
			if ( innerException != string.Empty)
				throw new FrontPageRPCException(innerException, documentUrl);
			else
				rtn = true;

			return rtn;;
		}

		/// <summary>
		/// Checks a document out from the supplied library
		/// </summary>
		/// <param name="documentUrl">The full path to the destination document (http://server/site/Folder/Docname.doc)</param>
		/// <returns></returns>
		public bool CheckOutDocument(string documentUrl)
		{
			bool rtn = false;
			WebUrl webUrl = UrlToWebUrl(documentUrl);
			NameValueCollection methodData = new NameValueCollection();
 
			methodData.Add("method", "checkout document: " + GetServerExtensionsVersion(webUrl.SiteUrl));
			methodData.Add("service_name","/");
			methodData.Add("document_name", webUrl.FileUrl );
			methodData.Add("force", "0");
			methodData.Add("timeout", "0");

			HttpWebRequest request = this.StartWebRequest(GetAuthorURL(webUrl.SiteUrl) , methodData);
			System.IO.Stream reqStream = request.GetRequestStream();			

			reqStream.Flush();
			reqStream.Close();

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();	

			Stream stream = response.GetResponseStream();
			StreamReader read = new StreamReader(stream);
			string resp = read.ReadToEnd();

			if (! CheckInAndOutDocumentResponseSuccess("method=checkout", resp ))
				throw new FrontPageRPCException("Failed to check out document.", documentUrl);

			string innerException = CheckForInternalErrorMessage(resp); 
			
			if ( innerException != string.Empty)
				throw new FrontPageRPCException(innerException, documentUrl);
			else
				rtn=true;

			return rtn;
		}

		/// <summary>
		/// Undoes a checkout of a file from a source control database. 
		/// If the file had changes made since it was checked out, this method causes those changes to be lost
		/// </summary>
		/// <param name="documentUrl">The full path to the destination document (http://server/site/Folder/Docname.doc)</param>
		/// <returns></returns>
		public bool UnCheckOutDocument(string documentUrl)
		{
			bool rtn = false;
			WebUrl webUrl = UrlToWebUrl(documentUrl);
			NameValueCollection methodData = new NameValueCollection();
 
			methodData.Add("method", "uncheckout document: " + GetServerExtensionsVersion(webUrl.SiteUrl));
			methodData.Add("service_name","/");
			methodData.Add("document_name", webUrl.FileUrl );
			methodData.Add("force", "false");
			

			HttpWebRequest request = this.StartWebRequest(GetAuthorURL(webUrl.SiteUrl) , methodData);
			System.IO.Stream reqStream = request.GetRequestStream();			

			reqStream.Flush();
			reqStream.Close();

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();	

			Stream stream = response.GetResponseStream();
			StreamReader read = new StreamReader(stream);
			string resp = read.ReadToEnd();
			string innerException = CheckForInternalErrorMessage(resp); 
			
			if ( innerException != string.Empty)
				throw new FrontPageRPCException(innerException, documentUrl);
			else
				rtn=true;

			return rtn;
		}

		public void PutDocument(string destinationUrl, System.IO.Stream file) 
		{
			PutDocument(destinationUrl, file, null);
		}

		/// <summary>
		/// Puts a file stream as a document at the specified URI.
		/// </summary>
		/// <param name="destinationUri">The full path to the destination document (http://server/site/Folder/Docname.doc)</param>
		/// <param name="file">Stream to write</param>
		public void PutDocument(string destinationUri, System.IO.Stream file, DocumentPropertyCollection properties) 
		{

			WebUrl webUrl = UrlToWebUrl(destinationUri);

			System.Collections.Specialized.NameValueCollection methodData = new System.Collections.Specialized.NameValueCollection();

			// Add general request to stream
			methodData.Add("method","put document:" + GetServerExtensionsVersion(webUrl.SiteUrl));
			methodData.Add("service_name",""); 
			methodData.Add("put_option","overwrite,createdir,migrationsemantics"); 
			methodData.Add("keep_checked_out","false");

			HttpWebRequest req = StartWebRequest(GetAuthorURL(webUrl.SiteUrl), methodData);
			System.IO.Stream reqStream = req.GetRequestStream();

			WriteDocumentData(reqStream, webUrl.FileUrl, file, properties);

			reqStream.Flush();
			reqStream.Close();

			HttpWebResponse response = (HttpWebResponse)req.GetResponse();
			try 
			{
				if (!PutDocumentResponseSuccess(GetResponseString(response)))
				{
					throw new FrontPageRPCException("Failed to save document.", destinationUri);
				}
			}
			finally
			{
				if (null != response) response.Close();
			}
		}

		/// <summary>
		/// Updates document meta information.
		/// </summary>
		/// <param name="documentUrl"></param>
		/// <param name="docInfo"></param>
		public void SetDocumentMetaInfo(string documentUrl, DocumentInfo docInfo) 
		{
			string responseData = string.Empty;

			try 
			{
				WebUrl webUrl = UrlToWebUrl(documentUrl);

				System.Collections.Specialized.NameValueCollection methodData = new System.Collections.Specialized.NameValueCollection();

				methodData.Add("method","setDocsMetaInfo:" + GetServerExtensionsVersion(webUrl.SiteUrl));
				methodData.Add("service_name","");
				methodData.Add("listHiddenDocs","true");
				methodData.Add("listLinkInfo","true");
				methodData.Add("url_list","[" + webUrl.FileUrl + "]");
				methodData.Add("metaInfoList","[" + docInfo.GetMetaInfoList(false) + "]");

				HttpWebRequest req = StartWebRequest(GetAuthorURL(webUrl.SiteUrl), methodData);
				System.IO.Stream reqStream = req.GetRequestStream();

				reqStream.Flush();
				reqStream.Close();

				HttpWebResponse response = (HttpWebResponse)req.GetResponse();

				System.IO.Stream responseStream = response.GetResponseStream();
				System.IO.StreamReader sr = new StreamReader(responseStream);
				responseData = sr.ReadToEnd();
			}
			catch(Exception ex) 
			{
				throw new FrontPageRPCException("SetDocumentMetaInfo failed", documentUrl, ex);
			}

			if (!SetMetaDataResponseSuccess(responseData)) 
			{
				throw new FrontPageRPCException("SetDocumentMetaInfo failed", documentUrl);
			}
			
		}

		
		/// <summary>
		/// Has WSS parse the site vs. file/folder portion of a URL.
		/// </summary>
		/// <param name="uri"></param>
		/// <returns></returns>
		public WebUrl UrlToWebUrl(string url) 
		{
			WebUrl webUrl = new WebUrl();
			Uri aUri = new Uri(url);
			
			System.Collections.Specialized.NameValueCollection methodData = new System.Collections.Specialized.NameValueCollection();
			methodData.Add("method","url to web url");
			methodData.Add("url",aUri.AbsolutePath);
			methodData.Add("flags","0");

			HttpWebRequest req = StartWebRequest(GetVtiRPC(aUri.AbsoluteUri), methodData);
			System.IO.Stream reqStream = req.GetRequestStream();

			reqStream.Flush();
			reqStream.Close();
			
			string response = GetResponseString((HttpWebResponse)req.GetResponse());

			string internalError = this.CheckForInternalErrorMessage(response);

			if ( internalError != string.Empty )
				throw new FrontPageRPCException(internalError, url);
			else
			{
				webUrl.SiteUrl = aUri.GetLeftPart(System.UriPartial.Authority) + GetReturnValue(response, "webUrl");
				webUrl.FileUrl = System.Web.HttpUtility.UrlDecode(GetReturnValue(response, "fileUrl"));
			}
			return webUrl;
		}


		#endregion

		#region private/protected methods
		

		/// <summary>
		/// Encodes a collection of data to the request stream.
		/// </summary>
		/// <param name="tw"></param>
		/// <param name="data"></param>
		protected void AddCollectionData(System.IO.TextWriter tw, System.Collections.Specialized.NameValueCollection data) 
		{			
			bool first = true;
			foreach(string key in data) 
			{
				if (first) 
				{
					tw.Write("{0}={1}",key, Encode(data[key]));
					first = false;
				}
				else 
				{
					tw.Write("&{0}={1}", key, Encode(data[key]));
				}												   
			}
		}

		/// <summary>
		/// Searches the response string for the "msg" string
		/// which is return with error meessage as part of the response stream
		/// if an error occurred
		/// </summary>
		/// <param name="response"></param>
		/// <returns></returns>
		private string CheckForInternalErrorMessage(string response)
		{
			string rtn = string.Empty;
			int msgPosition = response.IndexOf("msg");

			if (msgPosition > 0 )
			{
				string toEnd = response.Substring(msgPosition, response.Length -  msgPosition);
				string err = toEnd.Substring(0, toEnd.Length -(toEnd.Length - toEnd.IndexOf("\n")) );
				rtn = err;
			}
			return rtn;
		}

		/// <summary>
		/// Evaluates checkin/checkout responses for success.
		/// </summary>
		/// <param name="stringToTestFor"></param>
		/// <param name="response"></param>
		/// <returns></returns>
		private bool CheckInAndOutDocumentResponseSuccess(string stringToTestFor, string response)
		{
			bool rtn = false;
	
			if ( response.IndexOf(stringToTestFor) > 0 )
				rtn = true;

			return rtn;

		}
		
		/// <summary>
		/// Encodes supplied data appropriately for the FPRPC call.
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		protected string Encode(string data) 
		{
			return System.Web.HttpUtility.UrlEncode(data);
		}
		
		
		
		/// <summary>
		/// Extracts the filename portion of the path.
		/// </summary>
		/// <param name="fullFileName"></param>
		/// <returns></returns>
		protected string ExtractFileName(string fullFileName)
		{
			int pos = fullFileName.LastIndexOf('/');
			if (pos > -1) 
			{
				return fullFileName.Substring(pos + 1);
			}
			return fullFileName;
		}

		/// <summary>
		/// Extract the &lt;html&gt;&lt;/html&gt; preamble in the response stream.
		/// </summary>
		/// <remarks>
		/// We're using the ReadStreamLine method instead of wrapping the stream in a StreamReader because we only
		/// want to consume up to the end of the preamble; the remaining stream is other, potentially binary, data.
		/// </remarks>
		/// <param name="stream"></param>
		/// <returns></returns>
		private string ExtractResponsePreamble(System.IO.Stream stream) 
		{
			// locate <html></html> response and extract.
			System.Text.StringBuilder responseData = new StringBuilder();

			string firstLine = ReadStreamLine(stream);
			if (firstLine.IndexOf("<html>") > -1) 
			{
				responseData.Append(firstLine + "\n");
			}
			else 
			{
				return string.Empty;
			}

			bool endOfData = false;
			string line;
			while(!endOfData) 
			{
				line = ReadStreamLine(stream);
				if (null == line) 
				{
					break;
				}
				if (line.IndexOf("</html>") > -1)
					endOfData = true;
				responseData.Append(line + "\n");
			}

			return responseData.ToString();
		}


		/// <summary>
		/// Invokes the get document FP RPC call and returns the HttpWebResponse.
		/// </summary>
		/// <param name="documentUrl">Complete url to the document, including folders.</param>
		/// <param name="getOption">Options to apply for retrieving the document:
		/// <list>
		/// <item>None</item>
		/// <item>chkoutExclusive</item>
		/// <item>chkoutNonExclusive</item>
		/// </list>
		/// See the SharePoint Front Page RPC documentation for more details <a href="http://msdn.microsoft.com/library/default.asp?url=/library/en-us/spptsdk/html/tsfppget_option_SV01031279.asp"></a>
		/// </param>
		/// <returns></returns>
		private HttpWebResponse GetDocumentResponse(string documentUrl, string getOption) 
		{
			WebUrl webUrl = UrlToWebUrl(documentUrl);

			System.Collections.Specialized.NameValueCollection methodData = new System.Collections.Specialized.NameValueCollection();

			methodData.Add("method","get document:" + GetServerExtensionsVersion(webUrl.SiteUrl));
			methodData.Add("service_name","");
			methodData.Add("document_name",webUrl.FileUrl);
			methodData.Add("get_option",getOption);
			methodData.Add("timeout","10");

			HttpWebRequest req = StartWebRequest(GetAuthorURL(webUrl.SiteUrl), methodData);
			System.IO.Stream reqStream = req.GetRequestStream();

			reqStream.Flush();
			reqStream.Close();

			HttpWebResponse response = (HttpWebResponse)req.GetResponse();

			return response;
		}

		/// <summary>
		/// Retrieves the HttpWebRequest for a given URI setting appropriate values
		/// for FrontPageRPC transmission.
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		protected virtual HttpWebRequest GetHttpWebRequest(string url) 
		{
			System.Diagnostics.Debug.WriteLine("URL: " + url);
			HttpWebRequest req = (HttpWebRequest) WebRequest.Create(url);			
			req.Credentials = Credentials;
			req.Method = "POST";
			req.KeepAlive = true;
			req.ContentType = "application/x-www-form-urlencoded";
			req.Headers.Add("X-Vermeer-Content-Type", "application/x-www-form-urlencoded");
					
			return req;
		}


		/// <summary>
		/// Given a url, returns the appropriate VTI Author extensions.
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		protected string GetAuthorURL(string url) 
		{
			return url + STR_AUTHOR_DLL_PATH;
		}

		/// <summary>
		/// Evalutaes the get document response, returning the response status and the stream to the retrieved document,
		/// if the call succeeded.
		/// </summary>
		/// <param name="responseStream"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		protected System.IO.Stream GetDocumentResponse(System.IO.Stream responseStream, out GetDocumentResponseStatus status ) 
		{
			status = GetDocumentResponseStatus.Failed;
			if (null == responseStream) 
			{
				return null;
			}

			System.IO.BufferedStream bufferedResponseStream = new BufferedStream(responseStream, 1024);

			string responseData = ExtractResponsePreamble(bufferedResponseStream);

			if (null == responseData || string.Empty.Equals(responseData)) 
			{
				return null;
			}
			
			string fpStatus = GetReturnValue(responseData, "status");
			if (fpStatus != null) 
			{
				status = GetDocumentResponseStatus.Failed_CheckedOut;
				return null;
			}

			string fpMessage = GetReturnValue(responseData, "message");
			if (fpMessage.IndexOf("successfully") > -1)
				status = GetDocumentResponseStatus.Success;

			return bufferedResponseStream;
		}

		
		/// <summary>
		/// Retrieves the response string from an HttpWebResponse.
		/// </summary>
		/// <param name="response"></param>
		/// <returns></returns>
		protected string GetResponseString(HttpWebResponse response)
		{
			System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream());
			return sr.ReadToEnd();
		}


		/// <summary>
		/// Extracts a specified value from a FrontPage response string.
		/// </summary>
		/// <param name="response"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		protected string GetReturnValue(string response, string key) 
		{
			int startPos = response.IndexOf(key);
			if (-1 == startPos)
				return null;
			else
				startPos += key.Length + 1;

			int endPos = response.IndexOf("\n",startPos);
			return response.Substring(startPos, endPos - startPos);

		}

		
		/// <summary>
		/// Given a url, returns url with appropriate VTI_RPC path extensions.
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		protected string GetVtiRPC(string url) 
		{
			return url + STR_VTI_RPC_PATH;
		}


		/// <summary>
		/// Parses the GetDocMetaInfo response into a DocumentInfo structure.
		/// </summary>
		/// <param name="responseData"></param>
		/// <returns></returns>
		private DocumentInfo ParseMetaInformationResponse(string responseData)
		{

			DocumentInfo docInfo = new DocumentInfo();

			Match metaInfoMatch = metaInfoMatchRegEx.Match(responseData);

			if (metaInfoMatch.Success) 
			{
				MatchCollection propMatches = proprtyInfoRegEx.Matches(metaInfoMatch.Value);
				foreach(Match propMatch in propMatches) 
				{
					DocumentProperty prop = new DocumentProperty(
						propMatch.Groups["propName"].Value, 
						propMatch.Groups["propType"].Value, 
						System.Web.HttpUtility.UrlDecode(propMatch.Groups["propValue"].Value)
						);

					docInfo.Properties.Add(prop);
				}
			}

			return docInfo;
		}
	

		/// <summary>
		/// Evaluates a response string from a put document for success.
		/// </summary>
		/// <param name="responseString"></param>
		/// <returns></returns>
		protected bool PutDocumentResponseSuccess(string responseString) 
		{
			if(-1 != responseString.IndexOf("method") && -1 != responseString.IndexOf("message"))
			{
				return true;
			}
			return false;
		}
		
		/// <summary>
		/// Reads the next line in a UTF8 Stream.
		/// </summary>
		/// <remarks>
		///	May want to consider wrapping this in our own TextReader derivative that doesn't eat up the
		///	original stream like StreamReader does.
		/// </remarks>
		/// <param name="stream"></param>
		/// <returns></returns>
		private string ReadStreamLine(System.IO.Stream stream) 
		{
			if (null == stream) 
			{
				return null;
			}

			System.Text.StringBuilder sb = new StringBuilder();
			int byteValue;
			do 
			{
				byteValue = stream.ReadByte();
				if (byteValue == -1) break;

				char thisChar = (char)byteValue;

				if ( thisChar == '\r' ) continue;
				if ( thisChar == '\n' )
				{
					break;
				}
				else 
				{
					sb.Append(thisChar);
				}
			}
			while (byteValue != -1);
			
			if (sb.Length == 0)
				return null;
			else
				return sb.ToString();
		}

		/// <summary>
		/// Evaluates the SetMetaData response for success.
		/// </summary>
		/// <param name="responseString"></param>
		/// <returns></returns>
		protected bool SetMetaDataResponseSuccess(string responseString) 
		{
			if (responseString.IndexOf("method") > -1 && -1 == responseString.IndexOf("failedUrls")) 
			{
				return true;
			}
			return false;
		}

		

		/// <summary>
		/// Initiates the web request for a particular FPRPC call.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="methodData"></param>
		/// <returns></returns>
		private HttpWebRequest StartWebRequest(string url, System.Collections.Specialized.NameValueCollection methodData) 
		{
			HttpWebRequest req = GetHttpWebRequest(url);
            //Added this line here
            req.Credentials = CredentialCache.DefaultCredentials;
			System.IO.Stream reqStream = req.GetRequestStream();
			System.IO.StreamWriter sw = new System.IO.StreamWriter(reqStream);

			AddCollectionData(sw, methodData);
			sw.Flush();
			return req;
		}
		
		/// <summary>
		/// Writes metadata and file into the stream appropriately for transmission.
		/// </summary>
		/// <param name="tw"></param>
		/// <param name="fileName">Name of the destination file, including folder paths.</param>
		/// <param name="file"></param>
		protected void WriteDocumentData(System.IO.Stream stream, string destinationFileName, System.IO.Stream file, DocumentPropertyCollection properties) 
		{
			System.IO.TextWriter tw = new StreamWriter(stream);

			DocumentInfo docInfo = new DocumentInfo();
			docInfo.ModifiedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
			docInfo.ModifiedDate = DateTime.Now.ToUniversalTime();
			docInfo.DestinationFileName = destinationFileName;
			docInfo.Title = ExtractFileName(destinationFileName);
			docInfo.Properties.Add(properties);
			docInfo.WriteDocumentData(tw);
			tw.Flush();

			// Write raw document data
			byte[] buffer = new byte[WRITE_BUFFER_SIZE];

			int bytesRead = file.Read(buffer, 0, buffer.Length);
			while(0 != bytesRead) 
			{
				stream.Write(buffer, 0, bytesRead);
				bytesRead = file.Read(buffer, 0, buffer.Length);
			}
			stream.Flush();
		}
		#endregion

		#region static methods
		//Helper method for GetServerExtensionsVersion()
		//The response from the server is in the format :
		//<html><head><title>vermeer RPC packet</title></head>\n<body>\n
		//<p>method=server version:6.0.0.0\n
		//<p>server version=\n<ul>\n
		//<li>major ver=6\n <li>minor ver=0\n<li>phase ver=2\n<li>ver incr=5528\n
		//</ul>\n<p>source control=1\n
		//</body>\n</html>\n
		private static string ExtractServerExtensionsVersion(string response)
		{
			const string majorVer = "major ver";
			const string minorVer = "minor ver";
			const string phaseVer = "phase ver";
			const string verIncr = "ver incr";

			StringBuilder bldr = new StringBuilder();
			int index = response.IndexOf(majorVer) ;
			if (-1 == index) return null;
			int startIndex = index + majorVer.Length + 1;
			int endIndex = response.IndexOf(STR_LEFT_ANGLE_BRACKET,index);
			bldr.Append(response.Substring(startIndex,endIndex-startIndex).Trim());
			bldr.AppendFormat(STR_PERIOD); 

			index = response.IndexOf(minorVer, index) ;
			if (-1 == index) return null;
			startIndex = index + minorVer.Length + 1;
			endIndex = response.IndexOf(STR_LEFT_ANGLE_BRACKET,index);
			bldr.Append(response.Substring(startIndex,endIndex-startIndex).Trim());
			bldr.AppendFormat(STR_PERIOD); 

			index = response.IndexOf(phaseVer, index) ;
			if (-1 == index) return null;
			startIndex = index + phaseVer.Length + 1;
			endIndex = response.IndexOf(STR_LEFT_ANGLE_BRACKET,index);
			bldr.Append(response.Substring(startIndex,endIndex-startIndex).Trim());
			bldr.AppendFormat(STR_PERIOD); 
 
			index = response.IndexOf(verIncr, index) ;
			if (-1 == index) return null;
			startIndex = index + verIncr.Length + 1;
			endIndex = response.IndexOf(STR_LEFT_ANGLE_BRACKET,index);
			bldr.Append(response.Substring(startIndex,endIndex-startIndex).Trim());

			return bldr.ToString();
		}
		#endregion

	}

}

