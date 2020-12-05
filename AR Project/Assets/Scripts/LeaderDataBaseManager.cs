using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        Debug.Log("started");
        //SaveScoreToDataBase("cwll", 10);
        
        //GetScoresFromDataBase();
    }

    public async void SaveScoreToDataBase(string username, int score)
    {
        BsonDocument document = new BsonDocument { {"username", username }, {"score", score }, {"city", GPS.Instance.city } };

        await collection.InsertOneAsync(document);
    }

    public async Task<List<PlayerScore>> GetScoresFromDataBase()
    {
        FilterDefinition<BsonDocument> filter = new BsonDocument("city", "SLC");
        var localScores = await collection.FindAsync(filter);
        

        List<PlayerScore> highScores = new List<PlayerScore>();
        foreach (var score in localScores.ToList())
        {
            highScores.Add(Deserialize(score));
        }

        return highScores;
    }

    private PlayerScore Deserialize(BsonDocument raw)
    {
        PlayerScore playerScore = new PlayerScore();

        playerScore.UserName = raw[1].ToString();
        playerScore.Score = raw[2].ToInt32();
        playerScore.City = raw[3].ToString();

        return playerScore;
    }
}
[Serializable]
public class PlayerScore
{
    public string UserName { get; set; }
    public int Score { get; set; }
    public string City { get; set; }
}
