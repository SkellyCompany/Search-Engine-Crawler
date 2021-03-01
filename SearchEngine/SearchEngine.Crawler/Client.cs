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

		public int IndexDocuments(List<Document> documentFiles)
		{
			List<BsonDocument> bsonDocuments = new List<BsonDocument>();
			for (int i = 0; i < documentFiles.Count; i++)
			{
				BsonDocument bsonDocument = new BsonDocument
				{
					{ "url",  $"https://{ documentFiles[i].Name }.com"  },
					{ "name", documentFiles[i].Name },
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

		public int IndexTerms(List<Document> documents)
		{
			List<BsonDocument> insertBsonDocuments = new List<BsonDocument>();
			List<BsonDocument> updateBsonDocuments = new List<BsonDocument>();
			List<BsonDocument> incrementBsonDocuments = new List<BsonDocument>();
			for (int i = 0; i < documents.Count; i++)
			{
				for (int j = 0; j < documents[i].Terms.Count; j++)
				{
					BsonDocument arrayBsonDocument = new BsonDocument { { "url", $"https://{ documents[i].Name }.com" }, { "occurence", documents[i].Terms[j].Occurence }, { "docId", "" } };
					BsonArray bsonArray = new BsonArray
					{
						arrayBsonDocument
					};
					BsonDocument bsonDocument = new BsonDocument
					{
						{ "name", documents[i].Terms[j].Name },
						{ "documents", bsonArray }
					};
					FilterDefinition<BsonDocument> termNameFilter = Builders<BsonDocument>.Filter.Eq("name", documents[i].Terms[j].Name);
					BsonDocument foundbsonDocument = _termCollection.Find(termNameFilter).FirstOrDefault();
					if (foundbsonDocument == null)
					{
						Console.WriteLine("a");
						insertBsonDocuments.Add(bsonDocument);
					}
					else
					{
						//var filter = Builders<BsonDocument>.Filter.Eq("name", documents[i].Terms[j].Name);
						//var update = Builders<BsonDocument>.Update.Set("documents.0.occurence", documents[i].Terms[j].Occurence);
						//_termCollection.UpdateOne(filter, update);

						var filter = Builders<BsonDocument>.Filter.Eq("name", documents[i].Terms[j].Name);

						//_termCollection.Update(qu
						//_termCollection.UpdateOne(filter, update); bool doesContainArrayElement = foundbsonDocument.GetElement(2).Value.AsBsonArray.Contains(arrayBsonDocument);
						//if (!doesContainArrayElement)
						//{
						//	Console.WriteLine("b");
						//	updateBsonDocuments.Add(bsonDocument);
						//	//var filter = new BsonDocument();
						//	//var update = new BsonDocument("$set", new BsonDocument("documents", bsonArray));
						//	//_termCollection.UpdateMany(filter, update);
						//	//updateBsonDocuments.Add(bsonDocument);
						//}
						//else
						//{
						//	Console.WriteLine(documents[i].Terms[j].Name + "|" + documents[i].Terms[j].Occurence);
						//	var filter = Builders<BsonDocument>.Filter.Eq("name", documents[i].Terms[j].Name);
						//	var update = Builders<BsonDocument>.Update.Set("documents.0.occurence", documents[i].Terms[j].Occurence);
						//	_termCollection.UpdateOne(filter, update);
						//}
					}
				}
			}

			if (insertBsonDocuments.Count > 0)
			{
				_termCollection.InsertMany(insertBsonDocuments);
			}
			if (updateBsonDocuments.Count > 0)
			{

			}
			if (incrementBsonDocuments.Count > 0)
			{

			}
			return insertBsonDocuments.Count;
		}
	}
}
