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


        player.velocity = new Vector2(currentSpeed, verticalVelocity);



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
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            player.velocity = new Vector2(player.velocity.x, player.velocity.y / 1.2f);
        }
    }

    private void CheckGrounded()
    {
        RaycastHit2D hit;
        Vector2 raycastOrigin = transform.position - new Vector3(0f, 0.75f, 0f);

        // Lanza un rayo hacia abajo para detectar el suelo
        hit = Physics2D.Raycast(raycastOrigin, Vector2.down * 0.6f, RaycastDistance, groundLayer);
        Debug.DrawRay(raycastOrigin, Vector2.down * 0.5f, Color.red);
        if (hit.collider != null)
        {
            Debug.Log("Raycast hit: " + hit.collider.name + " | Tag: " + hit.collider.tag);
        }
        else
        {
            Debug.Log("Raycast didn't hit anything.");
        }

        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            Debug.Log("touching ground...");
            // Si está tocando el suelo, reinicia el contador de saltos
            jumpCount = 1;
            IsDoubleJumping = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position - new Vector3(0f, .75f, 0f), transform.position - new Vector3(0f, .75f, 0f) + Vector3.down * 0.6f);
    }

    private void Jump()
    {
        player.velocity = new Vector2(player.velocity.x, 0f); // Elimina cualquier fuerza acumulada
        player.AddForce(Vector2.up * jumpForce, ForceMode2D.Force); // Salto instantáneo
        jumpCount++;

        // Activa doble salto si es el segundo salto
        IsDoubleJumping = (jumpCount == 2);
    }
     
    private void Flip()
    {
        isFacingRight = !isFacingRight; // Cambia la direccion
        transform.Rotate(0f, 180f, 0f);
    }
}