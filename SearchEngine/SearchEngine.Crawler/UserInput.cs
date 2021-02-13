using System;
using System.Collections.Generic;

namespace SearchEngine.Crawler
{
	class UserInput
	{

		public void Initialize()
		{
			Console.WriteLine("--------------------------------");
			Console.WriteLine("Press any key to start fetching terms");
			Console.WriteLine("--------------------------------");
			Console.ReadLine();

			Console.WriteLine();
			Console.WriteLine("Searching for terms . . .");
			Console.WriteLine();

			CrawlerManager crawlerManager = new CrawlerManager();
			List<Crawler> crawlers = crawlerManager.CreateCrawlers(1);
			crawlerManager.StartCrawlers();

			List<string> uniqueTerms = crawlers[0].Terms;
			for (int i = 0; i < uniqueTerms.Count; i++)
			{
				Console.WriteLine($"{i}: {uniqueTerms[i]}");
			}
		}
	}
}
