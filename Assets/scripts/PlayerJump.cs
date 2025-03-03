using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int maxJumps = 2; // Número máximo de saltos permitidos

    private Rigidbody2D player;
    private int jumpCount; // Contador de saltos
    private bool isGrounded; // Estado actual del suelo
    private bool wasGrounded; // Estado del frame anterior

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckGrounded(); // Verifica si el personaje toca el suelo

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            Jump();
        }
    }

    private void CheckGrounded()
    {
        // Lanza un rayo hacia abajo para detectar el suelo
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, groundLayer);

        // Dibuja el rayo en la escena para depuración
        Debug.DrawRay(transform.position, Vector2.down * 0.2f, Color.red);

        // Si antes no estaba en el suelo pero ahora sí => Se reinicia el contador de saltos
        if (isGrounded && !wasGrounded)
        {
            jumpCount = 0;
        }

        // Guardamos el estado del suelo para el próximo frame
        wasGrounded = isGrounded;
    }

    private void Jump()
    {
        player.velocity = new Vector2(player.velocity.x, speed);
        jumpCount++; // Aumenta el contador de saltos
    }
}
