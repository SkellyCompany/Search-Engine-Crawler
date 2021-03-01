using System.Collections.Generic;

namespace SearchEngine.Crawler
{
	class Document
	{
		public string Name { get; set; }
		public List<Term> Terms { get; set; }
	}
}
