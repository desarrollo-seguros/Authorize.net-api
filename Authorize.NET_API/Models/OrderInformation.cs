namespace Authorize.NET_API.Models
{
	public class OrderInformation
	{
		private string invoiceNumber = string.Empty;
		private string description = string.Empty;

		public string InvoiceNumber
		{
			get => this.invoiceNumber;
			set => this.invoiceNumber = value;
		}

		public string Description
		{
			get => this.description;
			set => this.description = value;
		}
	}
}