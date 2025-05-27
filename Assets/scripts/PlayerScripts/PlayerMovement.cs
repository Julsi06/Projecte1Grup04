using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float RAYCAST_GROUND = 0.25f;

    // Movement variables
    [SerializeField] private float maxSpeed = 8f;  // Velocidad máxima
    [SerializeField] private float acceleration = 0.2f; // Aceleración
    [SerializeField] private float deceleration = 0.3f; // Desaceleración

    // Jump and ground detecting variables
    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float RaycastDistance = 1.5f;

    public Collider2D attackCollider;

    // Player initialized variables
    public bool IsDoubleJumping { get; private set; }

    private Rigidbody2D player;
    private bool isFacingRight = true;
    private float currentSpeed = 0f;
    private int jumpCount;
    public int maxPlayerLives = 4;
    public bool canMove = true;
    float moveInput;

    // Attack variables
    public Queue<int> attackQueue = new Queue<int>(); // Cola de ataques
    private bool isAttacking = false;
    private int currentAttackIndex = 0; // Para ciclar entre 0-3 animaciones

    private PlayerAnimations animations;

    private float lastClickTime = 0f; // Tiempo del último clic
    private float clearQueueDelay = 0.2f; // Tiempo para limpiar la cola sin clics

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        player.drag = 0;
        jumpCount = 1;
        animations = GetComponentInChildren<PlayerAnimations>();
        lastClickTime = -clearQueueDelay; // Para iniciar limpio
    }

    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        HandleJump();
        HandleAttackInput(); // Manejar ataque
        HandleMovement();
        CheckClearAttackQueue();
    }


    private void FixedUpdate()
    {
        CheckGrounded();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (canMove)
        {
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

            // Aplica un threshold para evitar valores residuales en Y
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
    }

    private void HandleJump()
    {
        if (canMove)
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
    }
    private void HandleAttackInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (attackCollider != null)
            {
                attackCollider.enabled = true;
            }
            // Cada click añade un ataque en la cola
            attackQueue.Enqueue(currentAttackIndex);
            currentAttackIndex = (currentAttackIndex + 1) % 2;
            lastClickTime = Time.time; // Actualiza tiempo último clic
            if (!isAttacking)
            {
                StartNextAttack();
            }
        }
        else
        {
            if (attackCollider != null)
            {
                attackCollider.enabled = false;
            }
        }
    }

    private void CheckClearAttackQueue()
    {
        if (attackQueue.Count > 0)
        {
            if (Time.time - lastClickTime >= clearQueueDelay)
            {
                attackQueue.Clear();
            }
        }
    }

    // Método llamado para iniciar un nuevo ataque de la cola
    public void StartNextAttack()
    {
        if (attackQueue.Count > 0)
        {
            isAttacking = true;
            int attackIdx = attackQueue.Dequeue();
            if (animations != null)
            {
                animations.SetAttackAnimation(attackIdx);
                attackCollider.enabled = true;
            }
            else
            {
                Debug.LogWarning("La referencia a animations es null.");
            }
        }
        else
        {
            isAttacking = false; // No hay más ataques: parar estado de ataque
            attackCollider.enabled = false;
        }
    }

    private void CheckGrounded()
    {
        RaycastHit2D hit;
        Vector2 raycastOrigin = transform.position - new Vector3(0f, RAYCAST_GROUND, 0f);

        // Lanza un rayo hacia abajo para detectar el suelo
        hit = Physics2D.Raycast(raycastOrigin, Vector2.down * RAYCAST_GROUND, RaycastDistance, groundLayer);
        Debug.DrawRay(raycastOrigin, Vector2.down * RAYCAST_GROUND, Color.red);

        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {

            Debug.Log("Pta vida tt");
            // Si está tocando el suelo, reinicia el contador de saltos
            jumpCount = 1;
            IsDoubleJumping = false;

            // Asegura que el Rigidbody no tenga residuos verticales
            if (Mathf.Abs(player.velocity.y) < 0.1f)
            {
                player.velocity = new Vector2(player.velocity.x, 0f);
            }
        }
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