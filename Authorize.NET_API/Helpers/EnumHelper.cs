using System;
using System.ComponentModel;
using System.Reflection;

namespace Authorize.NET_API
{
	public static class EnumHelper
	{
		public static string GetDescription(Enum value)
		{
			if (value == null) return null;

			var field = value.GetType().GetField(value.ToString());
			if (field == null) return value.ToString();

			var attr = field.GetCustomAttribute<DescriptionAttribute>();
			return attr?.Description ?? value.ToString();
		}

		public static string GetEnumDescription<TEnum>(string value) where TEnum : struct, Enum
		{
			if (string.IsNullOrWhiteSpace(value))
				return EnumHelper.GetDescription(
					(Enum) Enum.Parse(typeof(TEnum), "Unknown"));

			if (Enum.TryParse<TEnum>(value, true, out var enumValue))
				return EnumHelper.GetDescription(enumValue as Enum);

			return EnumHelper.GetDescription(
				(Enum) Enum.Parse(typeof(TEnum), "Unknown"));
		}
	}
}
