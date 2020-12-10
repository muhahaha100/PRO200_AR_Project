using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupScript : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(scriptsLoaded());   
    }

    private IEnumerator scriptsLoaded()
    {
        yield return new WaitUntil(() => GPS.Instance != null);
        yield return new WaitUntil(() => LeaderDataBaseManager.Instance != null);

        Debug.Log("here");
        SceneManager.LoadScene("MainMenuScene");
    }

}
