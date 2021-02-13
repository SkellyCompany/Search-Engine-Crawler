using System;
using System.Net;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using System.IO;

namespace SearchEngineCrawler
{
	class Crawler
	{
		private static Thread thread;
		private static string _searchWord;
		private static int _maximumLevel;
		private int _currentLinkAmount;
		private bool _hasFinishedCrawling;


		public Crawler()
		{
			thread = new Thread(new ThreadStart(Crawl));
			thread.Start();
		}

		public Crawler(string searchWord, Queue<Uri> urls, int maximumLevel) : this()
		{
			//_searchWord = searchWord;
			//_maximumLevel = maximumLevel;
			//foreach (Uri url in urls)
			//{
			//	_frontier.Add(new KeyValuePair<Uri, int>(url, 0));
			//}
		}

		private void Crawl()
		{

		}
	}
}
