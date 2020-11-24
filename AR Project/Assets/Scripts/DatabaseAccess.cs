using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseAccess : MonoBehaviour
{
    MongoClient client = new MongoClient("mongodb+srv://Cwill:DataExpressData@cluster0.4ygyx.mongodb.net/5minus1?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;
    void Start()
    {
        database = client.GetDatabase("5minus1");
        collection = database.GetCollection<BsonDocument>("scoreBoard");
    }

    void Update()
    {
        
    }
}
