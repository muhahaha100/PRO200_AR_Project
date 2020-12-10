using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PopulateBoard : MonoBehaviour
{
    [SerializeField] public GameObject prefab;
    private Task<List<PlayerScore>> scoresTask;
    public List<PlayerScore> scores;
    //public int numToCreate;

    private void Start()
    {
        scoresTask =  LeaderDataBaseManager.Instance.GetScoresFromDataBase();
        StartCoroutine(waitForScores());
    }

    private IEnumerator waitForScores()
    {
        yield return new WaitUntil(() => scoresTask.IsCompleted)  ;
        
        scores = scoresTask.Result;
        Populate();
        
    }

    private void Populate()
    {
        GameObject newObj;

        for (int i = 0; i < scores.Count; i++)
        {
            newObj = (GameObject)Instantiate(prefab, transform);
            LeaderBoardTile tile = newObj.GetComponent<LeaderBoardTile>();
            tile.City = scores[i].City;
            tile.Name = scores[i].UserName;
            tile.Score = scores[i].Score;
        }
    }
}
