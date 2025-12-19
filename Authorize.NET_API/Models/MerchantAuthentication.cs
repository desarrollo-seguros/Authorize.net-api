namespace Authorize.NET_API.Models
{
	public class MerchantAuthentication
	{
		private string apiLoginId = string.Empty;
		private string transactionKey = string.Empty;

		public string ApiLoginId
		{
			get => this.apiLoginId;
			set => this.apiLoginId = value;
		}

		public string TransactionKey
		{
			get => this.transactionKey;
			set => this.transactionKey = value;
		}
	}
}