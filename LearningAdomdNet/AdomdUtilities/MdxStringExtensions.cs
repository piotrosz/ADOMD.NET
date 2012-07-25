using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdomdUtilities
{
	internal static class MdxStringExtensions
	{
		public static string Braces(this string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new Exception("MDX element value cannot be null or white space");

			if (value.Contains('[') || value.Contains(']'))
				throw new Exception("MDX element already contains square braces");

			return string.Format("[{0}]", value);
		}

		public static string JoinDots(this string[] values)
		{
			return string.Join(".", values);
		}

		public static string JoinComma(this string[] values)
		{
			return string.Join(",", values);
		}

		public static string Measures(this string value)
		{
			return new string[] { Braces("Measures"), Braces(value) }.JoinDots();
		}

		public static string Tuple(this string[] values)
		{
			return string.Format("({0})", values.JoinComma());
		}

		public static string Set(this string[] values)
		{
			return string.Format("{{{0}}}", values.JoinComma());
		}
	}
}
