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
    [SerializeField] private float RaycastDistance = 0.2f; // Reducido para mejor detección

    // Player initialized variables
    public bool IsDoubleJumping { get; private set; }

    private Rigidbody2D player;
    private bool isFacingRight = true;
    private float currentSpeed = 0f;
    private int jumpCount;
    private bool isHanging = false;
    public int maxPlayerLives = 4;

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        player.drag = 0;
        jumpCount = 0; // Cambiado a 0 para permitir primer salto
    }

    private void Update()
    {
        HandleJump();

        if (!isHanging)
        {
            HandleMovement();
        }
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        HandleMovement();
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0)
        {
            currentSpeed += acceleration;
            if (currentSpeed > maxSpeed) currentSpeed = maxSpeed;
        }
        else if (moveInput < 0)
        {
            currentSpeed -= acceleration;
            if (currentSpeed < -maxSpeed) currentSpeed = -maxSpeed;
        }
        else
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= deceleration;
                if (currentSpeed < 0) currentSpeed = 0;
            }
            else if (currentSpeed < 0)
            {
                currentSpeed += deceleration;
                if (currentSpeed > 0) currentSpeed = 0;
            }
        }

        float verticalVelocity = player.velocity.y;

        if (Mathf.Abs(verticalVelocity) < 0.1f)
        {
            verticalVelocity = 0f;
        }

        player.velocity = new Vector2(currentSpeed, verticalVelocity);

        if ((currentSpeed > 0 && !isFacingRight) || (currentSpeed < 0 && isFacingRight))
        {
            Flip();
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Puede saltar si tiene saltos disponibles
            if (jumpCount < maxJumps)
            {
                Jump();
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            player.velocity = new Vector2(player.velocity.x, player.velocity.y / 1.2f);
        }
    }

    private void CheckGrounded()
    {
        // Usamos BoxCast para mejor detección
        Vector2 boxSize = new Vector2(0.5f, 0.1f); // Tamaño del área de detección
        Vector2 boxOrigin = transform.position - new Vector3(0f, 0.8f, 0f); // Origen del boxcast

        RaycastHit2D hit = Physics2D.BoxCast(boxOrigin, boxSize, 0f, Vector2.down, 0.1f, groundLayer);

        // Dibujamos el área de detección en el editor
        Debug.DrawRay(boxOrigin + new Vector2(-boxSize.x / 2, -boxSize.y / 2), Vector2.right * boxSize.x, Color.red);
        Debug.DrawRay(boxOrigin + new Vector2(-boxSize.x / 2, boxSize.y / 2), Vector2.right * boxSize.x, Color.red);
        Debug.DrawRay(boxOrigin + new Vector2(-boxSize.x / 2, -boxSize.y / 2), Vector2.up * boxSize.y, Color.red);
        Debug.DrawRay(boxOrigin + new Vector2(boxSize.x / 2, -boxSize.y / 2), Vector2.up * boxSize.y, Color.red);

        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            // Solo reiniciamos los saltos si estamos cayendo (velocity.y <= 0)
            if (player.velocity.y <= 0.1f)
            {
                // Si está tocando el suelo, reinicia el contador de saltos
                jumpCount = 0;
                IsDoubleJumping = false;
                player.velocity = new Vector2(player.velocity.x, 0f);
            }
        }
        else
        {
            // Si no está tocando el suelo y no estamos saltando, establecer jumpCount al máximo
            // Esto previene saltos infinitos en el aire
            if (jumpCount == 0)
            {
                jumpCount = maxJumps - 1;
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Dibujamos el área de detección en el editor
        Vector2 boxSize = new Vector2(0.5f, 0.1f);
        Vector2 boxOrigin = transform.position - new Vector3(0f, 0.8f, 0f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(boxOrigin + new Vector2(0f, 0f), boxSize);
    }

    private void Jump()
    {
        // Asegúrate de resetear la velocidad Y antes del salto
        player.velocity = new Vector2(player.velocity.x, 0f);
        player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        // Limita la velocidad máxima en Y para evitar movimientos bruscos
        if (player.velocity.y > 20f) // Ajusta este valor según necesites
        {
            player.velocity = new Vector2(player.velocity.x, 20f);
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}