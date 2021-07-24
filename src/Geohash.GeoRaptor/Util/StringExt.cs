using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geohash.GeoRaptor.Util
{
	internal static class StringExt
	{
		public static string Truncate(this string self, int maxLength)
		{
			if (string.IsNullOrEmpty(self)) return self;
			return self.Length <= maxLength ? self : self.Substring(0, maxLength);
		}

		public static string RemoveLast(this string self)
		{
			if (self.Length < 1) return self;
			return self.Remove(self.Length - 1);
		}
	}
}
