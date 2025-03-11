using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer; // el layerMask estableix el que detectara el rayccast 
    [SerializeField] private int maxJumps = 2; // Número máximo de saltos permitidos
    public float RaycastDistance = 0.5f;


    private Rigidbody2D player;
    private int jumpCount; // Contador de saltos
 

    private void Awake()
    {
        // Agafa els components del GameObject (jugador)
        player = GetComponent<Rigidbody2D>();
        jumpCount = 1;
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
        Vector2 raycastOrigin = transform.position - new Vector3(0f, 1.01f, 0f);

        // Lanza un rayo hacia abajo para detectar el suelo
        hit = Physics2D.Raycast(raycastOrigin, Vector2.down * 0.5f, RaycastDistance, groundLayer);

        // Si antes no estaba en el suelo pero ahora sí => Se reinicia el contador de saltos
        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            jumpCount = 1;
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
        Vector2 raycastOrigin = transform.position - new Vector3(0f, 1.01f, 0f);
        // Dibuja el rayo en la escena para depuración
        Debug.DrawRay(raycastOrigin, Vector2.down*0.5f, Color.red);
    }
}