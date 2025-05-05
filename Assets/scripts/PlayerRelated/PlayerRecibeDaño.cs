using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRecibeDaño : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private float fuerzaKnockback = 5f;
    [SerializeField] private float tiempoInvulnerabilidad = 1f;

    [SerializeField] private int currentLives;
    private bool canTakeDamage = true;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        currentLives = playerMovement.maxPlayerLives;
    }

    // Método público para recibir daño desde el enemigo
    public void RecibirDaño(Vector2 attackDirection, int daño)
    {
        if (!canTakeDamage) return;

        currentLives -= daño;
        Debug.Log($"¡Daño recibido! Vida restante: {currentLives}");

        // Knockback (empuje en dirección opuesta al ataque)
        Vector2 direccionKnockback = new Vector2(
            Mathf.Sign(transform.position.x - attackDirection.x),
            0.3f  // Pequeño componente vertical
        ).normalized;

        rb.velocity = Vector2.zero;
        rb.AddForce(direccionKnockback * fuerzaKnockback, ForceMode2D.Impulse);

        // Temporizador de invulnerabilidad
        StartCoroutine(ActivarInvulnerabilidad());

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
    }

    
}
