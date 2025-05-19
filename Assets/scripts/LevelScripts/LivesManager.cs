using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    public PlayerRespawn respawn;

    public GameObject[] blueHearts; // Corazones azules (vidas activas)
    // Suponemos que los corazones rojos están debajo y siempre visibles.

    private int currentLives;

    void Start()
    {
        currentLives = blueHearts.Length;
        ResetHearts();
    }

    public void LoseLife()
    {
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetHearts();
        }

        if (currentLives == 0)
        {
            respawn.Respawn();
        }
    }
}


