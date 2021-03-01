using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SearchEngine.Crawler
{
	class UserInput
	{
		public void Initialize()
		{
			Console.WriteLine("Press enter to start the indexing process");
			Console.WriteLine("--------------------------------");
			Console.ReadLine();
			Console.WriteLine("Searching for all unique terms in documents . . .");
			Console.WriteLine();

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			CrawlerManager crawlerManager = new CrawlerManager();
			List<Crawler> crawlers = crawlerManager.CreateCrawlers(1);
			crawlerManager.StartCrawlers();
			List<Document> documentFiles = crawlers[0].DocumentFiles;
			int totalUniqueTermsFound = 0;
			for (int i = 0; i < documentFiles.Count; i++)
			{
				totalUniqueTermsFound += documentFiles[i].Terms.Count;
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine($"Document_{i}: {documentFiles[i].Name}");
				Console.ForegroundColor = ConsoleColor.Yellow;
				for (int j = 0; j < documentFiles[i].Terms.Count; j++)
				{
					Console.WriteLine($"Term_{j}: {documentFiles[i].Terms[j].Name} | Occurence: {documentFiles[i].Terms[j].Occurence}");
				}
				Console.WriteLine();
			}
			stopwatch.Stop();

			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("--------------------------------");
			Console.WriteLine("Finished searching");
			Console.Write("Total document files found: ");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine(documentFiles.Count);
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("Total unique terms found: ");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(totalUniqueTermsFound);
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("Elapsed time: ");
			Console.WriteLine(stopwatch.ElapsedMilliseconds);
			Console.WriteLine("--------------------------------");
			Console.WriteLine();

			Console.ReadLine();
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
			stopwatch.Start();
			int documentsIndexedAmount = client.IndexDocuments(documentFiles);
			int termsIndexedAmount = client.IndexTerms(documentFiles);
			stopwatch.Stop();
			Console.WriteLine("--------------------------------");
			Console.WriteLine("Finished indexing");
			Console.Write("Total document files indexed: ");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"{documentsIndexedAmount}/{documentFiles.Count}");
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("Total unique terms indexed: ");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine($"{termsIndexedAmount}/{totalUniqueTermsFound}");
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("Elapsed time: ");
			Console.WriteLine(stopwatch.ElapsedMilliseconds);
			Console.WriteLine("--------------------------------");
			Console.WriteLine();
			Console.WriteLine("Press enter to exit");
			Console.ReadLine();
		}
	}
}
