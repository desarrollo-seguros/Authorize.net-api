using Newtonsoft.Json;
using System.Collections.Generic;

namespace Authorize.NET_API.Models
{
	#region Messages
	public class Messages
	{
		[JsonProperty("resultCode")]
		public string ResultCode { get; set; }

		[JsonProperty("message")]
		public List<MessageItem> Message { get; set; }
	}

	public class MessageItem
	{
		[JsonProperty("code")]
		public string Code { get; set; }

		[JsonProperty("text")]
		public string Text { get; set; }
	}
	#endregion
}
