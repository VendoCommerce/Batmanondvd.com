using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using CSPaymentProvider;


namespace CSPaymentProvider.Providers
{
	public class NationalBankcardAccount : IPaymentProvider
	{

		#region Private Members

		//private static ProviderConfiguration m_ProviderConfiguration = ProviderConfiguration.GetProviderConfiguration(m_ProviderSection);
		//public static Provider objProvider = null;
		private GatewaySettings gatewaySettings = null;
		//private string errRequiredNode = "A required gateway node does not exist: {0}";

		#endregion

		#region Public Methods
		public Response PerformRequest(Request request)
		{

			Response response = new Response();
			StreamWriter streamWriter = null;
			StreamReader streamReader = null;
			string strPost = string.Empty;

			strPost = BuildRequestPost(request);

			//Initialize & populate HTTP request object
			HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(gatewaySettings.TransactionURL);

			//set header attributes
			objRequest.Method = "POST";
			objRequest.ContentLength = strPost.Length;
			objRequest.ContentType = "application/x-www-form-urlencoded";

			try
			{

				//write post for request to url
				streamWriter = new StreamWriter(objRequest.GetRequestStream());
				streamWriter.Write(strPost);

			}
			catch (Exception ex)
			{
				//error while writing post
				throw new PaymentProviderException("An exception occured while getting the request stream for the gateway", ex);
			}
			finally
			{
				streamWriter.Close();
			}

			HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();

			try
			{

				//read response from request
				streamReader = new StreamReader(objResponse.GetResponseStream());

				response = ParseResponse(streamReader.ReadToEnd());

			}
			catch (Exception ex)
			{
				throw new PaymentProviderException("An exception occured while getting the response stream for the gateway", ex);
			}
			finally
			{
				streamReader.Close();
			}

			return response;

		}
        public Response PerformVoidSettledRequest(Request request)
        {
            Response response = new Response();
            return response;
        }
        public Response PerformValidationRequest(Request request)
        {
            Response response = new Response();
            return response;
        }
		public Response PerformVoidRequest(Request request)
		{

			Response response = new Response();
			StreamWriter streamWriter = null;
			StreamReader streamReader = null;
			string strPost = string.Empty;

			strPost = BuildVoidRequestPost(request);

			//Initialize & populate HTTP request object
			HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(gatewaySettings.TransactionURL);

			//set header attributes
			objRequest.Method = "POST";
			objRequest.ContentLength = strPost.Length;
			objRequest.ContentType = "application/x-www-form-urlencoded";

			try
			{

				//write post for request to url
				streamWriter = new StreamWriter(objRequest.GetRequestStream());
				streamWriter.Write(strPost);

			}
			catch (Exception ex)
			{
				//error while writing post
				throw new PaymentProviderException("An exception occured while getting the request stream for the gateway", ex);
			}
			finally
			{
				streamWriter.Close();
			}

			HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();

			try
			{

				//read response from request
				streamReader = new StreamReader(objResponse.GetResponseStream());

				response = ParseResponse(streamReader.ReadToEnd());

			}
			catch (Exception ex)
			{
				throw new PaymentProviderException("An exception occured while getting the response stream for the gateway", ex);
			}
			finally
			{
				streamReader.Close();
			}

			return response;

		}
		protected string BuildRequestPost(Request request)
		{

			string NameValueFormat = "{0}={1}";

			List<string> aryPostParams = new List<string>();


			//Create parameters from objProvider settings
			aryPostParams.Add(string.Format(NameValueFormat, "type", gatewaySettings.RequestType));
			aryPostParams.Add(string.Format(NameValueFormat, "username", gatewaySettings.Login));
			aryPostParams.Add(string.Format(NameValueFormat, "password", gatewaySettings.Password));


			//Create parameters from request            
			try
			{
				if (request.Amount > 0)
				{
					aryPostParams.Add(string.Format("amount={0:.00}", request.Amount));
				}
				//load optional params
				aryPostParams.Add(string.Format("ccexp={0:####}", request.ExpireDate));
				aryPostParams.Add(string.Format(NameValueFormat, "ccnumber", request.CardNumber));
				aryPostParams.Add(string.Format(NameValueFormat, "cvv", request.CardCvv));
				aryPostParams.Add(string.Format(NameValueFormat, "orderdescription", request.TransactionDescription));
				aryPostParams.Add(string.Format(NameValueFormat, "orderid", request.InvoiceNumber));
				if (request.FirstName.Trim().Length > 0) aryPostParams.Add(string.Format(NameValueFormat, "firstname", request.FirstName));
				if (request.LastName.Trim().Length > 0) aryPostParams.Add(string.Format(NameValueFormat, "lastname", request.LastName));
				if (request.Address1.Trim().Length > 0) aryPostParams.Add(string.Format(NameValueFormat, "address1", request.Address1 + (string.IsNullOrEmpty(request.Address2) == false ? "+" + request.Address2 : string.Empty)));
				if (request.City.Trim().Length > 0) aryPostParams.Add(string.Format(NameValueFormat, "city", request.City));
				if (request.State.Trim().Length > 0) aryPostParams.Add(string.Format(NameValueFormat, "state", request.State));
				if (request.ZipCode.Trim().Length > 0) aryPostParams.Add(string.Format(NameValueFormat, "zip", request.ZipCode));
				if (request.ZipCode.Trim().Length > 0) aryPostParams.Add(string.Format(NameValueFormat, "country", request.Country));
				if (request.ZipCode.Trim().Length > 0) aryPostParams.Add(string.Format(NameValueFormat, "phone", request.Phone));
				if (request.ZipCode.Trim().Length > 0) aryPostParams.Add(string.Format(NameValueFormat, "email", request.Email));

				bool testMode = false;
				bool.TryParse(gatewaySettings.TestMode, out testMode);
			}
			catch (Exception ex)
			{

				throw new PaymentProviderException(string.Format("An exception occured while creating the post parameters: {0}", ex.Message), ex);
			}

			return string.Join("&", aryPostParams.ToArray());

		}
		protected string BuildVoidRequestPost(Request request)
		{

			string NameValueFormat = "{0}={1}";

			List<string> aryPostParams = new List<string>();


			//Create parameters from objProvider settings
			aryPostParams.Add(string.Format(NameValueFormat, "type", "void"));// This is to void particular transaction.
			aryPostParams.Add(string.Format(NameValueFormat, "username", gatewaySettings.Login));
			aryPostParams.Add(string.Format(NameValueFormat, "password", gatewaySettings.Password));


			//Create parameters from request            
			try
			{
				if (request.Amount > 0)
				{
					aryPostParams.Add(string.Format("amount={0:.00}", request.Amount));
				}
				//load optional params
				aryPostParams.Add(string.Format(NameValueFormat, "transactionid", request.TransactionID));
				bool testMode = false;
				bool.TryParse(gatewaySettings.TestMode, out testMode);
			}
			catch (Exception ex)
			{

				throw new PaymentProviderException(string.Format("An exception occured while creating the post parameters: {0}", ex.Message), ex);
			}

			return string.Join("&", aryPostParams.ToArray());

		}
		private Response ParseResponse(string GatewayResponse)
		{

			Response response = new Response();

			response.GatewayResponseRaw = GatewayResponse;

			string[] aryGatewayResponse = GatewayResponse.Split(Convert.ToChar(gatewaySettings.DelimChar));


			if (aryGatewayResponse.Length > 0)
			{
				if (aryGatewayResponse[8].Split('=')[1].Contains("100"))
				{
					//Declined
					response.ResponseType = TransactionResponseType.Approved;
				}
				else
				{
					//Approved

					response.ResponseType = TransactionResponseType.Denied;
				}
				response.TransactionID = aryGatewayResponse[3].Split('=')[1].ToString();
				response.AuthCode = aryGatewayResponse[2].Split('=')[1].ToString();
				response.MerchantDefined1 = aryGatewayResponse[9].Split('=')[1].ToString();


			}
			else
			{
				response.ResponseType = TransactionResponseType.Error;
				response.ReasonText = "Unknown Error (" + aryGatewayResponse + ")";
			}

			return response;

		}

		#endregion

		private class GatewaySettings
		{

			#region Public Properties
                public string CurrencyCode { get; set; }
                public string MerchantEmail { get; set; }
                public string EmailCustomer { get; set; }
                public string DuplicateWindow { get; set; }
                public string Login { get; set; }
                public string Password { get; set; }
                public string TransactionKey { get; set; }
                public string DelimData { get; set; }
                public string DelimChar { get; set; }
                public string EncapChar { get; set; }
                public string Version { get; set; }
                public string RelayResponse { get; set; }
                public string TransactionURL { get; set; }
                public string TestMode { get; set; }
                public string MarketType { get; set; }
                public string DeviceType { get; set; }
                public string RequestType { get; set; }
			#endregion
		}

		public PaymentProviderType Type
		{
			get { return PaymentProviderType.NationalBankcardSystem; }
		}

		public void Initialize(System.Xml.Linq.XElement xmlSettings)
		{
			try
			{
				gatewaySettings = new GatewaySettings();

				//get transaction url attribute
				if (xmlSettings.Attribute("transactionUrl") != null)
					gatewaySettings.TransactionURL = xmlSettings.Attribute("transactionUrl").Value;

				if (xmlSettings.Attribute("login") != null)
					gatewaySettings.Login = xmlSettings.Attribute("login").Value;

				if (xmlSettings.Attribute("password") != null)
					gatewaySettings.Password = xmlSettings.Attribute("password").Value;

				if (xmlSettings.Attribute("transactionKey") != null)
					gatewaySettings.TransactionKey = xmlSettings.Attribute("transactionKey").Value;

				if (xmlSettings.Attribute("delimitedData") != null)
					gatewaySettings.DelimData = xmlSettings.Attribute("delimitedData").Value;

				gatewaySettings.DelimChar = "&";

				if (xmlSettings.Attribute("version") != null)
					gatewaySettings.Version = xmlSettings.Attribute("version").Value;

				if (xmlSettings.Attribute("transactionTest") != null)
					gatewaySettings.TestMode = xmlSettings.Attribute("transactionTest").Value;

				if (xmlSettings.Attribute("requestType") != null)
					gatewaySettings.RequestType = xmlSettings.Attribute("requestType").Value;

				if (xmlSettings.Attribute("deviceType") != null)
					gatewaySettings.DeviceType = xmlSettings.Attribute("deviceType").Value;

				if (xmlSettings.Attribute("marketType") != null)
					gatewaySettings.MarketType = xmlSettings.Attribute("marketType").Value;

				if (string.IsNullOrEmpty(gatewaySettings.TransactionURL))
				{
					throw new PaymentProviderException("TransactionURL cannot be null");
				}

				if (string.IsNullOrEmpty(gatewaySettings.Login))
				{
					throw new PaymentProviderException("Login cannot be null");
				}

				if (string.IsNullOrEmpty(gatewaySettings.TransactionKey))
				{
					throw new PaymentProviderException("TransactionKey cannot be null");
				}

				if (string.IsNullOrEmpty(gatewaySettings.DelimData))
				{
					gatewaySettings.DelimData = "TRUE";
				}

				if (string.IsNullOrEmpty(gatewaySettings.DelimChar))
				{
					gatewaySettings.DelimData = "|";
				}

				if (string.IsNullOrEmpty(gatewaySettings.TestMode))
				{
					gatewaySettings.DelimData = "FALSE";
				}
			}
			catch (Exception ex)
			{
				throw new PaymentProviderException("An error occured while reading the gateway settings", ex);
			}
		}
	}
}
