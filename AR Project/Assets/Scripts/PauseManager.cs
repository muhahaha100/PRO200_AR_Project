using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject PauseButton = null;
    [SerializeField] GameObject PauseMenu = null;

    public void OpenMenu()
    {
        PauseButton.SetActive(false);
        PauseMenu.SetActive(true);
    }

    public void CloseMenu()
    {
        PauseButton.SetActive(true);
        PauseMenu.SetActive(false);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

}
