using BenchmarkDotNet.Running;

namespace Geohash.GeoRaptor.Benchmarks
{
	/// <summary>
	/// Provides the entry point for executing benchmark suites in this assembly.
	/// </summary>
	public static class Program
	{
		/// <summary>
		/// Runs all benchmarks discovered in the current assembly.
		/// </summary>
		/// <param name="args">Optional BenchmarkDotNet command-line arguments.</param>
		public static void Main(string[] args)
		{
			BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
		}
	}
}
