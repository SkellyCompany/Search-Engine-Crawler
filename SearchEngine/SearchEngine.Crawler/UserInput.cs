using System;
using System.Collections.Generic;

namespace SearchEngine.Crawler
{
	class UserInput
	{
		public void Initialize()
		{
			Console.WriteLine("Press enter to start fetching terms from all documents");
			Console.WriteLine("--------------------------------");
			Console.ReadLine();
			Console.WriteLine("Searching for all unique terms in documents . . .");
			Console.WriteLine();

			CrawlerManager crawlerManager = new CrawlerManager();
			List<Crawler> crawlers = crawlerManager.CreateCrawlers(1);
			crawlerManager.StartCrawlers();
			List<DocumentFile> documentFiles = crawlers[0].DocumentFiles;
			int totalUniqueTermsFound = 0;
			for (int i = 0; i < documentFiles.Count; i++)
			{
				totalUniqueTermsFound += documentFiles[i].Terms.Count;
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine($"Document_{i}: {documentFiles[i].FileName}");
				Console.ForegroundColor = ConsoleColor.Yellow;
				for (int j = 0; j < documentFiles[i].Terms.Count; j++)
				{
					Console.WriteLine($"Term_{j}: {documentFiles[i].Terms[j]}");
				}
				Console.WriteLine();
			}

			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("--------------------------------");
			Console.WriteLine("Finished searching");
			Console.Write("Total unique terms found: ");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(totalUniqueTermsFound);
			Console.ForegroundColor = ConsoleColor.White;

			Console.WriteLine();
			Console.WriteLine("Press enter to exit");
			Console.ReadLine();
		}
	}
}
