using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private Vector2 playerStartPosition = new Vector2(202.114f, 0.763f);

    public void OnStartClick()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void OnSettingsClick()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void OnCreditsClick()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void OnRestartClick()
    {
        // Suscribirse al evento de escena cargada
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

            // Desuscribirse para evitar múltiples llamadas
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    public void OnExitClick()
    {
        Debug.Log("salir...");
        Application.Quit();
    }
}
