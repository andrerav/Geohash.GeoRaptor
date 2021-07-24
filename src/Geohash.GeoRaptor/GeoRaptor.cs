using Geohash.GeoRaptor.Util;
using System.Collections.Generic;
using System.Linq;

namespace Geohash.GeoRaptor
{
	public static class GeoRaptor
	{
		// Combination generator for a given geohash at the next level
		private static List<string> GetCombinations(string str)
		{
			var base32 = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "b", "c", "d", "e", "f", "g", 
											"h", "j", "k", "m", "n", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
			return (from i in base32
					select (str + $"{i}")).ToList();
		}

		// Recursive optimization of the geohash set
		public static HashSet<string> Compress(HashSet<string> geohashes, int minimumPrecision, int maximumPrecision)
		{
			var removed = new HashSet<string>();
			var temporaryResult = new HashSet<string>();
			var flag = true;
			var temporaryResultCount = 0;

			// Input size less than 32
			if (geohashes.Count == 0)
			{
				return geohashes;
			}
			while (flag)
			{
				temporaryResult.Clear();
				removed.Clear();
				foreach (var geohash in geohashes)
				{
					var currentGeohashPrecision = geohash.Count();

					// Compress only if geohash length is greater than the min level
					if (currentGeohashPrecision >= minimumPrecision)
					{
						// Reduce precision by one step for the current geohash. We will use this to generate possible combinations.
						var parent = geohash.RemoveLast();

						// Check that the current geohash has not already been compressed
						if (!removed.Contains(parent) && !removed.Contains(geohash))
						{
							// Generate combinations
							var combinations = new HashSet<string>(GetCombinations(parent));

							// If all generated combinations exist in the input set
							if (combinations.IsSubsetOf(geohashes))
							{
								// Add parent to output
								temporaryResult.Add(parent);

								// Add parent to the set of processed geohashes
								removed.Add(parent);
							}
							else
							{
								removed.Add(geohash);
								temporaryResult.Add(geohash.Truncate(maximumPrecision));
							}
							// Break if compressed output size same as the last iteration
							if (temporaryResultCount == temporaryResult.Count)
							{
								flag = false;
							}
						}
					}
				}

				temporaryResultCount = temporaryResult.Count;
				geohashes.Clear();
				geohashes.UnionWith(temporaryResult);
			}
			return geohashes;
		}
	}
}