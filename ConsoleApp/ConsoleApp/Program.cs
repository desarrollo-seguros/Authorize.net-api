using ME.Mexicard;
using System;
using System.IO;

namespace ConsoleApp
{
	public class Program
	{
		static void Main(string[] args)
		{
			GetInvoiceData();
			//ChargeCreditCard();
			Console.ReadLine();
		}


		static void GetInvoiceData()
		{
			clAuthorizeInvoice authInvoice = new clAuthorizeInvoice();

			authInvoice.isTest = "False";
			authInvoice.netTransid = "121399121582";

			//authInvoice.isTest = "True";
			//authInvoice.netTransid = "80049821986";
			
			if (authInvoice.GenerateInvoice())
			{
				string fileName = $"Invoice_{authInvoice.netTransid}.pdf";
				string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

				File.WriteAllBytes(outputPath, authInvoice.pdfBytes);

				Console.WriteLine("PDF generado en:");
				Console.WriteLine(outputPath);

			}
			else
			{
				Console.WriteLine("Error generate invoice");
				Console.WriteLine(authInvoice.msg);
			}

			//authInvoice.GetTransactionDetails();
			//authInvoice.GetMerchantDetails();
		}

		static void ChargeCreditCard()
		{
			clAuthorizeCC cc = new clAuthorizeCC();

			cc.isTest = "True";

			cc.address = "reforma 522";
			cc.amount = 120;
			cc.CCcode = "000";
			cc.CCExpDate = "012026";
			cc.CCno = "4111 1111 1111 1111".Trim();
			cc.city = "los aneles";
			cc.country = "USA";
			cc.firstName = "eric";
			cc.lastName = "diaz";
			cc.orderDescription = "Autos policyNo p20251229143701";
			cc.state = "CA";
			cc.zip = "99270";
			cc.invoiceNo = "p20251229143701";

			cc.AuthorizePayment();

			Console.WriteLine(cc.plainResponse);
			Console.WriteLine();
			Console.WriteLine(cc.msg);
		}
	}
}
