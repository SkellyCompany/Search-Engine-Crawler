using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SearchEngine.Crawler.Domain;
using SearchEngine.UI.Crawler;

namespace SearchEngine.Crawler {
	class CrawlerManager {
		private List<Crawler> _crawlers = new List<Crawler>();


		public int StartCrawlers() {
			string mainPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
			string documentsPath = Path.GetFullPath(Path.Combine(mainPath, @"..\..\..\..\..\..\..\documents\"));
			//string[] documentFiles = Directory.GetFiles(documentsPath, "*.*", SearchOption.AllDirectories);
			string[] documentFiles = Directory.GetFiles(documentsPath);
			ProgressBar.Initialize(0, documentFiles.Length);
			for (int i = 0; i < _crawlers.Count; i++) {
				for (int j = 0; j < documentFiles.Length; j++) {
					_crawlers[i].Crawl(documentFiles[j], Path.GetFileName(documentFiles[j]));
					ProgressBar.Progress();
				}
			}
			return documentFiles.Length;
		}

		public List<Crawler> CreateCrawlers(int amount) {
			_crawlers = new List<Crawler> { new Crawler() };
			for (int i = 1; i < amount; i++) {
				_crawlers.Add(new Crawler());
			}
			return _crawlers;
		}

		public List<Term> CrawlersResult() {
			List<Term> documentFilesTerms = _crawlers[0].Terms;
			ProgressBar.Initialize(0, documentFilesTerms.Count);
			for (int i = 0; i < documentFilesTerms.Count; i++) {
				for (int j = 0; j < documentFilesTerms.Count; j++) {
					if (documentFilesTerms[i].Value.Equals(documentFilesTerms[j].Value)
						&& documentFilesTerms[i].Documents[0].Name != documentFilesTerms[j].Documents[0].Name) {
						documentFilesTerms[i].Documents.AddRange(documentFilesTerms[j].Documents);
						documentFilesTerms.Remove(documentFilesTerms[j]);
					}
					ProgressBar.Update(0, documentFilesTerms.Count);
				}
				ProgressBar.Progress();
			}
			return documentFilesTerms;
		}
	}
}
