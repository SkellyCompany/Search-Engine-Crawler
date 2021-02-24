using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace SearchEngine.Crawler
{
	class Client
	{
		private IMongoCollection<BsonDocument> _termCollection;
		private IMongoCollection<BsonDocument> _documentCollection;

		public bool IsConnected { get; private set; }


		public void Connect()
		{
			try
			{
				MongoClient client = new MongoClient("mongodb+srv://admin:skelly@cluster0.7ncjo.mongodb.net/SearchEngine?retryWrites=true&w=majority");
				IMongoDatabase database = client.GetDatabase("SearchEngine");
				_termCollection = database.GetCollection<BsonDocument>("Term");
				_documentCollection = database.GetCollection<BsonDocument>("Document");
				IsConnected = true;
			}
			catch (ArgumentOutOfRangeException e)
			{
				throw e;
			}
		}

		public int IndexDocuments(List<DocumentFile> documentFiles)
		{
			List<BsonDocument> bsonDocuments = new List<BsonDocument>();
			for (int i = 0; i < documentFiles.Count; i++)
			{
				BsonDocument bsonDocument = new BsonDocument
				{
					{ "url",  $"https://{ documentFiles[i].FileName }.com"  },
					{ "name", documentFiles[i].FileName },
				};
				BsonDocument foundbsonDocument = _documentCollection.Find(bsonDocument).FirstOrDefault();
				if (foundbsonDocument == null)
				{
					bsonDocuments.Add(bsonDocument);
				}
			}

			if (bsonDocuments.Count > 0)
			{
				_documentCollection.InsertMany(bsonDocuments);
			}
			return bsonDocuments.Count;
		}

		public int IndexTerms(List<DocumentFile> documentFiles)
		{
			List<BsonDocument> insertBsonDocuments = new List<BsonDocument>();
			List<BsonDocument> updateBsonDocuments = new List<BsonDocument>();
			for (int i = 0; i < documentFiles.Count; i++)
			{
				for (int j = 0; j < documentFiles[i].Terms.Count; j++)
				{
					BsonArray bsonArray = new BsonArray
					{
						new BsonDocument { { "url", $"https://{ documentFiles[i].FileName }.com" }, { "occurence", 1 }, { "docId", "" } },
					};
					BsonDocument bsonDocument = new BsonDocument
					{
						{ "value", documentFiles[i].Terms[j] },
						{ "documents", bsonArray }
					};
					BsonDocument foundbsonDocument = _termCollection.Find(bsonDocument).FirstOrDefault();
					if (foundbsonDocument == null)
					{
						insertBsonDocuments.Add(bsonDocument);
					}
					else
					{
						//var filter = new BsonDocument();
						//var update = new BsonDocument("$set", new BsonDocument("documents", bsonArray));
						//_termCollection.UpdateMany(filter, update);
						//updateBsonDocuments.Add(bsonDocument);
					}
				}
			}

			if (insertBsonDocuments.Count > 0)
			{
				_termCollection.InsertMany(insertBsonDocuments);
			}
			if (updateBsonDocuments.Count > 0)
			{
				//var filter = new BsonDocument();
				//var update = new BsonDocument("$set", new BsonDocument("documents", t));
				//_termCollection.UpdateMany(filter, update);
			}
			return insertBsonDocuments.Count;
		}
	}
}
