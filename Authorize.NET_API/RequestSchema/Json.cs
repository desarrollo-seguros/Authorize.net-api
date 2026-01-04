using Authorize.NET_API.Models;
using Newtonsoft.Json.Linq;

namespace Authorize.NET_API.RequestSchema
{
	internal static class Json
	{
		public static JObject AuthenticateTestRequest(Authorize.NET_API.Models.MerchantAuthentication merchant)	
		{
			return new JObject((object) new JProperty("authenticateTestRequest", (object) new JObject((object) new JProperty("merchantAuthentication", (object) Authorize.NET_API.RequestSchema.Json.MerchantAuthentication(merchant)))));
		}

		public static JObject CreateTransactionRequest(Transaction transaction)
		{
			JObject content1 = Authorize.NET_API.RequestSchema.Json.MerchantAuthentication(transaction.Merchant);
			JObject content2 = Authorize.NET_API.RequestSchema.Json.Payment(transaction.CreditCard);
			JObject content3 = Authorize.NET_API.RequestSchema.Json.BillTo(transaction.Address);
			JObject content4 = Authorize.NET_API.RequestSchema.Json.Order(transaction.OrderInformation);

			return new JObject((object) new JProperty("createTransactionRequest", (object) new JObject(new object[2]
			{
				(object) new JProperty("merchantAuthentication", (object) content1),
				(object) new JProperty("transactionRequest", (object) new JObject(new object[5]
				{
					(object) new JProperty("transactionType", (object) "authCaptureTransaction"),
					(object) new JProperty("amount", (object) transaction.Amount),
					(object) new JProperty("payment", (object) content2),
					(object) new JProperty("order", (object) content4),
					(object) new JProperty("billTo", (object) content3)
				}))
			})));
		}

		private static JObject MerchantAuthentication(Authorize.NET_API.Models.MerchantAuthentication merchant)
		{
			return new JObject(new object[2]
			{
				(object) new JProperty("name", (object) merchant.ApiLoginId),
				(object) new JProperty("transactionKey", (object) merchant.TransactionKey)
			});
		}

		private static JObject Payment(CreditCard creditCard)
		{
			return new JObject((object) new JProperty(nameof(creditCard), (object) new JObject(new object[3]
			{
				(object) new JProperty("cardNumber", (object) creditCard.Number),
				(object) new JProperty("expirationDate", (object) creditCard.ExpirationDate),
				(object) new JProperty("cardCode", (object) creditCard.Code)
			})));
		}

		private static JObject BillTo(Address address)
		{
			return new JObject(new object[7]
			{
				(object) new JProperty("firstName", (object) address.FirstName),
				(object) new JProperty("lastName", (object) address.LastName),
				(object) new JProperty(nameof (address), (object) address.AddressName),
				(object) new JProperty("city", (object) address.City),
				(object) new JProperty("state", (object) address.State),
				(object) new JProperty("zip", (object) address.Zip),
				(object) new JProperty("country", (object) address.Country)
			});
		}

		private static JObject Order(OrderInformation order)
		{
			return new JObject(new object[2]
			{
				(object) new JProperty("invoiceNumber", (object) order.InvoiceNumber),
				(object) new JProperty("description", (object) order.Description)
			});
		}

		public static JObject GetTransactionDetailsRequest(TransactionDetailsRequest request)
		{
			JObject merchantAuth = MerchantAuthentication(request.Merchant);

			return new JObject(
				new JProperty("getTransactionDetailsRequest",
					new JObject(
						new JProperty("merchantAuthentication", merchantAuth),
						new JProperty("transId", request.TransactionId)
					)
				)
			);
		}

		public static JObject GetMerchantDetailsRequest(MerchantDetailsRequest request)
		{
			JObject merchantAuth = MerchantAuthentication(request.Merchant);

			return new JObject(
				new JProperty("getMerchantDetailsRequest",
					new JObject(
						new JProperty("merchantAuthentication", merchantAuth)
					)
				)
			);
		}
	}
}
