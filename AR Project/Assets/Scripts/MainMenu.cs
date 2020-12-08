using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenu : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI CurrentCity;

    public void Play()
    {
        Debug.Log("playing");
        SceneManager.LoadScene("GridCreation");
    }

    public void LeaderBoard()
    {
        SceneManager.LoadScene("LeaderBoard");
    }

    private void Update()
    {
        CurrentCity.text = GPS.Instance.City;
    }

}
