using MongoDB.Bson;
using MongoDB.Driver;
using SearchEngine.Crawler.Domain;
using SearchEngine.UI.Crawler;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchEngine.Crawler.Infrastructure {
	class Client {
		private IMongoDatabase _database;
		private IMongoCollection<BsonDocument> _termCollection;
		private IMongoCollection<BsonDocument> _documentCollection;

		public bool IsConnected { get; private set; }


		public void Connect() {
			try {
				MongoClient client = new MongoClient("mongodb+srv://admin:skelly@cluster0.7ncjo.mongodb.net/SearchEngine?retryWrites=true&w=majority");
				_database = client.GetDatabase("SearchEngine");
				_termCollection = _database.GetCollection<BsonDocument>("Term");
				_documentCollection = _database.GetCollection<BsonDocument>("Document");
				IsConnected = true;
			} catch (ArgumentOutOfRangeException e) {
				throw e;
			}
		}

		public int IndexDocuments(List<Document> documentFiles) {
			List<BsonDocument> bsonDocuments = new List<BsonDocument>();
			for (int i = 0; i < documentFiles.Count; i++) {
				BsonDocument bsonDocument = new BsonDocument
				{
					{ "url",  $"https://{ documentFiles[i].Name }.com"  },
					{ "name", documentFiles[i].Name },
				};
				BsonDocument foundbsonDocument = _documentCollection.Find(bsonDocument).FirstOrDefault();
				if (foundbsonDocument == null) {
					bsonDocuments.Add(bsonDocument);
				}
			}

			if (bsonDocuments.Count > 0) {
				_documentCollection.InsertMany(bsonDocuments);
			}
			return bsonDocuments.Count;
		}

		public int IndexTerms(List<Term> terms) {
			ProgressBar.Initialize(0, terms.Count);
			_database.DropCollection("Term");
			List<BsonDocument> insertBsonDocuments = new List<BsonDocument>();
			for (int i = 0; i < terms.Count; i++) {
				BsonArray bsonArray = new BsonArray();
				for (int j = 0; j < terms[i].Documents.Count; j++) {
					bsonArray.Add(new BsonDocument { { "url", $"https://{ terms[i].Documents[j].Name }.com" }, { "occurrences", terms[i].Documents[j].Occurence }, { "docId", "" } });
				}
				BsonDocument bsonDocument = new BsonDocument
				{
					{ "value", terms[i].Value},
					{ "documents", bsonArray}
				};
				insertBsonDocuments.Add(bsonDocument);
				ProgressBar.Progress();
			}

			if (insertBsonDocuments.Count > 0) {
				_termCollection.InsertMany(insertBsonDocuments);
			}
			return insertBsonDocuments.Count;
		}
	}
}
