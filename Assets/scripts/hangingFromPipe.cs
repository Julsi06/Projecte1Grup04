using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingFromPipe : MonoBehaviour
{
    [SerializeField] private LayerMask pipeLayer;
    [SerializeField] private float maxSpeed = 4f;  // Velocidad máxima al moverse colgado
    [SerializeField] private float hangingOffset = 0.5f;  // Desplazamiento del sprite hacia abajo para que se enganche desde los brazos
    private Rigidbody2D player;
    private bool isHanging = false;
    private Animator animator;

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();  // Obtener el componente Animator
    }

    private void Update()
    {
        CheckHangingFromPipe();  // Verifica si está colgado de la tubería

        if (isHanging)
        {
            // Detener el movimiento vertical y la gravedad
            player.velocity = Vector2.zero;
            player.gravityScale = 0f;

            // Movimiento horizontal mientras se cuelga
            HandleHangingMovement();

            // Mantener al jugador pegado a la tubería ajustando la posición
            transform.position = new Vector2(transform.position.x, transform.position.y - hangingOffset);

            // Si presiona 'S' para soltarse
            if (Input.GetKeyDown(KeyCode.S))
            {
                DropFromPipe();
            }
        }
    }

    private void HandleHangingMovement()
    {
        // Movimiento horizontal mientras se cuelga
        float moveInput = Input.GetAxisRaw("Horizontal");  // Obtiene el movimiento horizontal

        // Actualiza la animación de movimiento mientras cuelga
        animator.SetFloat("xVelocity", Mathf.Abs(player.velocity.x));

        // Si hay entrada horizontal, está en movimiento
        if (moveInput != 0)
        {
            animator.SetBool("isHangingMoving", true);  // Se está moviendo mientras cuelga
            animator.SetBool("isHanging", false); // No está en idle
        }
        else
        {
            // Si no hay entrada horizontal, está en idle
            animator.SetBool("isHangingMoving", false);
            animator.SetBool("isHanging", true); // Se queda colgando sin moverse
        }

        // El jugador se mueve en la dirección horizontal
        player.velocity = new Vector2(moveInput * maxSpeed, player.velocity.y);
    }

    private void CheckHangingFromPipe()
    {
        // Lanza un rayo hacia arriba desde el centro del jugador para detectar la tubería
        Vector2 raycastOrigin = new Vector2(transform.position.x, transform.position.y - 0.5f);  // Origen ajustado
        Vector2 raycastDirection = Vector2.up;  // Dirección del rayo hacia arriba (si la tubería está arriba del jugador)

        // Cambiar a izquierda/derecha si la tubería está en el eje horizontal
        if (player.transform.position.y < 0)  // Si el jugador está en la parte inferior
        {
            raycastDirection = Vector2.right;  // Dirección a la derecha
        }

        // Depuración: Muestra el rayo en la vista de la escena
        Debug.DrawRay(raycastOrigin, raycastDirection * 1.0f, Color.red);
        Debug.Log("Raycast Origin: " + raycastOrigin + " Raycast Direction: " + raycastDirection);

        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, raycastDirection, 1.0f, pipeLayer);  // Raycast hacia la tubería

        if (hit.collider != null)
        {
            Debug.Log("Raycast Hit: " + hit.collider.name);  // Muestra el nombre del objeto al que ha golpeado el rayo
            if (hit.collider.CompareTag("Pipe"))
            {
                // Si se colisiona con una tubería, el jugador comienza a colgarse
                isHanging = true;
                animator.SetBool("isHanging", true);  // Activa la animación de colgarse
            }
        }
        else
        {
            Debug.Log("No hit detected.");
        }

        // Si no está colgado, se restablece la gravedad
        if (!isHanging)
        {
            animator.SetBool("isHanging", false);  // Desactiva la animación de colgarse
            player.gravityScale = 1.0f;  // Restaura la gravedad
        }
    }

    private void DropFromPipe()
    {
        // Si el jugador se suelta de la tubería, se restablece la gravedad
        isHanging = false;
        animator.SetBool("isHanging", false);  // Desactiva la animación de colgarse
        player.gravityScale = 1.0f;  // Restaura la gravedad
    }
}