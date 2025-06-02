using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private Vector2 playerStartPosition = new Vector2(202.114f, 0.763f);

    public void OnStartClick()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void OnSettingsClick()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void OnReturnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnCreditsClick()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void OnRestartClick()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("Level_1");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level_1")
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                player.transform.position = playerStartPosition;
            }

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    public void OnExitClick()
    {
        Debug.Log("salir...");
        Application.Quit();
    }
}
