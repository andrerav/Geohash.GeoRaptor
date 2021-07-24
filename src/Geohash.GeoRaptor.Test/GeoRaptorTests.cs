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
			if (!File.Exists(filename))
			{
				filename = "./Geohash.GeoRaptor.Test/sample.csv"; // Hack for .NET 4.8 / 4.7.x
			}
			_geoHashes = new HashSet<string>(File.ReadAllLines(filename));
		}

		[Test]
		public void Test01()
		{
			var result = GeoRaptor.Compress(_geoHashes, 5, 6);
			Assert.AreEqual(414, result.Count());
		}

		[Test]
		public void Test02()
		{
			var result = GeoRaptor.Compress(_geoHashes, 4, 5);
			Assert.AreEqual(50, result.Count());
		}

		[Test]
		public void Test03()
		{
			var result = GeoRaptor.Compress(_geoHashes, 3, 4);
			Assert.AreEqual(5, result.Count());
		}
	}
}