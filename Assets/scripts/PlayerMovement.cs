using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 8f;  // Velocidad máxima
    [SerializeField] private float acceleration = 0.2f; // Aceleración
    [SerializeField] private float deceleration = 0.3f; // Desaceleración

    [SerializeField] private float jumpForce = 650f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int maxJumps = 2;
    public float RaycastDistance = 0.5f;

    private Rigidbody2D player;
    private bool isFacingRight = true;
    private float currentSpeed = 0f;
    private int jumpCount;
    private bool isGrounded = false;
    private Animator animator;

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
    }

    private void FixedUpdate()
    {
        HandleMovement();
        CheckGrounded();
        animator.SetFloat("yVelocity", player.velocity.y);
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("xVelocity", Math.Abs(player.velocity.x));

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
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
        }
    }

    private void CheckGrounded()
    {
        RaycastHit2D hit;
        Vector2 raycastOrigin = transform.position - new Vector3(0f, 1.01f, 0f);

        hit = Physics2D.Raycast(raycastOrigin, Vector2.down * 0.5f, RaycastDistance, groundLayer);

        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            jumpCount = 1;
            isGrounded = true;
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
        else if (player.velocity.y < 0) // Falling condition
        {
            isGrounded = false;
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }
    }

    private void Jump()
    {
        Debug.Log(player.velocity.y);
        player.velocity = new Vector2(player.velocity.x, 0f);
        player.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
        jumpCount++;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
