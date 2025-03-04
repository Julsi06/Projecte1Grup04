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
    private bool isGrounded; // Estado actual del suelo
 

    private void Awake()
    {
        // Agafa els components del GameObject (jugador)
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
        RaycastHit2D hit;


        // Lanza un rayo hacia abajo para detectar el suelo
        hit = Physics2D.Linecast(transform.position + new Vector3(0, transform.position.y - 0.5f, 0), transform.position + Vector3.down);

        // Dibuja el rayo en la escena para depuración
        Debug.DrawRay(transform.position, Vector2.down, Color.red);

        if (hit.collider != null) {
            // Si antes no estaba en el suelo pero ahora sí => Se reinicia el contador de saltos
            if (hit.collider.gameObject.name == "Ground")
            {

                jumpCount = 0;
            }
        }


       
     
    }

    private void Jump()
    {
        Debug.Log("Flag");
        player.AddForce(new Vector2(0f, jumpForce));
        jumpCount++; // Aumenta el contador de saltos
    }
}
