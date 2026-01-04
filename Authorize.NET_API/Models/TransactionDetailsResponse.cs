using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace Authorize.NET_API.Models
{
	#region Root
	public class TransactionDetailsResponse : IAuthorizeResponse
	{
		[JsonProperty("transaction")]
		public TransactionDetails TransactionDetails { get; set; }

		[JsonProperty("messages")]
		public Messages Messages { get; set; }
	}
	#endregion

	#region TransactionDetails
	public class TransactionDetails
	{
		[JsonProperty("transId")]
		public string TransId { get; set; }

		[JsonProperty("submitTimeUTC")]
		public DateTime? SubmitTimeUTC { get; set; }

		[JsonProperty("submitTimeLocal")]
		public DateTime? SubmitTimeLocal { get; set; }

		[JsonProperty("transactionType")]
		public string TransactionType { get; set; }

		[JsonIgnore]
		public TransactionTypeEnum TransactionTypeEnum =>
			EnumMapper.ParseOrUnknown<TransactionTypeEnum>(TransactionType);

		[JsonIgnore]
		public string TransactionTypeDescription =>
		EnumHelper.GetDescription(TransactionTypeEnum);

		[JsonProperty("transactionStatus")]
		public string TransactionStatus { get; set; }

		[JsonIgnore]
		public TransactionStatusEnum TransactionStatusEnum =>
			EnumMapper.ParseOrUnknown<TransactionStatusEnum>(TransactionStatus);

		[JsonIgnore]
		public string TransactionStatusDescription =>
		EnumHelper.GetDescription(TransactionStatusEnum);

		[JsonProperty("responseCode")]
		public int? ResponseCode { get; set; }

		[JsonProperty("responseReasonCode")]
		public int? ResponseReasonCode { get; set; }

		[JsonProperty("responseReasonDescription")]
		public string ResponseReasonDescription { get; set; }

		[JsonProperty("authCode")]
		public string AuthCode { get; set; }

		[JsonProperty("AVSResponse")]
		public string AVSResponse { get; set; }

		[JsonProperty("cardCodeResponse")]
		public string CardCodeResponse { get; set; }

		[JsonProperty("order")]
		public Order Order { get; set; }

		[JsonProperty("authAmount")]
		public decimal? AuthAmount { get; set; }

		[JsonProperty("settleAmount")]
		public decimal? SettleAmount { get; set; }

		[JsonProperty("taxExempt")]
		public bool? TaxExempt { get; set; }

		[JsonProperty("payment")]
		public Payment Payment { get; set; }

		[JsonProperty("billTo")]
		public BillTo BillTo { get; set; }

		[JsonProperty("recurringBilling")]
		public bool? RecurringBilling { get; set; }

		[JsonProperty("customerIP")]
		public string CustomerIP { get; set; }

		[JsonProperty("product")]
		public string Product { get; set; }

		[JsonProperty("marketType")]
		public string MarketType { get; set; }

		[JsonProperty("networkTransId")]
		public string NetworkTransId { get; set; }

		[JsonProperty("authorizationIndicator")]
		public string AuthorizationIndicator { get; set; }

		[JsonProperty("tapToPhone")]
		public bool? TapToPhone { get; set; }
	}
	#endregion

	#region Order
	public class Order
	{
		[JsonProperty("invoiceNumber")]
		public string InvoiceNumber { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("discountAmount")]
		public decimal? DiscountAmount { get; set; }

		[JsonProperty("taxIsAfterDiscount")]
		public bool? TaxIsAfterDiscount { get; set; }
	}
	#endregion

	#region Payment
	public class Payment
	{
		[JsonProperty("creditCard")]
		public CreditCardDetails CreditCard { get; set; }
	}

	public class CreditCardDetails
	{
		[JsonProperty("cardNumber")]
		public string CardNumber { get; set; }

		[JsonProperty("expirationDate")]
		public string ExpirationDate { get; set; }

		[JsonProperty("cardType")]
		public string CardType { get; set; }
	}
	#endregion

	#region Billing
	public class BillTo
	{
		[JsonProperty("firstName")]
		public string FirstName { get; set; }

		[JsonProperty("lastName")]
		public string LastName { get; set; }

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
}
