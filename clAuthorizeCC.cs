using Authorize.NET_API;
using Authorize.NET_API.Constants;
using Authorize.NET_API.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace ME.Mexicard
{
	public class clAuthorizeCC
	{
		public string firstName;
		public string lastName;
		public string address;
		public string city;
		public string state;
		public string zip;
		public string country;
		public double amount;
		public string orderDescription;
		public string invoiceNo;
		public string CCno;
		public string CCExpDate;
		public string CCcode;
		public string CCtype;
		public string netCode;
		public string netTransid;
		public string msg;
		public string isTest;
		public string plainResponse = string.Empty;

		public clAuthorizeCC()
		{
			this.isTest = "False";
		}

		public bool AuthorizePayment()
		{
			Endpoint.EndPointData endPointData = this.isTest == "False" ? Endpoint.Production : Endpoint.Sandbox;

			Transaction transaction = new Transaction()
			{
				Merchant = new MerchantAuthentication()
				{
					ApiLoginId = endPointData.ApiLoginId,
					TransactionKey = endPointData.TransactionKey
				},
				Address = new Address()
				{
					AddressName = this.address,
					Country = this.country,
					State = this.state,
					City = this.city,
					FirstName = this.firstName,
					LastName = this.lastName,
					Zip = this.zip
				},
				CreditCard = new CreditCard()
				{
					Number = this.CCno,
					Code = this.CCcode,
					ExpirationDate = this.CCExpDate
				},
				OrderInformation = new OrderInformation()
				{
					Description = this.orderDescription,
					InvoiceNumber = this.invoiceNo
				},
				Amount = (Decimal) this.amount
			};

			this.msg = string.Empty;

			JObject jObject = new JObject();

			try
			{
				this.plainResponse = AuthorizeApi.CreateTransactionRequest(endPointData.Url, transaction);
				jObject = JObject.Parse(this.plainResponse);
			}
			catch (Exception ex)
			{
				this.msg = ex.Message;
				return false;
			}

			if (this.HasResultCode(jObject) && this.HasMessages(jObject) && (string) jObject["messages"][(object) "resultCode"] == "Error")
				this.msg += this.BuildMessages(jObject["messages"][(object) "message"]);

			if (this.HasResponseMessages(jObject))
				this.msg += this.BuildResponseMessages(jObject["transactionResponse"][(object) "messages"]);

			if (this.HasResponseErrors(jObject))
				this.msg += this.BuildResponseErrors(jObject["transactionResponse"][(object) "errors"]);

			Console.WriteLine();
			Console.WriteLine(this.msg);

			if (!this.HasResultCode(jObject) || !this.HasResponseCode(jObject) ||
				!((string) jObject["messages"][(object) "resultCode"] == "Ok") ||
				!((string) jObject["transactionResponse"][(object) "responseCode"] == "1"))
				return false;

			this.netCode = (string) jObject["transactionResponse"][(object) "authCode"];
			this.netTransid = (string) jObject["transactionResponse"][(object) "transId"];

			return true;
		}

		//public bool GetTransactionDetails(string transId)
		//{
		//	Endpoint.EndPointData endPointData = this.isTest == "False" ? Endpoint.Production : Endpoint.Sandbox;

		//	TransactionDetailsRequest request = new TransactionDetailsRequest()
		//	{
		//		Merchant = new MerchantAuthentication()
		//		{
		//			ApiLoginId = endPointData.ApiLoginId,
		//			TransactionKey = endPointData.TransactionKey
		//		},
		//		TransactionId = transId
		//	};

		//	try
		//	{
		//		this.plainResponse = AuthorizeApi.GetTransactionDetails(
		//			endPointData.Url,
		//			request
		//		);

		//		JObject responseJson = JObject.Parse(this.plainResponse);

		//		// Aquí puedes reutilizar lógica similar a HasResultCode / BuildMessages
		//		return responseJson["messages"]?["resultCode"]?.ToString() == "Ok";
		//	}
		//	catch (Exception ex)
		//	{
		//		this.msg = ex.Message;
		//		return false;
		//	}
		//}

		private bool HasMessages(JObject json)
		{
			return json["messages"] != null && json["messages"][(object) "message"] != null;
		}

		private bool HasResultCode(JObject json)
		{
			return json["messages"] != null && json["messages"][(object) "resultCode"] != null;
		}

		private bool HasResponseMessages(JObject json)
		{
			return json["transactionResponse"] != null && json["transactionResponse"][(object) "messages"] != null;
		}

		private bool HasResponseErrors(JObject json)
		{
			return json["transactionResponse"] != null && json["transactionResponse"][(object) "errors"] != null;
		}

		private bool HasResponseCode(JObject json)
		{
			return json["transactionResponse"] != null && json["transactionResponse"][(object) "responseCode"] != null;
		}

		private string FormatToMessage(JToken token) => string.Empty;

		private string BuildMessages(JToken token)
		{
			return token.Select(jtoken => new
			{
				Key = (string) jtoken.SelectToken("code"),
				Message = (string) jtoken.SelectToken("text")
			}).Select(obj => $"Code: {obj.Key}\n{obj.Message}\n").Aggregate<string, string>("", (Func<string, string, string>) ((current, next) => current + next));
		}

		private string BuildResponseMessages(JToken token)
		{
			return token.Select(jtoken => new
			{
				Key = (string) jtoken.SelectToken("code"),
				Message = (string) jtoken.SelectToken("description")
			}).Select(obj => $"Code: {obj.Key}\n{obj.Message}\n").Aggregate<string, string>("", (Func<string, string, string>) ((current, next) => current + next));
		}

		private string BuildResponseErrors(JToken token)
		{
			return token.Select(jtoken => new
			{
				Key = (string) jtoken.SelectToken("errorCode"),
				Message = (string) jtoken.SelectToken("errorText")
			}).Select(obj => $"Code: {obj.Key}\n{obj.Message}\n").Aggregate<string, string>("", (Func<string, string, string>) ((current, next) => current + next));
		}
	}
}