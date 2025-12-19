namespace Authorize.NET_API.Models
{
	public class CreditCard
	{
		private string number = string.Empty;
		private string expirationDate = string.Empty;
		private string code = string.Empty;

		public string Number
		{
			get => this.number;
			set => this.number = value;
		}

		public string ExpirationDate
		{
			get => this.expirationDate;
			set => this.expirationDate = value;
		}

		public string Code
		{
			get => this.code;
			set => this.code = value;
		}
	}
}