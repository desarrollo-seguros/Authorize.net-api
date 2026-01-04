using System;
using System.IO;
using System.Reflection;

namespace AuthorizeNet.Pdf
{
	internal static class PdfTemplateProvider
	{
		// Cache en memoria (thread-safe por inicialización estática)
		private static readonly byte[] _templateBytes;

		// Nombre EXACTO del Embedded Resource
		private const string ResourceName =
			"ME.Mexicard.AuthorizeNet.Pdf.Templates.Transaction_Receipt.pdf";
			//"AuthorizeNet.Pdf.Templates.Transaction_Receipt.pdf";

		// Static constructor: se ejecuta UNA sola vez
		static PdfTemplateProvider()
		{
			var assembly = typeof(pdfBase).Assembly;

			using (var stream = assembly.GetManifestResourceStream(ResourceName))
			{
				if (stream == null)
				{
					throw new InvalidOperationException(
						$"PDF template not found as Embedded Resource: {ResourceName}"
					);
				}

				using (var ms = new MemoryStream())
				{
					stream.CopyTo(ms);
					_templateBytes = ms.ToArray();
				}
			}
		}

		/// <summary>
		/// Devuelve un Stream nuevo del template PDF (cacheado en memoria)
		/// </summary>
		public static Stream GetTemplateStream()
		{
			return new MemoryStream(_templateBytes, writable: false);
		}
	}
}
