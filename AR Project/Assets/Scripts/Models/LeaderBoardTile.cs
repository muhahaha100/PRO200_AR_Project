using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoardTile : MonoBehaviour
{
    public string playerName;

    public string Name
    {
        get { return playerName; }
        set 
        {
            playerName = value;
            nameText.text = Name;
        }
    }
    public int score;

    public int Score
    {
        get { return score; }
        set 
        {
            score = value;
            scoreText.text = Score.ToString();

        }
    }
    public string city;

    public string City
    {
        get { return city; }
        set 
        {
            city = value;
            cityText.text = City;
        }
    }

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI cityText;

}
