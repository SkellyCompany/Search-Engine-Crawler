using System.Collections.Generic;

namespace SearchEngine.Crawler.Domain {
	class Term {
		public string Value { get; set; }
		public List<Document> Documents { get; set; }
	}
}
