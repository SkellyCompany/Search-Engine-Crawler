using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SearchEngine.Crawler
{
	class CrawlerManager
	{
		private List<Crawler> _crawlers = new List<Crawler>();
		public List<Term> DocumentFileTerms { get; private set; } = new List<Term>();


		public void StartCrawlers()
		{
			string mainPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
			string documentsPath = Path.GetFullPath(Path.Combine(mainPath, @"..\..\..\..\..\..\..\documents\"));
			string[] documentFiles = Directory.GetFiles(documentsPath);
			for (int i = 0; i < _crawlers.Count; i++)
			{
				for (int j = 0; j < documentFiles.Length; j++)
				{
					_crawlers[i].Crawl(documentFiles[j], Path.GetFileName(documentFiles[j]));
				}
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

		public List<Term> CrawlersResult()
		{
			List<Term> documentFilesTerms = _crawlers[0].DocumentFileTerms;
			for (int i = 0; i < documentFilesTerms.Count; i++)
			{
				for (int j = 0; j < documentFilesTerms.Count; j++)
				{
					if (documentFilesTerms[i].Name.Equals(documentFilesTerms[j].Name)
						&& documentFilesTerms[i].Documents[0].Name != documentFilesTerms[j].Documents[0].Name)
					{
						documentFilesTerms[i].Documents.AddRange(documentFilesTerms[j].Documents);
						documentFilesTerms.Remove(documentFilesTerms[j]);
					}
				}
			}
			return documentFilesTerms;
		}
	}
}
