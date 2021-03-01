using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SearchEngine.Crawler
{
	class UserInput
	{
		public void Initialize()
		{
			Console.WriteLine("Press enter to start the indexing process");
			Console.WriteLine("(Press R to include results)");
			Console.WriteLine("--------------------------------");
			string input = Console.ReadLine();
			Console.WriteLine("Searching for all unique terms in documents . . .");
			Console.WriteLine();

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			CrawlerManager crawlerManager = new CrawlerManager();
			crawlerManager.CreateCrawlers(1);
			crawlerManager.StartCrawlers();
			List<Term> documentFilesTerms = crawlerManager.CrawlersResult();

			if (input.ToUpper().Equals("R"))
			{
				for (int i = 0; i < documentFilesTerms.Count; i++)
				{
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine($"Term_{i}: {documentFilesTerms[i].Name}");
					for (int j = 0; j < documentFilesTerms[i].Documents.Count; j++)
					{
						Console.WriteLine($"	Name: {documentFilesTerms[i].Documents[j].Name},");
						Console.WriteLine($"	Occurence: {documentFilesTerms[i].Documents[j].Occurence}");
						Console.WriteLine();
					}
				}
			}
			stopwatch.Stop();

			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("--------------------------------");
			Console.WriteLine("Finished searching");
			Console.Write("Total document files found: ");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine(documentFilesTerms.Count);
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("Total unique terms found: ");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(documentFilesTerms.Count);
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("Elapsed time: ");
			Console.WriteLine(stopwatch.Elapsed.ToString("mm\\:ss\\.ff"));
			Console.WriteLine("--------------------------------");
			Console.WriteLine();

			Client client = new Client();
			while (!client.IsConnected)
			{
				Console.WriteLine("Connecting to the database . . .");
				try
				{
					client.Connect();
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("Successfully connected to the database");
				}
				catch (Exception)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Failed to connect to the database");
				}
			}
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine();

			Console.WriteLine("Indexing data to the database . . .");
			Console.WriteLine();
			stopwatch.Start();
			client.IndexTerms(documentFilesTerms);
			stopwatch.Stop();
			Console.WriteLine("--------------------------------");
			Console.WriteLine("Finished indexing");
			Console.Write("Elapsed time: ");
			Console.WriteLine(stopwatch.Elapsed.ToString("mm\\:ss\\.ff"));
			Console.WriteLine("--------------------------------");
			Console.WriteLine();
			Console.WriteLine("Press enter to exit");
			Console.ReadLine();
		}
	}
}
