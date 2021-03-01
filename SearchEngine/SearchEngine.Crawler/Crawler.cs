using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchEngine.Crawler
{
	class Crawler
	{
		public List<Document> DocumentFiles { get; private set; } = new List<Document>();


		public void Crawl(string path, string documentName)
		{
			string documentFile = File.ReadAllText(path);
			Regex regex = new Regex("[^a-zA-Z0-9]");
			documentFile = regex.Replace(documentFile, " ");
			List<string> words = documentFile.Split(' ').ToList();
			List<string> uniqueWords = words.Distinct().ToList();
			List<Term> terms = new List<Term>();
			for (int i = 0; i < uniqueWords.Count; i++)
			{
				int occurence = 0;
				for (int j = 0; j < words.Count; j++)
				{
					if (words[j].Equals(uniqueWords[i]))
					{
						occurence++;
					}
				}
				Term term = new Term() { Name = uniqueWords[i], Occurence = occurence };
				terms.Add(term);
			}
			Document document = new Document() { Name = documentName, Terms = terms };
			DocumentFiles.Add(document);
		}
	}
}
