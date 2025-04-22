using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement variables
    [SerializeField] private float maxSpeed = 8f;  // Velocidad máxima
    [SerializeField] private float acceleration = 0.2f; // Aceleración
    [SerializeField] private float deceleration = 0.3f; // Desaceleración

    // Jump and ground detecting variables
    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float RaycastDistance = 1.5f;

    // Player initialized variables
    private Rigidbody2D player;
    private bool isFacingRight = true;
    private float currentSpeed = 0f;
    private int jumpCount;
    private bool isHanging = false;
    private Animator animator;
    public int maxPlayerLives = 4;

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player.drag = 0;
        jumpCount = 1;
    }

    private void Update()
    {
        HandleJump();

        if (!isHanging) // Si no está colgado, maneja el movimiento normal
        {
            HandleMovement();
        }
    }


    private void FixedUpdate()
    {
        HandleMovement();
        CheckGrounded();
        animator.SetFloat("yVelocity", player.velocity.y);
    }

    private void HandleMovement()
    {
        if (isHanging) // Si está colgado, no se permite movimiento vertical, pero sí horizontal
        {
            // Esto ya está manejado en el script HangingFromPipe
            return;
        }
        
        float moveInput = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("xVelocity", Math.Abs(player.velocity.x));

        if (moveInput > 0)
        {
            currentSpeed += acceleration;
            if (currentSpeed > maxSpeed) currentSpeed = maxSpeed;
            animator.SetBool("isRunning", true); // Establece que está corriendo
        }
        else if (moveInput < 0)
        {
            currentSpeed -= acceleration;
            if (currentSpeed < -maxSpeed) currentSpeed = -maxSpeed;
            animator.SetBool("isRunning", true); // Establece que está corriendo
        }
        else
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= deceleration;
                if (currentSpeed < 0) currentSpeed = 0;
                animator.SetBool("isRunning", false); // Detiene la animación de correr
            }
            else if (currentSpeed < 0)
            {
                currentSpeed += deceleration;
                if (currentSpeed > 0) currentSpeed = 0;
                animator.SetBool("isRunning", false); // Detiene la animación de correr
            }
        }

        float verticalVelocity = player.velocity.y;
        player.velocity = new Vector2(currentSpeed, verticalVelocity);

        if (verticalVelocity < 0)
        {
            animator.SetBool("isFalling", true);
        }

        if ((currentSpeed > 0 && !isFacingRight) || (currentSpeed < 0 && isFacingRight))
        {
            Flip();
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            Jump();
            animator.SetBool("isJumping", true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            player.velocity = new Vector2(player.velocity.x, player.velocity.y / 1.2f);
        }
    }

    private void CheckGrounded()
    {
        RaycastHit2D hit;
        Vector2 raycastOrigin = transform.position - new Vector3(0f, 1.01f, 0f);

        // Lanza un rayo hacia abajo para detectar el suelo
        hit = Physics2D.Raycast(raycastOrigin, Vector2.down * 0.5f, RaycastDistance, groundLayer);
        Debug.DrawRay(raycastOrigin, Vector2.down * 1.2f, Color.red);

        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            // Si está tocando el suelo, reinicia el contador de saltos
            jumpCount = 1;
            animator.SetBool("isGrounded", true); // Establece que está en el suelo
            if (player.velocity.y <= 0)
            {
                if(jumpCount == maxJumps)
                    animator.SetBool("isDoubleJumping", false); // Detiene la animación de doble salto
                else
                    animator.SetBool("isJumping", false); // Detiene la animación de salto

                animator.SetBool("isFalling", false); // Detiene la animación de caída
            }
        }
        else
        {
            animator.SetBool("isGrounded", false); // Establece que no está en el suelo

            if (player.velocity.y > 0.1f)
            {
                if (jumpCount == maxJumps)
                    animator.SetBool("isDoubleJumping", true); // Detiene la animación de doble salto
                else
                    animator.SetBool("isJumping", true); // Detiene la animación de salto
                animator.SetBool("isFalling", false); // Asegúrate de que no está cayendo
            }
            else if (player.velocity.y < -0.1f)
            {
                if (jumpCount == maxJumps)
                    animator.SetBool("isDoubleJumping", false); // Detiene la animación de doble salto
                else
                    animator.SetBool("isJumping", false); // Detiene la animación de salto
                animator.SetBool("isFalling", true); // Establece que está cayendo
            }
        }
    }

    private void Jump()
    {
        player.velocity = new Vector2(player.velocity.x, 0f); // Elimina cualquier fuerza acumulada
        player.AddForce(Vector2.up * jumpForce, ForceMode2D.Force); // Salto instantáneo
        jumpCount++;
    }
     
    private void Flip()
    {
        isFacingRight = !isFacingRight; // Cambia la direccion
        transform.Rotate(0f, 180f, 0f);
    }
}