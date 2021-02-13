using System.Collections.Generic;
using System.IO;

namespace SearchEngine.Crawler
{
	class CrawlerManager
	{
		private List<Crawler> _crawlers = new List<Crawler>();


		public void StartCrawlers()
		{
			string mainPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
			string path = Path.GetFullPath(Path.Combine(mainPath, @"..\..\..\..\..\..\..\files\TestDocument.txt"));
			for (int i = 0; i < _crawlers.Count; i++)
			{
				_crawlers[i].Crawl(path);
			}
		}

		public List<Crawler> CreateCrawlers(int amount)
		{
			_crawlers = new List<Crawler> { new Crawler() };
			for (int i = 1; i < amount; i++)
			{
				_crawlers.Add(new Crawler());
			}
			return _crawlers;
		}
	}
}
