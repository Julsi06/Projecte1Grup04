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
    private bool isGrounded; // Nueva variable para trackear si está en tierra

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

    [Header("Audio Settings")]
    public AudioClip attackSound; // Clip de audio para el ataque
    [Range(0f, 1f)] public float attackSoundVolume = 1f; // Volumen del sonido de ataque

    public AudioClip walkSound; // Audio de pasos

    private AudioSource audioSourceAttack;
    private AudioSource audioSourceWalk;

    // Controlar el tiempo de ataque
    private bool canPlayAttackSound = true; // Controla si se puede reproducir el sonido de ataque

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        player.drag = 0;
        jumpCount = 1;
        animations = GetComponentInChildren<PlayerAnimations>();
        lastClickTime = -clearQueueDelay; // Para iniciar limpio

        // Crear AudioSources
        audioSourceAttack = gameObject.AddComponent<AudioSource>();
        audioSourceWalk = gameObject.AddComponent<AudioSource>();

        // Configuración Audio caminata
        if (walkSound != null)
        {
            audioSourceWalk.clip = walkSound;
            audioSourceWalk.loop = true;
            audioSourceWalk.playOnAwake = false;
            audioSourceWalk.volume = 0.5f; // Puedes ajustar volumen aquí o con un parámetro si quieres
        }
    }

    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        HandleJump();
        HandleAttackInput(); // Manejar ataque
        HandleMovement();
        CheckClearAttackQueue();

        HandleWalkSound();
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        HandleMovement();
    }

    private void HandleWalkSound()
    {
        if (isGrounded && Mathf.Abs(moveInput) > 0.1f)
        {
            if (!audioSourceWalk.isPlaying)
                audioSourceWalk.Play();
        }
        else
        {
            if (audioSourceWalk.isPlaying)
                audioSourceWalk.Stop();
        }
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
            attackQueue.Enqueue(currentAttackIndex);
            currentAttackIndex = (currentAttackIndex + 1) % 2;
            lastClickTime = Time.time;
            if (!isAttacking)
            {
                StartNextAttack();
            }

            if (canPlayAttackSound && audioSourceAttack != null && attackSound != null)
            {
                audioSourceAttack.PlayOneShot(attackSound, attackSoundVolume);
                canPlayAttackSound = false;
                StartCoroutine(ResetAttackSoundCooldown());
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

    private IEnumerator ResetAttackSoundCooldown()
    {
        yield return new WaitForSeconds(0.33f);
        canPlayAttackSound = true;
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
            isAttacking = false;
            attackCollider.enabled = false;
        }
    }

    private void CheckGrounded()
    {
        RaycastHit2D hit;
        Vector2 raycastOrigin = transform.position - new Vector3(0f, RAYCAST_GROUND, 0f);

        hit = Physics2D.Raycast(raycastOrigin, Vector2.down * RAYCAST_GROUND, RaycastDistance, groundLayer);
        Debug.DrawRay(raycastOrigin, Vector2.down * RAYCAST_GROUND, Color.red);

        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            jumpCount = 1;
            IsDoubleJumping = false;
            isGrounded = true;

            if (Mathf.Abs(player.velocity.y) < 0.1f)
            {
                player.velocity = new Vector2(player.velocity.x, 0f);
            }
        }
        else
        {
            isGrounded = false;
        }
    }

    private void Jump()
    {
        player.velocity = new Vector2(player.velocity.x, 0f);
        player.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
        jumpCount++;
        IsDoubleJumping = (jumpCount == 2);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
