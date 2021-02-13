using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace SearchEngine.Crawler
{
	class Crawler
	{
		public List<string> Terms { get; private set; } = new List<string>();


		public void Crawl(string path)
		{
			string document = File.ReadAllText(path);
			Regex regex = new Regex("[^a-zA-Z0-9]");
			document = regex.Replace(document, " ");
			List<string> words = document.Split(' ').ToList();
			Terms = words.Distinct().ToList();
		}
	}
}
