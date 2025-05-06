using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRecibeDaño : MonoBehaviour
{
    private PlayerMovement playerMovement;
    [SerializeField] private float tiempoInvulnerabilidad = 1f;
    [SerializeField] private Transform respawnPoint;
    public int currentLives;
    public bool canTakeDamage = true;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        currentLives = playerMovement.maxPlayerLives;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("enemy"))
        {
            Debug.Log("Collision");
            currentLives--;
            StartCoroutine(ActivarInvulnerabilidad());
            if (currentLives <= 0) Morir();

        }
        if(collision.gameObject.CompareTag("damagePlat"))
        {
            currentLives--;
        }
    }

    private IEnumerator ActivarInvulnerabilidad()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(tiempoInvulnerabilidad);
        canTakeDamage = true;
    }

    private void Morir()
    {
        Debug.Log("¡Jugador derrotado!");
        transform.position = respawnPoint.position;
        currentLives = playerMovement.maxPlayerLives;
    }
}
