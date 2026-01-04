using Authorize.NET_API.Code;
using Authorize.NET_API.Models;
using Authorize.NET_API.RequestSchema;
using Newtonsoft.Json;
using System;

namespace Authorize.NET_API
{
	public static class AuthorizeApi
	{
		public static string AuthenticateTestRequest(
		  string urlEndpoint,
		  string contentType,
		  MerchantAuthentication merchant)
		{
			string response = contentType == "application/json" ? AuthorizeApi.AutheticateTestRequestJson(urlEndpoint, merchant) : AuthorizeApi.AutheticateTestRequestXml(urlEndpoint, merchant);
			Console.WriteLine(AuthorizeApi.BuildResponse(response));
			return response;
		}

		private static string AutheticateTestRequestJson(
		  string urlEndpoint,
		  MerchantAuthentication merchant)
		{
			string body = JsonConvert.SerializeObject((object) Authorize.NET_API.RequestSchema.Json.AuthenticateTestRequest(merchant));
			return Rest.Request(urlEndpoint, "POST", "application/json", body);
		}

		private static string AutheticateTestRequestXml(
		  string urlEndpoint,
		  MerchantAuthentication merchant)
		{
			string body = Xml.AuthenticateTestRequest(merchant);
			return Rest.Request(urlEndpoint, "POST", "text/xml", body);
		}

		public static string CreateTransactionRequest(string urlEndpoint, Transaction transaction)
		{
			string body = JsonConvert.SerializeObject((object) RequestSchema.Json.CreateTransactionRequest(transaction));
			string response = Rest.Request(urlEndpoint, "POST", "application/json", body);
			//Console.WriteLine(AuthorizeApi.BuildResponse(response));
			return response;
		}

		public static string GetTransactionDetails(string urlEndpoint, TransactionDetailsRequest transDetails)
		{
			string body = JsonConvert.SerializeObject((object) RequestSchema.Json.GetTransactionDetailsRequest(transDetails));
			string response = Rest.Request(urlEndpoint, "POST", "application/json", body);
			//Console.WriteLine(AuthorizeApi.BuildResponse(response));
			return response;
		}

		public static string GetMerchantDetails(string urlEndpoint, MerchantDetailsRequest merchantDetails)
		{
			string body = JsonConvert.SerializeObject((object) RequestSchema.Json.GetMerchantDetailsRequest(merchantDetails));
			string response = Rest.Request(urlEndpoint, "POST", "application/json", body);
			//Console.WriteLine(AuthorizeApi.BuildResponse(response));
			return response;
		}

		public static string GetSettledBatchListRequest(string urlEndpoint, MerchantAuthentication merchant, DateTime from, DateTime to)
		{
			string body = Xml.SettledBatchListRequest(merchant, from, to);
			string response = Rest.Request(urlEndpoint, "POST", "text/xml", body);
			Console.WriteLine(AuthorizeApi.BuildResponse(response));
			return response;
		}

		private static string BuildResponse(string response) => "Response:\n" + response;
	}
}