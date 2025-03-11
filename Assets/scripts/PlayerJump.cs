using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
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

    //private void Update()
    //{
    //    jumpCount = 2;
    //    CheckGrounded(); // Verifica si el personaje toca el suelo

    //    if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
    //    {
    //        Jump();
    //    }
    //}

    //private void CheckGrounded()
    //{
    //    RaycastHit2D hit;


    //    // Lanza un rayo hacia abajo para detectar el suelo
    //    hit = Physics2D.Linecast(transform.position + new Vector3(0, transform.position.y - 0.5f, 0), transform.position + Vector3.down);

    //    // Dibuja el rayo en la escena para depuración
    //    Debug.DrawRay(transform.position, Vector2.down, Color.red);

    //    if (hit.collider != null) {
    //        // Si antes no estaba en el suelo pero ahora sí => Se reinicia el contador de saltos
    //        if (hit.collider.gameObject.name == "Ground")
    //        {

    //            jumpCount = 0;
    //        }
    //    }
    //}

    //private void Jump()
    //{
    //    player.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Force);
    //    jumpCount++; // Aumenta el contador de saltos
    //}
}
