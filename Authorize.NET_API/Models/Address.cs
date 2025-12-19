namespace Authorize.NET_API.Models
{
	public class Address
	{
		private string firstName = string.Empty;
		private string lastName = string.Empty;
		private string addressName = string.Empty;
		private string city = string.Empty;
		private string state = string.Empty;
		private string zip = string.Empty;
		private string country = string.Empty;

		public string FirstName
		{
			get => this.firstName;
			set => this.firstName = value;
		}

		public string LastName
		{
			get => this.lastName;
			set => this.lastName = value;
		}

		public string AddressName
		{
			get => this.addressName;
			set => this.addressName = value;
		}

		public string City
		{
			get => this.city;
			set => this.city = value;
		}

		public string State
		{
			get => this.state;
			set => this.state = value;
		}

		public string Zip
		{
			get => this.zip;
			set => this.zip = value;
		}

		public string Country
		{
			get => this.country;
			set => this.country = value;
		}
	}
}