using Authorize.NET_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorize.NET_API.Models
{
	public class TransactionDetailsRequest
	{
		public MerchantAuthentication Merchant { get; set; }
		public string TransactionId { get; set; }
	}
}
