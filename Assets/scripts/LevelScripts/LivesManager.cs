using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    public PlayerRespawn respawn;

    public GameObject[] blueHearts; // Corazones azules (vidas activas)
    // Suponemos que los corazones rojos están debajo y siempre visibles.

    public int currentLives;

    private bool isImmune = false;

    void Start()
    {
        currentLives = blueHearts.Length;
        ResetHearts();
    }

    public void LoseLife()
    {
        if (isImmune)
        {
            Debug.Log("Jugador inmune: no se pierde vida.");
            return;
        }

        if (currentLives > 0)
        {
            currentLives--;
            blueHearts[currentLives].SetActive(false);
        }
    }

    public void GainLife()
    {
        if (currentLives < blueHearts.Length)
        {
            blueHearts[currentLives].SetActive(true);
            currentLives++;
        }
    }

    public void ResetHearts()
    {
        for (int i = 0; i < blueHearts.Length; i++)
        {
            blueHearts[i].SetActive(true);
        }
        currentLives = blueHearts.Length;
    }

    private void SetImmunity(bool value)
    {
        isImmune = value;
        Debug.Log("Inmunidad: " + (isImmune ? "ACTIVADA" : "DESACTIVADA"));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isImmune = !isImmune; // Alternar inmunidad con la tecla P
            Debug.Log("Modo inmune: " + (isImmune ? "ON" : "OFF"));
        }

        if (currentLives == 0 && !isImmune)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}


