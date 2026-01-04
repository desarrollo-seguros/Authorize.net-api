using Authorize.NET_API.Models;
using System;
using System.Linq;

namespace Authorize.NET_API
{
	public interface IAuthorizeResponse
	{
		Messages Messages { get; }
	}

	#region AuthorizeResponseValidator
	public class AuthorizeValidationResult
	{
		public bool IsSuccess { get; set; }
		public AuthorizeResultCode ResultCode { get; set; }
		public string ErrorCode { get; set; }
		public string ErrorMessage { get; set; }
	}

	public static class AuthorizeResponseValidator
	{
		public static AuthorizeValidationResult Validate(IAuthorizeResponse response)
		{
			// Seguridad básica
			if (response?.Messages == null)
			{
				return new AuthorizeValidationResult
				{
					IsSuccess = false,
					ResultCode = AuthorizeResultCode.Unknown,
					ErrorMessage = "Respuesta sin nodo messages."
				};
			}

			var resultCode = response.Messages.ResultCode;

			// OK
			if (string.Equals(resultCode, "Ok", StringComparison.OrdinalIgnoreCase))
			{
				return new AuthorizeValidationResult
				{
					IsSuccess = true,
					ResultCode = AuthorizeResultCode.Ok
				};
			}

			// ERROR
			if (string.Equals(resultCode, "Error", StringComparison.OrdinalIgnoreCase))
			{
				var firstError = response.Messages.Message?.FirstOrDefault();

				return new AuthorizeValidationResult
				{
					IsSuccess = false,
					ResultCode = AuthorizeResultCode.Error,
					ErrorCode = firstError?.Code,
					ErrorMessage = firstError?.Text
				};
			}

			// Caso inesperado
			return new AuthorizeValidationResult
			{
				IsSuccess = false,
				ResultCode = AuthorizeResultCode.Unknown,
				ErrorMessage = $"ResultCode no reconocido: {resultCode}"
			};
		}
	}
	#endregion
}
