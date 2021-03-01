using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchEngine.Crawler
{
	class Crawler
	{
		public List<Term> DocumentFileTerms { get; private set; } = new List<Term>();


		public void Crawl(string path, string documentName)
		{
			string documentFile = File.ReadAllText(path);
			Regex regex = new Regex("[^a-zA-Z0-9]");
			documentFile = regex.Replace(documentFile, " ");
			List<string> words = documentFile.Split(' ').ToList();
			List<string> uniqueWords = words.Distinct().ToList();

			for (int i = 0; i < uniqueWords.Count; i++)
			{
				Document document = new Document() { Url = $"https:{ documentName }.com ", Name = documentName, Occurence = 1 };
				List<Document> documents = new List<Document>();
				Term term = new Term() { Name = uniqueWords[i], Documents = documents };

				for (int j = 0; j < words.Count; j++)
				{
					if (uniqueWords[i].Equals(words[j]))
					{
						if (term.Documents.Contains(document))
						{
							document.Occurence++;
						}
						else
						{
							term.Documents.Add(document);
							DocumentFileTerms.Add(term);
						}
					}
				}
			}
		}
	}
}
