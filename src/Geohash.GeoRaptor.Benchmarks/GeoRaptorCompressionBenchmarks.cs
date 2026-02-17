using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;

namespace Geohash.GeoRaptor.Benchmarks
{
	/// <summary>
	/// Measures compression throughput and memory usage for <see cref="GeoRaptor.Compress"/> across different input sizes.
	/// </summary>
	[MemoryDiagnoser]
	public class GeoRaptorCompressionBenchmarks
	{
		private static readonly char[] Base32Chars = "0123456789bcdefghjkmnpqrstuvwxyz".ToCharArray();
		private HashSet<string> _sourceGeohashes = new HashSet<string>();

		/// <summary>
		/// Gets or sets the number of geohashes generated for the benchmark input set.
		/// </summary>
		[Params(16384)]
		public int InputSize { get; set; }

		/// <summary>
		/// Gets or sets the minimum precision allowed in the compressed output.
		/// </summary>
		[Params(3)]
		public int MinimumPrecision { get; set; }

		/// <summary>
		/// Gets or sets the maximum precision allowed in the compressed output.
		/// </summary>
		[Params(6)]
		public int MaximumPrecision { get; set; }

		/// <summary>
		/// Creates a deterministic input set for each benchmark parameter combination.
		/// </summary>
		[GlobalSetup]
		public void GlobalSetup()
		{
			var seed = InputSize * 31 + MinimumPrecision * 17 + MaximumPrecision;
			_sourceGeohashes = GenerateRandomGeohashes(seed, InputSize, MinimumPrecision, MaximumPrecision + 2);
		}

		/// <summary>
		/// Executes geohash compression for a prepared input set.
		/// </summary>
		/// <returns>The number of geohashes in the compressed result.</returns>
		[Benchmark]
		public int Compress()
		{
			var workingSet = new HashSet<string>(_sourceGeohashes);
			var result = GeoRaptor.Compress(workingSet, MinimumPrecision, MaximumPrecision);
			return result.Count;
		}

		/// <summary>
		/// Generates deterministic pseudo-random geohashes using valid base32 geohash characters.
		/// </summary>
		/// <param name="seed">Seed used by the random generator.</param>
		/// <param name="count">Number of unique geohashes to generate.</param>
		/// <param name="minLength">Minimum geohash length in the generated set.</param>
		/// <param name="maxLength">Maximum geohash length in the generated set.</param>
		/// <returns>A set containing generated geohashes.</returns>
		private static HashSet<string> GenerateRandomGeohashes(int seed, int count, int minLength, int maxLength)
		{
			var random = new Random(seed);
			var result = new HashSet<string>();

			while (result.Count < count)
			{
				var length = random.Next(minLength, maxLength + 1);
				var geohashChars = new char[length];
				for (var i = 0; i < length; i++)
				{
					geohashChars[i] = Base32Chars[random.Next(Base32Chars.Length)];
				}

				result.Add(new string(geohashChars));
			}

			return result;
		}
	}
}
