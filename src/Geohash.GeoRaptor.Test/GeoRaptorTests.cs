using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Geohash.GeoRaptor.Tests
{
    public class GeoRaptorTests
	{
		HashSet<string> _geoHashes;

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
			Assert.AreEqual(414, result.Count());
		}

		[Test]
		public void Test_Level_4To5()
		{
			var result = GeoRaptor.Compress(_geoHashes, 4, 5);
			Assert.AreEqual(50, result.Count());
		}

		[Test]
		public void Test_Level_3To4()
		{
			var result = GeoRaptor.Compress(_geoHashes, 3, 4);
			Assert.AreEqual(5, result.Count());
		}

		[Test]
		public void Test_Level_2To6()
		{
			var result = GeoRaptor.Compress(_geoHashes, 2, 6);
			Assert.AreEqual(414, result.Count());
		}

		[Test]
		public void Test_Level_3To4_Compare_Sets()
		{
			var expectedResult = new HashSet<string>
			{
				"w23b","w238","w21x","w21z","w21w"
			};

			var actualResult = GeoRaptor.Compress(_geoHashes, 3, 4);

			Assert.IsTrue(actualResult.SetEquals(expectedResult));
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

			Assert.IsTrue(actualResult.SetEquals(expectedResult));
		}
	}
}