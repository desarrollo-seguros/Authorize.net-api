using Authorize.NET_API;
using Authorize.NET_API.Constants;
using Authorize.NET_API.Models;
using AuthorizeNet.Pdf;
using Newtonsoft.Json;
using System;

namespace ME.Mexicard
{
	public class clAuthorizeInvoice
	{
		public string msg;
		public string isTest;
		public string netTransid;
		public TransactionDetailsResponse transactionResponse;
		public MerchantDetailsResponse merchantResponse;
		public byte[] pdfBytes;

		public clAuthorizeInvoice()
		{
			this.isTest = "False";
			this.transactionResponse = null;
			this.merchantResponse = null;
			this.pdfBytes = null;
		}

		public bool GenerateInvoice()
		{
			this.msg = string.Empty;

			if (string.IsNullOrEmpty(netTransid))
			{
				this.msg = "netTransId is required";
				return false;
			}

			if (!GetMerchantDetails())
				return false;

			if (!GetTransactionDetails())
				return false;

			pdfBase pdf = new pdfBase(transactionResponse, merchantResponse);

			try
			{
				this.pdfBytes = pdf.Invoice();

				return true;
			}
			catch (Exception ex)
			{
				this.msg = ex.Message;
				return false;
			}
		}

		private bool GetTransactionDetails()
		{
			Endpoint.EndPointData endPointData = this.isTest == "False" ? Endpoint.Production : Endpoint.Sandbox;

			TransactionDetailsRequest request = new TransactionDetailsRequest()
			{
				Merchant = new MerchantAuthentication()
				{
					ApiLoginId = endPointData.ApiLoginId,
					TransactionKey = endPointData.TransactionKey
				},
				TransactionId = netTransid
			};

			this.msg = string.Empty;

			try
			{
				string plainResponse = AuthorizeApi.GetTransactionDetails(endPointData.Url, request);
				Console.WriteLine(plainResponse);

				this.transactionResponse = JsonConvert.DeserializeObject<TransactionDetailsResponse>(plainResponse);

				var validation = AuthorizeResponseValidator.Validate(this.transactionResponse);

				if (!validation.IsSuccess)
				{
					// Log completo
					this.msg = $"Authorize Error [{validation.ErrorCode}]: {validation.ErrorMessage}";
					return false;
				}
				// Aquí ya es OK
				// transactionResponse.TransactionDetails contiene la información válida

				return true;
			}
			catch (JsonException jex)
			{
				// JSON inválido o tipos incompatibles
				this.msg = "Error parseando respuesta de Authorize: " + jex.Message;
				return false;
			}
			catch (Exception ex)
			{
				this.msg = ex.Message;
				return false;
			}
		}

		private bool GetMerchantDetails()
		{
			Endpoint.EndPointData endPointData = this.isTest == "False" ? Endpoint.Production : Endpoint.Sandbox;

			MerchantDetailsRequest request = new MerchantDetailsRequest()
			{
				Merchant = new MerchantAuthentication()
				{
					ApiLoginId = endPointData.ApiLoginId,
					TransactionKey = endPointData.TransactionKey
				}
			};

			this.msg = string.Empty;

			try 
			{ 
				string plainResponse = AuthorizeApi.GetMerchantDetails(endPointData.Url, request);
				Console.WriteLine(plainResponse);

				this.merchantResponse = JsonConvert.DeserializeObject<MerchantDetailsResponse>(plainResponse);

				var validation = AuthorizeResponseValidator.Validate(this.merchantResponse);

				if (!validation.IsSuccess)
				{
					this.msg = $"Authorize Error [{validation.ErrorCode}]: {validation.ErrorMessage}";
					return false;
				}

				// ✔ OK
				// this.merchantResponse.MerchantDetails contiene los datos válidos
				return true;
			}
			catch (JsonException jex)
			{
				this.msg = "Error parseando respuesta de Authorize: " + jex.Message;
				return false;
			}
			catch (Exception ex)
			{
				this.msg = ex.Message;
				return false;
			}
		}
	}
}
