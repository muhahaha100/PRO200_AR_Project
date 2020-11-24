using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderDataBaseManager : MonoBehaviour
{
    public MongoClient client = new MongoClient("mongodb+srv://Cwill:DataExpressData@cluster0.4ygyx.mongodb.net/5minus1?retryWrites=true&w=majority");
    public IMongoDatabase database;
    public IMongoCollection<BsonDocument> collection;
    void Start()
    {
        database = client.GetDatabase("5minus1");
        collection = database.GetCollection<BsonDocument>("scoreBoard");
    }

    public async void SaveScoreToDataBase(string username, int score, float lon, float lat)
    {
        BsonDocument document = new BsonDocument { { username, score, lat, lon} }
    }
}
