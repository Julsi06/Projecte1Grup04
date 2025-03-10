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
        jumpCount = 0;
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
        hit = Physics2D.Linecast(transform.position + Vector3.down, transform.position + Vector3.down * 1.25f);


        if (hit.collider != null)
        {
            // Si antes no estaba en el suelo pero ahora sí => Se reinicia el contador de saltos
            if (hit.collider.gameObject.tag == "Ground")
            {
                jumpCount = 0;
            }
        }
    }

    private void Jump()
    {
        player.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Force);
        jumpCount++; // Aumenta el contador de saltos
        Debug.Log("Jump");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position + Vector3.down, 0.1f);
        Gizmos.DrawSphere(transform.position + Vector3.down * 1.25f, 0.1f);
        // Dibuja el rayo en la escena para depuración
        //Debug.DrawRay(transform.position + new Vector3(0, transform.position.y - 0.5f, 0), Vector3.down, Color.red);
    }
}
