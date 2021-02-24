using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace SearchEngine.Crawler
{
	class Client
	{
		private IMongoCollection<BsonDocument> _collection;

		public bool IsConnected { get; private set; }


		public void Connect()
		{
			try
			{
				MongoClient client = new MongoClient("mongodb+srv://admin:skelly@cluster0.7ncjo.mongodb.net/SearchEngine?retryWrites=true&w=majority");
				IMongoDatabase database = client.GetDatabase("SearchEngine");
				_collection = database.GetCollection<BsonDocument>("Term");
				IsConnected = true;
			}
			catch (ArgumentOutOfRangeException e)
			{
				throw e;
			}
		}

		public void IndexDocuments(List<DocumentFile> documentFiles)
		{
			List<BsonDocument> bsonDocuments = new List<BsonDocument>();
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					BsonDocument bsonDocument = new BsonDocument
					{
						{ "value", documentFiles[i].Terms[j] },
						{
							"documents",
							new BsonArray
							{
								new BsonDocument { { "url", "example.com" }, { "occurence", 1 }, { "docId", "" } },
							}
						}
					};
					bsonDocuments.Add(bsonDocument);
				}
			}
			_collection.InsertMany(bsonDocuments);
		}
	}
}
