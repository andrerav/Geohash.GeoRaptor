using CommandLine;

namespace Geohash.GeoRaptor.CLI
{
    public class CommandLineOptions
	{
		[Option('f', "input-file", Required = false, HelpText = "Path to a text file with geohashes (one geohash per line)")]
		public string InputFile { get; set; }

		[Option('l', "lowest-precision", Required = true, HelpText = "Lowest precision")]
		public int MinimumPrecision { get; set; }

		[Option('h', "highest-precision", Required = true, HelpText = "Highest precision")]
		public int MaximumPrecision { get; set; }

		[Option('g', "output-geometry", Required = false, HelpText = "Enable this to add WKB geometries to the output")]
		public bool? OutputGeometry { get; set; }
	}
}
