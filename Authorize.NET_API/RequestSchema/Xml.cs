using System;

namespace Authorize.NET_API.RequestSchema
{
	internal static class Xml
	{
		public static string AuthenticateTestRequest(Authorize.NET_API.Models.MerchantAuthentication merchant)
		{
			string str = Xml.MerchantAuthentication(merchant);
			return $"{string.Empty}<authenticateTestRequest xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><merchantAuthentication>{str}</merchantAuthentication></authenticateTestRequest>";
		}

		public static string SettledBatchListRequest(
		  Authorize.NET_API.Models.MerchantAuthentication merchant,
		  DateTime from,
		  DateTime to)
		{
			string str = Xml.MerchantAuthentication(merchant);
			return $"{string.Empty}<getSettledBatchListRequest xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><merchantAuthentication>{str}</merchantAuthentication><includeStatistics>true</includeStatistics><firstSettlementDate>{from.ToString("yyyy-MM-ddThh:mm:ss")}Z </firstSettlementDate><lastSettlementDate>{to.ToString("yyyy-MM-ddThh:mm:ss")}Z</lastSettlementDate></getSettledBatchListRequest>";
		}

		private static string MerchantAuthentication(Authorize.NET_API.Models.MerchantAuthentication merchant)
		{
			return $"{string.Empty}<name>{merchant.ApiLoginId}</name><transactionKey>{merchant.TransactionKey}</transactionKey>";
		}
	}
}