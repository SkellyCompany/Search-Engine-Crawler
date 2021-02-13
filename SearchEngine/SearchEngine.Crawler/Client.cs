using MongoDB.Driver;

namespace SearchEngine.Crawler
{
	class Client
	{
		public void Connect()
		{
			MongoClient client = new MongoClient("mongodb+srv://admin:skelly@cluster0.7ncjo.mongodb.net/SearchEngine?retryWrites=true&w=majority");
		}
	}
}
