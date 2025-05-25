using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Damage : MonoBehaviour
{
    private LivesManager livesManager;
    private PlayerRespawn playerRespawn;

    private void Start()
    {
        livesManager = FindObjectOfType<LivesManager>();
        playerRespawn = GetComponent<PlayerRespawn>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Damage"))
            Death();
    }

    private void Death()
    {
        gameObject.SetActive(false);
        playerRespawn.Respawn();
        if (livesManager != null)
        {
            livesManager.LoseLife();
        }
    }
}
