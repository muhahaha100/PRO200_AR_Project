using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderBoardScript : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI CurrentCity;

    private void Update()
    {
        CurrentCity.text = GPS.Instance.City;
    }

    public void BackButton()
    {
        SceneManager.LoadScene("StartupScene");
    }
}
