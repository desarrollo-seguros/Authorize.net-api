using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Authorize.NET_API.Models;
using iTextSharp.text.pdf;

namespace AuthorizeNet.Pdf
{
	internal class pdfBase
	{
		#region All Variables
		private TransactionDetailsResponse transaction;
		private MerchantDetailsResponse merchant;
		/*---------- Edición de PDF ----------*/
		private List<PDF_Accesors> pdfList;
		#endregion

		internal pdfBase(TransactionDetailsResponse t, MerchantDetailsResponse m)
		{
			this.transaction = t;
			this.merchant = m;
		}

		#region PDF Edition
		public byte[] Invoice()
		{
			pdfList = new List<PDF_Accesors>();

			// Merchant Section
			string mName = this.merchant.MerchantName;
			string mAddress = this.merchant.BusinessInformation.Address;
			SplitAddress(mAddress, out string address1, out string address2);
			string mCity = this.merchant.BusinessInformation.City;
			string mState = this.merchant.BusinessInformation.State;
			string mZIP = this.merchant.BusinessInformation.Zip;
			string mCiStZ = $"{mCity}, {mState} {mZIP}";
			string mCountry = this.merchant.BusinessInformation.Country;
			string mPhone = this.merchant.BusinessInformation.PhoneNumber;
			pdfList.Add(new PDF_Accesors { _fieldName = "txt_merchantName", _Value = mName });
			pdfList.Add(new PDF_Accesors { _fieldName = "txt_merchantAddressLine1", _Value = address1 });
			pdfList.Add(new PDF_Accesors { _fieldName = "txt_merchantAddressLine2", _Value = address2 });
			pdfList.Add(new PDF_Accesors { _fieldName = "txt_merchantCitySateZIP", _Value = mCiStZ });
			pdfList.Add(new PDF_Accesors { _fieldName = "txt_merchantCountry", _Value = mCountry });
			pdfList.Add(new PDF_Accesors { _fieldName = "txt_merchantPhone", _Value = mPhone });

			// Order Section
			string ordDescription = this.transaction.TransactionDetails.Order.Description;
			string ordInvoice = this.transaction.TransactionDetails.Order.InvoiceNumber;
			pdfList.Add(new PDF_Accesors { _fieldName = "txt_description", _Value = ordDescription });
			pdfList.Add(new PDF_Accesors { _fieldName = "txt_invoice", _Value = ordInvoice });

			// Billing Information Section
			string iName = this.transaction.TransactionDetails.BillTo.FirstName;
			string iLast = this.transaction.TransactionDetails.BillTo.LastName;
			string insName = $"{iName} {iLast}";
			string insAddress = this.transaction.TransactionDetails.BillTo.Address;
			string iCity = this.transaction.TransactionDetails.BillTo.City;
			string iState = this.transaction.TransactionDetails.BillTo.State;
			string iZIP = this.transaction.TransactionDetails.BillTo.Zip;
			string insCiStZ = $"{iCity}, {iState} {iZIP}";
			string insCountry = this.transaction.TransactionDetails.BillTo.Country;
			pdfList.Add(new PDF_Accesors { _fieldName = "txt_insuredName", _Value = insName });
			pdfList.Add(new PDF_Accesors { _fieldName = "txt_insuredAddress", _Value = insAddress });
			pdfList.Add(new PDF_Accesors { _fieldName = "txt_insuredCitySateZIP", _Value = insCiStZ });
			pdfList.Add(new PDF_Accesors { _fieldName = "txt_insuredCountry", _Value = insCountry });

			// Amount Section
			decimal? amount = this.transaction.TransactionDetails.AuthAmount;
			string total = amount.HasValue && amount.Value > 0 ? $"USD {amount.Value:0.00}" : "USD 0.00";
			string zero = "USD 0.00";
			pdfList.Add(new PDF_Accesors { _fieldName = "txt_shipping", _Value = zero });
			pdfList.Add(new PDF_Accesors { _fieldName = "txt_duty", _Value = zero });
			pdfList.Add(new PDF_Accesors { _fieldName = "txt_tax", _Value = zero });
			pdfList.Add(new PDF_Accesors { _fieldName = "txt_total", _Value = total });

			// Payment Information Section
			string trnsDate = this.transaction.TransactionDetails.SubmitTimeUTC?
				.ToString("dd-MMM-yyyy HH:mm:ss 'PST'", CultureInfo.InvariantCulture);
			string trnsId = this.transaction.TransactionDetails.TransId;
			string trnsType = this.transaction.TransactionDetails.TransactionTypeDescription;
			string trnsStatus = this.transaction.TransactionDetails.TransactionStatusDescription;
			string trnsAuth = this.transaction.TransactionDetails.AuthCode;
			string cardNumber = this.transaction.TransactionDetails.Payment.CreditCard.CardNumber;
			string cardType = this.transaction.TransactionDetails.Payment.CreditCard.CardType;
			string trnsPayment = $"{cardType} {cardNumber}";
			pdfList.Add(new PDF_Accesors { _fieldName = "txt_date", _Value = trnsDate });
			pdfList.Add(new PDF_Accesors { _fieldName = "txt_transactionID", _Value = trnsId });
			pdfList.Add(new PDF_Accesors { _fieldName = "txt_transactionType", _Value = trnsType });
			pdfList.Add(new PDF_Accesors { _fieldName = "txt_transactionStatus", _Value = trnsStatus });
			pdfList.Add(new PDF_Accesors { _fieldName = "txt_authorization", _Value = trnsAuth });
			pdfList.Add(new PDF_Accesors { _fieldName = "txt_payment", _Value = trnsPayment });

			//FillPDFInsured(this.pdfTemplate, _fileName);
			return GeneratePdfBytes();
		}
		#endregion

		#region PDF Helpers Methods
		private byte[] GeneratePdfBytes()
		{
			using (var templateStream = GetTemplateStream())
			using (var reader = new PdfReader(templateStream))
			using (var outputStream = new MemoryStream())
			{
				using (var stamper = new PdfStamper(reader, outputStream))
				{
					CompleteFields(stamper);
					stamper.FormFlattening = true;
				}

				return outputStream.ToArray();
			}
		}
		#endregion

		#region Private Helpers Methods
		private PdfStamper CompleteFields(PdfStamper _stamper)
		{
			for (int i = 0; i < pdfList.Count; i++)
			{
				_stamper.AcroFields.SetField(
					pdfList[i]._fieldName, pdfList[i]._Value);
			}

			return _stamper;
		}

		private static void SplitAddress(string address, out string address1, out string address2)
		{
			address1 = string.Empty;
			address2 = string.Empty;

			if (string.IsNullOrWhiteSpace(address))
				return;

			var parts = address.Split(new[] { ',' }, 2, StringSplitOptions.RemoveEmptyEntries);

			address1 = parts[0].Trim();

			if (parts.Length > 1)
				address2 = parts[1].Trim();
		}

		private Stream GetTemplateStream()
		{
			return PdfTemplateProvider.GetTemplateStream();
		}

		#endregion
	}

	internal class PDF_Accesors
	{
		public string _fieldName { get; set; }
		public string _Value { get; set; }
	}
}
