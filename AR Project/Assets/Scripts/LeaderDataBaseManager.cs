using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LeaderDataBaseManager : MonoBehaviour
{

    public static LeaderDataBaseManager Instance { set; get; }

  

    public MongoClient client = new MongoClient("mongodb+srv://Cwill:DataExpressData@cluster0.4ygyx.mongodb.net/5minus1?retryWrites=true&w=majority");
    public IMongoDatabase database;
    public IMongoCollection<BsonDocument> collection;

    
    void Awake()
    {      
        Instance = this;
        DontDestroyOnLoad(gameObject);

        database = client.GetDatabase("5minus1");
        collection = database.GetCollection<BsonDocument>("scoreBoard");

    }

    public async void SaveScoreToDataBase(string username, int score)
    {
        BsonDocument document = new BsonDocument { {"username", username }, {"score", score }, {"city", GPS.Instance.City } };

        await collection.InsertOneAsync(document);
    }

    public async Task<List<PlayerScore>> GetScoresFromDataBase()
    {
        var localScores = await collection.FindAsync(FilterDefinition<BsonDocument>.Empty);
        

        List<PlayerScore> highScores = new List<PlayerScore>();
        foreach (var score in localScores.ToList())
        {
            highScores.Add(Deserialize(score));
        }

        return highScores;
    }

    public async Task<List<PlayerScore>> GetScoresFromDataBase(string city)
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
