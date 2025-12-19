using System;

namespace Authorize.NET_API.Models
{
	public class Transaction
	{
		private Decimal amount = 0.0M;

		public MerchantAuthentication Merchant { get; set; }

		public CreditCard CreditCard { get; set; }

		public Address Address { get; set; }

		public OrderInformation OrderInformation { get; set; }

		public Decimal Amount
		{
			get => this.amount;
			set => this.amount = value;
		}
	}
}