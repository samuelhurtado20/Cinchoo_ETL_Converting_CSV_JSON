using System;
using System.IO;
using ChoETL;

namespace Cinchoo_ETL_Converting_CSV_JSON
{
	public class Program
	{
		static readonly string path = Path.Combine(Directory.GetCurrentDirectory(), @"MyCsvFile.csv");
		static void Main(string[] args)
		{
			string csv = ReadFile(path, false);

			Conversion(csv);

			WriteLineByLine(path);

			// Keep the console window open in debug mode.
			Console.WriteLine("Press any key to exit.");
			System.Console.ReadKey();
		}

		private static void Conversion(string csv)
		{
			Guid guid = Guid.NewGuid();
			using var w = new ChoJSONWriter(Path.Combine(Directory.GetCurrentDirectory(), $"{guid.ToString()}-json.json"))
				.Configure(c => c.DefaultArrayHandling = false);
			using var r = ChoCSVReader.LoadText(csv).WithFirstLineHeader()
					.Configure(c => c.NestedColumnSeparator = '/');
			w.Write(r);
		}

		private static string ReadFile(string path, bool write)
		{

			// The files used in this example are created in the topic
			// How to: Write to a Text File. You can change the path and
			// file name to substitute text files of your own.

			// Example #1
			// Read the file as one string.
			string text = File.ReadAllText(path);

			if(write)
			{
				// Display the file contents to the console. Variable text is a string.
				System.Console.WriteLine($"Contents of {path} = {text}");
			}

			return text;
		}

		private static void WriteLineByLine(string path)
		{
			// Example #2
			// Read each line of the file into a string array. Each element
			// of the array is one line of the file.
			string[] lines = System.IO.File.ReadAllLines(path);

			// Display the file contents by using a foreach loop.
			System.Console.WriteLine($"Contents of {path} = ");
			foreach (string line in lines)
			{
				// Use a tab to indent each line of the file.
				Console.WriteLine("\n" + line.Trim());
			}

			// Write a blank line
			Console.WriteLine("Console.WriteLine(" + @"\n" + ")");
			Console.WriteLine("\n");
			// With @ we can write literally (ex: \n)
			Console.WriteLine(@"\n");
			Console.WriteLine($"\n");
		}
	}
}
