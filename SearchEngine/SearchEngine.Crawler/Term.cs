using System.Collections.Generic;

namespace SearchEngine.Crawler
{
	class Term
	{
		public string Name { get; set; }
		public List<Document> Documents { get; set; }
	}
}
