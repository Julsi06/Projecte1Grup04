using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ENDGAME : MonoBehaviour
{
    public bool exitOnKeyPress = true;
    public KeyCode exitKey = KeyCode.Escape;

    public bool exitOnTouch = true;
    public string playerTag = "Player";

    private void Update()
    {
        if (exitOnKeyPress && Input.GetKeyDown(exitKey))
        {
            QuitGame();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (exitOnTouch && collision.gameObject.CompareTag(playerTag))
        {
            QuitGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (exitOnTouch && other.CompareTag(playerTag))
        {
            QuitGame();
        }
    }

    private void QuitGame()
    {
        Debug.Log("Cerrando el juego...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
