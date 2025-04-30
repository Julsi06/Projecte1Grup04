using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRecibeDaño : MonoBehaviour
{

    [SerializeField] private int vidaMaxima = 50;
    private float fuerzaKnockback = 5f;
    [SerializeField] private float tiempoInvulnerabilidad = 1f;
    [SerializeField] private Transform spawnPoint;

    private int vidaActual;
    private bool puedeRecibirDaño = true;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Respawn();
    }

    // Método público para recibir daño desde el enemigo
    public void RecibirDaño(Vector2 direccionAtaque, int daño)
    {
        if (!puedeRecibirDaño) return;

        vidaActual -= daño;
        Debug.Log($"¡Daño recibido! Vida restante: {vidaActual}");

        // Knockback (empuje en dirección opuesta al ataque)
        Vector2 direccionKnockback = new Vector2(
            Mathf.Sign(transform.position.x - direccionAtaque.x),
            0.3f  // Pequeño componente vertical
        ).normalized;

        rb.velocity = Vector2.zero;
        rb.AddForce(direccionKnockback * fuerzaKnockback, ForceMode2D.Impulse);

        // Temporizador de invulnerabilidad
        StartCoroutine(ActivarInvulnerabilidad());

        if (vidaActual <= 0) Morir();
    }

    private IEnumerator ActivarInvulnerabilidad()
    {
        puedeRecibirDaño = false;
        yield return new WaitForSeconds(tiempoInvulnerabilidad);
        puedeRecibirDaño = true;
    }

    private void Morir()
    {
        Debug.Log("¡Jugador derrotado!");
        Respawn();
    }

    private void Respawn()
    {
        transform.position = spawnPoint.position;
        vidaActual = vidaMaxima; 
    }
}
