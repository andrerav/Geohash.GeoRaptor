using System;
using System.IO;
using System.Collections.Generic;
using CommandLine;
using NetTopologySuite.Geometries;

namespace Geohash.GeoRaptor.CLI
{
    static class Program
	{
        static int exitCode = 0;
        static HashSet<string> geohashes = new HashSet<string>();
        static Lazy<Geohasher> Geohasher = new Lazy<Geohasher>((() => new Geohasher()));

        static void Main(string[] args)
		{
            CommandLine.Parser.Default.ParseArguments<CommandLineOptions>(args)
			.WithParsed(o =>
            {
                ValidateArguments(o);

                if (exitCode > 0) { return; }

                var reader = GetInputReader(o);

                ValidateReader(reader);

                if (exitCode > 0) { return; }

                LoadGeohashes(reader, geohashes);

                var compressed = CompressGeohashes(o);

                OutputCompressedGeohashes(o, compressed);
            });
		}

        /// <summary>
        /// Compress the geohashes using the specified precision range.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private static HashSet<string> CompressGeohashes(CommandLineOptions o)
        {
            return GeoRaptor.Compress(geohashes, o.MinimumPrecision, o.MaximumPrecision);
        }

        /// <summary>
        /// Outputs compressed geohashes to standard output
        /// </summary>
        /// <param name="o"></param>
        /// <param name="compressed"></param>
        private static void OutputCompressedGeohashes(CommandLineOptions o, HashSet<string> compressed)
        {

            var outputGeometry = o.OutputGeometry.HasValue && o.OutputGeometry.Value;
            if (outputGeometry && o.AddHeaders == true)
            {
                Console.WriteLine("geohash" + o.Separator + "geometry");
            }
            foreach (var g in compressed)
            {
                if (outputGeometry)
                {
                    var geometry = BoundingBox(g);
                    Console.WriteLine(g + o.Separator + geometry.AsText());
                }
                else
                {
                    Console.WriteLine(g);
                }
            }
        }

        /// <summary>
        /// Loads geohashes from the reader and stores them in the HashSet.
        /// It is assumed that each line contains a single geohash.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="geohashes"></param>
        private static void LoadGeohashes(TextReader reader, HashSet<string> geohashes)
        {
            string geohash;
            while ((geohash = reader.ReadLine()) != null)
            {
                geohashes.Add(geohash);
            }
        }

        /// <summary>
        /// Validate that there is input on the reader
        /// </summary>
        /// <param name="reader"></param>
        private static void ValidateReader(TextReader reader)
        {
            if (reader == null)
            {
                WriteError("Unable to read input, exiting");
                exitCode = 1;
            }
            else if (reader.Peek() == -1)
            {
                WriteError("No input detected");
                exitCode = 1;
            }
        }

        /// <summary>
        /// Validates the arguments specified by the user
        /// </summary>
        /// <param name="o"></param>
        private static void ValidateArguments(CommandLineOptions o)
        {
            if (o.MaximumPrecision > 12 || o.MaximumPrecision < 1)
            {
                WriteError("Maximum precision must be <= 12 and >= 1");
                exitCode = 1;
            }
            else if (o.MinimumPrecision > 12 || o.MinimumPrecision < 1)
            {
                WriteError("Minimum precision must be <= 12 and >= 1");
                exitCode = 1;
            }
            else if (o.MinimumPrecision > o.MaximumPrecision)
            {
                WriteError($"Minimum precision must be <= {o.MaximumPrecision}");
                exitCode = 1;
            }

            if (!string.IsNullOrWhiteSpace(o.InputFile) && !File.Exists(o.InputFile))
            {
                WriteError("Input file does not exist");
                exitCode = 1;
            }
        }

        /// <summary>
        /// Writes an error message to stderr
        /// </summary>
        /// <param name="message"></param>
        private static void WriteError(string message)
        {
            Console.Error.WriteLine("Error: " + message);
        }

        /// <summary>
        /// Returns an input reader based on whether the user has specified an input file or not
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private static TextReader GetInputReader(CommandLineOptions o)
        {
            TextReader reader;
            if (!string.IsNullOrWhiteSpace(o.InputFile))
            {

                reader = File.OpenText(o.InputFile);
            }
            else
            {
                reader = Console.In;
            }

            return reader;
        }

        /// <summary>
        /// Converts a valid geohash to its corresponding bounding box
        /// </summary>
        /// <param name="geohash"></param>
        /// <returns></returns>
		public static Polygon BoundingBox(string geohash)
		{
			var bb = Geohasher.Value.GetBoundingBox(geohash);

			var coordinates = new List<Coordinate>();
			coordinates.Add(new Coordinate(bb[3], bb[0]));
			coordinates.Add(new Coordinate(bb[3], bb[1]));
			coordinates.Add(new Coordinate(bb[2], bb[1]));
			coordinates.Add(new Coordinate(bb[2], bb[0]));
			coordinates.Add(new Coordinate(bb[3], bb[0]));

			var linearRing = new LinearRing(coordinates.ToArray());
			var polygon = new Polygon(linearRing);
			return polygon;
		}
	}
}
