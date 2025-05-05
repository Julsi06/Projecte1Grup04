using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyAttack : MonoBehaviour
{
    [SerializeField] private int dañoPorContacto = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si chocó con el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerRecibeDaño jugador = collision.gameObject.GetComponent<PlayerRecibeDaño>();

            if (jugador != null)
            {
                // Dirección del ataque (desde el enemigo hacia el jugador)
                Vector2 direccionAtaque = transform.position;
                jugador.RecibirDaño(direccionAtaque, dañoPorContacto);
            }
        }
    }
}

