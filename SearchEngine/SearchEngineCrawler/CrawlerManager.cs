using System;
using System.Collections.Generic;
using System.Text;

namespace SearchEngineCrawler
{
	class CrawlerManager
	{
		public List<Crawler> CreateCrawlers(string searchWord, Queue<Uri> initialQueue, int amount)
		{
			List<Crawler> crawlers = new List<Crawler> { new Crawler(searchWord, initialQueue, 5) };
			for (int i = 1; i < amount; i++)
			{
				crawlers.Add(new Crawler());
			}
			return crawlers;
		}

		public void StopCrawlers(List<Crawler> crawlers)
		{
			foreach (Crawler webCrawler in crawlers)
			{
				//webCrawler.Stop();
			}
		}
	}
}
