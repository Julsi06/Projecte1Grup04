using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce = 325f;
    [SerializeField] private LayerMask groundLayer; // el layerMask estableix el que detectara el rayccast 
    [SerializeField] private int maxJumps = 2; // Número máximo de saltos permitidos

    private Rigidbody2D player;
    private int jumpCount; // Contador de saltos


    private void Awake()
    {
        // Agafa els components del GameObject (jugador)
        player = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Detecta input para saltar
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            Jump();
        }
    }

    // Nuevo método para reiniciar los saltos al tocar cualquier superficie
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reinicia el contador de saltos al tocar cualquier cosa
        jumpCount = 0;
    }

    private void Jump()
    {
        // Elimina la velocidad vertical anterior para saltos más consistentes
        player.velocity = new Vector2(player.velocity.x, 0);

        // Aplica la fuerza de salto (puedes cambiar a Impulse para un salto más inmediato)
        player.AddForce(new Vector2(0f, jumpForce));

        // Aumenta el contador de saltos
        jumpCount++;
    }
}
