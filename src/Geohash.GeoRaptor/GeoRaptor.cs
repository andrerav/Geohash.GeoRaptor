using Geohash.GeoRaptor.Util;
using System;
using System.Collections.Generic;

namespace Geohash.GeoRaptor
{
	/// <summary>
	/// Geohash.GeoRaptor is a geohash compression library for efficiently reducing the size of large geohash collections.
	/// </summary>
	public static class GeoRaptor
	{
		private static readonly char[] Base32Chars = "0123456789bcdefghjkmnpqrstuvwxyz".ToCharArray();

		/// <summary>
		/// Checks whether all 32 direct child geohashes for a given parent exist in the current working set.
		/// </summary>
		/// <param name="geohashes">Current working set used for compression checks.</param>
		/// <param name="parent">Parent geohash to evaluate.</param>
		/// <returns><c>true</c> when all direct children are present; otherwise <c>false</c>.</returns>
		private static bool HasAllChildren(HashSet<string> geohashes, string parent)
		{
			foreach (var c in Base32Chars)
			{
				if (!geohashes.Contains(parent + c))
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Compresses the input set of geohashes within the given minimum and maximum precision levels.
		/// </summary>
		/// <param name="geohashes">A set of geohashes. Varying precision levels are ok.</param>
		/// <param name="minimumPrecision">The minimum precision level to maintain for the compressed output.</param>
		/// <param name="maximumPrecision">The maximum precision level to maintain for the compressed output.</param>
		/// <returns>A compressed set of geohashes.</returns>
		public static HashSet<string> Compress(HashSet<string> geohashes, int minimumPrecision, int maximumPrecision)
		{
			if (geohashes is null)
			{
				throw new ArgumentNullException(nameof(geohashes));
			}

			var removed = new HashSet<string>(geohashes.Comparer);
			var parentCompressionCache = new Dictionary<string, bool>(geohashes.Comparer);
			var currentResult = new HashSet<string>(geohashes.Comparer);
			var temporaryResult = new HashSet<string>(geohashes.Comparer);

			foreach (var geohash in geohashes)
			{
				currentResult.Add(geohash.Truncate(maximumPrecision));
			}

			if (currentResult.Count == 0)
			{
				return geohashes;
			}

			while (true)
			{
				temporaryResult.Clear();
				removed.Clear();
				parentCompressionCache.Clear();

				var anyCompression = false;
				foreach (var geohash in currentResult)
				{
					var currentGeohashPrecision = geohash.Length;

					// Compress only if geohash length is greater than the min level.
					if (currentGeohashPrecision > minimumPrecision)
					{
						var parent = geohash.RemoveLast();

						// Skip geohashes already accounted for in this iteration.
						if (!removed.Contains(parent) && !removed.Contains(geohash))
						{
							if (!parentCompressionCache.TryGetValue(parent, out var canCompressParent))
							{
								canCompressParent = HasAllChildren(currentResult, parent);
								parentCompressionCache[parent] = canCompressParent;
							}

							if (canCompressParent)
							{
								temporaryResult.Add(parent);
								removed.Add(parent);
								anyCompression = true;
							}
							else
							{
								removed.Add(geohash);
								temporaryResult.Add(geohash);
							}
						}
					}
					else
					{
						temporaryResult.Add(geohash);
					}
				}

				if (!anyCompression || temporaryResult.SetEquals(currentResult))
				{
					geohashes.Clear();
					geohashes.UnionWith(temporaryResult);
					return geohashes;
				}

				var swap = currentResult;
				currentResult = temporaryResult;
				temporaryResult = swap;
			}
		}
	}
}
