using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private int attackDamage = 1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si chocó con el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerRecibeDaño jugador = collision.gameObject.GetComponent<PlayerRecibeDaño>();

            if (jugador != null)
            {
                // Dirección del ataque (desde el enemigo hacia el jugador)
                Vector2 attackDirection = transform.position;
                jugador.RecibirDaño(attackDirection, attackDamage);
            }
        }
    }
}
