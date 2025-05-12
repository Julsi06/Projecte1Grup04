using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRecibeDaño : MonoBehaviour
{
    private PlayerMovement playerMovement;
    [SerializeField] private float tiempoInvulnerabilidad = 1f;
    [SerializeField] private Transform respawnPoint;
    public float currentLives;
    public bool canTakeDamage = true;
    private buttonDoor button;
    public GameObject door;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        currentLives = playerMovement.maxPlayerLives;
        button = GetComponent<buttonDoor>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("enemy"))
        {
            Debug.Log("Collision");
            currentLives--;
            StartCoroutine(ActivarInvulnerabilidad());
        }
        if(collision.gameObject.CompareTag("damagePlat"))
            currentLives -= 0.75f;

        if (currentLives <= 0) Morir();
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
