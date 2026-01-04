using System;

namespace Authorize.NET_API
{
	public static class EnumMapper
	{
		public static TEnum ParseOrUnknown<TEnum>(string value) where TEnum : struct, Enum
		{
			if (Enum.TryParse<TEnum>(value, true, out var result))
				return result;

			if (Enum.TryParse<TEnum>("Unknown", out var unknown))
				return unknown;

			return default;
		}
	}
}