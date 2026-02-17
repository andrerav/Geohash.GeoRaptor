using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Geohash.GeoRaptor.Tests
{
	public class GeoRaptorTests
	{
		private static readonly char[] Base32Chars = "0123456789bcdefghjkmnpqrstuvwxyz".ToCharArray();
		private HashSet<string> _geoHashes;

		[SetUp]
		public void Setup()
		{
			var filename = "sample.csv";
			var sampleFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, filename);
			_geoHashes = new HashSet<string>(File.ReadAllLines(sampleFilePath));
		}

		[Test]
		public void Test_Level_5To6()
		{
			var result = GeoRaptor.Compress(_geoHashes, 5, 6);
			Assert.That(result.Count(), Is.EqualTo(414));
		}

		[Test]
		public void Test_Level_4To5()
		{
			var result = GeoRaptor.Compress(_geoHashes, 4, 5);
			Assert.That(result.Count(), Is.EqualTo(50));
		}

		[Test]
		public void Test_Level_3To4()
		{
			var result = GeoRaptor.Compress(_geoHashes, 3, 4);
			Assert.That(result.Count(), Is.EqualTo(5));
		}

		[Test]
		public void Test_Level_2To6()
		{
			var result = GeoRaptor.Compress(_geoHashes, 2, 6);
			Assert.That(result.Count(), Is.EqualTo(414));
		}

		[Test]
		public void Test_Level_3To4_Compare_Sets()
		{
			var expectedResult = new HashSet<string>
			{
				"w23b","w238","w21x","w21z","w21w"
			};

			var actualResult = GeoRaptor.Compress(_geoHashes, 3, 4);

			Assert.That(actualResult, Is.EquivalentTo(expectedResult));
		}

		[Test]
		public void Test_Level_4To5_Compare_Sets()
		{
			var geohashes = new HashSet<string>
			{
				"w21zf9", "w21zfc", "w21zg1",
				"w21zg3", "w21zg9", "w21zgc2",
				"w21zu1", "w21zu3", "w21zu9",
				"w21zuc", "w21zv1", "w21zv3",
				"w21zv9", "w21zvc", "w21zy1",
				"w21zy3", "w21zy9", "w21zyc",
				"w21xy8", "w21xyb", "w21xz0"
			};

			var expectedResult = new HashSet<string> 
			{
				"w21zf", "w21zg", "w21zu",
				"w21zv", "w21zy", "w21xy",
				"w21xz" 
			};

			var actualResult = GeoRaptor.Compress(geohashes, 4, 5);

			Assert.That(actualResult, Is.EquivalentTo(expectedResult));
		}

		[Test]
		public void Test_Does_Not_Compress_Below_Minimum_Precision()
		{
			var geohashes = BuildChildren("w2");

			var actualResult = GeoRaptor.Compress(geohashes, 3, 6);

			Assert.That(actualResult, Is.EquivalentTo(geohashes));
		}

		[Test]
		public void Test_Null_Input_Throws()
		{
			Assert.That(() => GeoRaptor.Compress(null!, 3, 6), Throws.TypeOf<ArgumentNullException>());
		}

		[Test]
		public void Test_Empty_Input_Returns_Empty_Result()
		{
			var geohashes = new HashSet<string>();

			var result = GeoRaptor.Compress(geohashes, 3, 6);

			Assert.That(result, Is.Empty);
			Assert.That(ReferenceEquals(result, geohashes), Is.True);
		}

		[Test]
		public void Test_Empty_String_Geohash_Is_Preserved()
		{
			var geohashes = new HashSet<string> { string.Empty, "w21zf9" };

			var result = GeoRaptor.Compress(geohashes, 1, 6);

			Assert.That(result, Does.Contain(string.Empty));
		}

		[Test]
		public void Test_Invalid_Characters_Are_Handled_Without_Throwing()
		{
			var geohashes = new HashSet<string> { "w21@9", "w21#8" };
			var expectedResult = new HashSet<string>(geohashes.Select(x => Truncate(x, 4)));

			var result = GeoRaptor.Compress(geohashes, 3, 4);

			Assert.That(result, Is.EquivalentTo(expectedResult));
		}

		[Test]
		public void Test_Minimum_Precision_Greater_Than_Maximum_Precision_Still_Respects_Maximum()
		{
			var geohashes = new HashSet<string> { "w21zf9", "w21zfc" };
			var expectedResult = new HashSet<string> { "w21z" };

			var result = GeoRaptor.Compress(geohashes, 6, 4);

			Assert.That(result, Is.EquivalentTo(expectedResult));
		}

		[Test]
		public void Test_Negative_Maximum_Precision_Throws()
		{
			var geohashes = new HashSet<string> { "w21zf9" };

			Assert.That(() => GeoRaptor.Compress(geohashes, 3, -1), Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[Test]
		public void Test_Negative_Minimum_Precision_Is_Handled()
		{
			var geohashes = new HashSet<string> { "w21zf9" };

			var result = GeoRaptor.Compress(geohashes, -1, 5);

			Assert.That(result, Is.EquivalentTo(new[] { "w21zf" }));
		}

		[Test]
		public void Test_Single_Element_Input_Remains_Stable()
		{
			var geohashes = new HashSet<string> { "w21zf9" };

			var result = GeoRaptor.Compress(geohashes, 3, 6);

			Assert.That(result, Is.EquivalentTo(new[] { "w21zf9" }));
		}

		[Test]
		public void Test_Full_Sibling_Block_At_Maximum_Compresses_To_Parent()
		{
			var geohashes = BuildChildren("w21z");

			var result = GeoRaptor.Compress(geohashes, 4, 5);

			Assert.That(result, Is.EquivalentTo(new[] { "w21z" }));
		}

		[Test]
		public void Test_Mixed_Precision_Input_Compresses_Only_Eligible_Blocks()
		{
			var geohashes = BuildChildren("w21x");
			geohashes.Add("w21z");
			geohashes.Add("w21bb7");

			var result = GeoRaptor.Compress(geohashes, 4, 5);

			Assert.That(result, Is.EquivalentTo(new[] { "w21x", "w21z", "w21bb" }));
		}

		[Test]
		public void Test_Compress_Mutates_Input_Set_And_Returns_Same_Instance()
		{
			var geohashes = BuildChildren("w21z");

			var result = GeoRaptor.Compress(geohashes, 4, 5);

			Assert.That(ReferenceEquals(result, geohashes), Is.True);
			Assert.That(geohashes, Is.EquivalentTo(new[] { "w21z" }));
		}

		[Test]
		public void Test_Compress_Is_Idempotent_On_Realistic_Data()
		{
			var input = new HashSet<string>(_geoHashes);
			var firstPass = GeoRaptor.Compress(input, 4, 5);
			var secondPass = GeoRaptor.Compress(new HashSet<string>(firstPass), 4, 5);

			Assert.That(secondPass, Is.EquivalentTo(firstPass));
		}

		[Test]
		public void Test_Randomized_Data_Remains_Bounded_And_Idempotent()
		{
			for (var seed = 1; seed <= 25; seed++)
			{
				var input = GenerateRandomValidGeohashes(seed, 200, 3, 8);
				var normalizedInput = new HashSet<string>(input.Select(x => Truncate(x, 6)));
				var firstPass = GeoRaptor.Compress(new HashSet<string>(input), 3, 6);
				var secondPass = GeoRaptor.Compress(new HashSet<string>(firstPass), 3, 6);

				Assert.That(firstPass, Is.EquivalentTo(secondPass), $"Idempotence failed for seed {seed}.");
				Assert.That(firstPass.All(x => x.Length >= 3 && x.Length <= 6), Is.True, $"Precision bounds failed for seed {seed}.");
				Assert.That(firstPass.Count, Is.LessThanOrEqualTo(normalizedInput.Count), $"Set grew unexpectedly for seed {seed}.");
			}
		}

		[Test]
		[CancelAfter(5000)]
		public void Test_Large_Dataset_Compresses_Within_Expected_Output_Size()
		{
			var geohashes = new HashSet<string>();
			var parents = new HashSet<string>();
			var parentSeeds = Base32Chars.Take(8).ToArray();
			foreach (var c1 in parentSeeds)
			{
				foreach (var c2 in parentSeeds)
				{
					var parent = $"w{c1}{c2}k";
					parents.Add(parent);
					geohashes.UnionWith(BuildChildren(parent));
				}
			}

			var result = GeoRaptor.Compress(geohashes, 4, 5);

			Assert.That(result, Is.EquivalentTo(parents));
		}

		private static HashSet<string> BuildChildren(string parent)
		{
			return new HashSet<string>(Base32Chars.Select(c => $"{parent}{c}"));
		}

		private static HashSet<string> GenerateRandomValidGeohashes(int seed, int count, int minLength, int maxLength)
		{
			var random = new Random(seed);
			var results = new HashSet<string>();
			while (results.Count < count)
			{
				var length = random.Next(minLength, maxLength + 1);
				var chars = Enumerable.Range(0, length)
					.Select(_ => Base32Chars[random.Next(Base32Chars.Length)])
					.ToArray();
				results.Add(new string(chars));
			}

			return results;
		}

		private static string Truncate(string value, int maxLength)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}

			return value.Length <= maxLength
				? value
				: value.Substring(0, maxLength);
		}
	}
}
