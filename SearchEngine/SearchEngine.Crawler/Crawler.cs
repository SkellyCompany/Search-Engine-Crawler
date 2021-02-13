using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchEngine.Crawler
{
	class Crawler
	{
		public List<DocumentFile> DocumentFiles { get; private set; } = new List<DocumentFile>();


		public void Crawl(string path, string documentName)
		{
			string document = File.ReadAllText(path);
			Regex regex = new Regex("[^a-zA-Z0-9]");
			document = regex.Replace(document, " ");
			List<string> words = document.Split(' ').ToList();
			words.Distinct().ToList();
			DocumentFile documentFile = new DocumentFile() { FileName = documentName, Terms = words.Distinct().ToList() };
			DocumentFiles.Add(documentFile);
		}
	}
}
