using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnSettingsClick()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void OnCreditsClick()
    {
        SceneManager.LoadScene("CreditsScene");
    }
    public void OnExitClick()
    {
        Application.Quit();
    }
}
