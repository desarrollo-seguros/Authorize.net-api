namespace Authorize.NET_API.Constants
{
	public static class Endpoint
	{
		private static Endpoint.EndPointData sandbox = new Endpoint.EndPointData()
		{
			Url = "https://apitest.authorize.net/xml/v1/request.api",
			//Developer User
			ApiLoginId = "6x7NyN6A",
			TransactionKey = "78p6q84Xb3sL7aHj"

			//eric@creatsol.com User
			//ApiLoginId = "544Bs5zzHVc",
			//TransactionKey = "4yr82m3L9eEX872K"
		};
		private static Endpoint.EndPointData production = new Endpoint.EndPointData()
		{
			Url = "https://api.authorize.net/xml/v1/request.api",
			ApiLoginId = "macafee14",
			TransactionKey = "9zZcW7572rk69TEX"
		};

		public static Endpoint.EndPointData Sandbox => Endpoint.sandbox;

		public static Endpoint.EndPointData Production => Endpoint.production;

		public class EndPointData
		{
			private string url = string.Empty;
			private string apiLoginId = string.Empty;
			private string transactionKey = string.Empty;

			public string Url { get; set; }

			public string ApiLoginId { get; set; }

			public string TransactionKey { get; set; }
		}
	}
}