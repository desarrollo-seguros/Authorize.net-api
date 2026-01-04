using Newtonsoft.Json;
using System.Collections.Generic;

namespace Authorize.NET_API.Models
{
	//#region Root
	//public class MerchantDetailsResponse : IAuthorizeResponse
	//{
	//	[JsonProperty("merchantDetails")]
	//	public MerchantDetails MerchantDetails { get; set; }

	//	[JsonProperty("messages")]
	//	public Messages Messages { get; set; }
	//}
	//#endregion

	#region MerchantDetails
	public class MerchantDetailsResponse : IAuthorizeResponse
	{
		[JsonProperty("isTestMode")]
		public bool IsTestMode { get; set; }

		[JsonProperty("processors")]
		public List<Processor> Processors { get; set; }

		[JsonProperty("merchantName")]
		public string MerchantName { get; set; }

		[JsonProperty("gatewayId")]
		public string GatewayId { get; set; }

		[JsonProperty("marketTypes")]
		public List<string> MarketTypes { get; set; }

		[JsonProperty("productCodes")]
		public List<string> ProductCodes { get; set; }

		[JsonProperty("paymentMethods")]
		public List<string> PaymentMethods { get; set; }

		[JsonProperty("currencies")]
		public List<string> Currencies { get; set; }

		[JsonProperty("publicClientKey")]
		public string PublicClientKey { get; set; }

		[JsonProperty("businessInformation")]
		public BusinessInformation BusinessInformation { get; set; }

		[JsonProperty("merchantTimeZone")]
		public string MerchantTimeZone { get; set; }

		[JsonProperty("contactDetails")]
		public List<ContactDetail> ContactDetails { get; set; }

		[JsonProperty("messages")]
		public Messages Messages { get; set; }
	}
	#endregion

	#region Processors
	public class Processor
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("cardTypes")]
		public List<string> CardTypes { get; set; }
	}
	#endregion

	#region BusinessInformation
	public class BusinessInformation
	{
		[JsonProperty("phoneNumber")]
		public string PhoneNumber { get; set; }

		[JsonProperty("company")]
		public string Company { get; set; }

		[JsonProperty("address")]
		public string Address { get; set; }

		[JsonProperty("city")]
		public string City { get; set; }

		[JsonProperty("state")]
		public string State { get; set; }

		[JsonProperty("zip")]
		public string Zip { get; set; }

		[JsonProperty("country")]
		public string Country { get; set; }
	}
	#endregion

	#region ContactDetails
	public class ContactDetail
	{
		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("firstName")]
		public string FirstName { get; set; }

		[JsonProperty("lastName")]
		public string LastName { get; set; }
	}
	#endregion
}
