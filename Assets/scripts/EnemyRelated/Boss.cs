using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float radioDetection = 5f;
    [SerializeField] private float followVelocity = 3f;
    [SerializeField] private float distanciaMinima = 2f;
    [SerializeField] private float leftLimitOffset = -12f;
    [SerializeField] private float rightLimitOffset = 5f;
    private float leftLimit;
    private float rightLimit;
    private float startXPosition;
    private float startYPosition;

    [SerializeField] private float patrolSpeed = 2f;
    private float moveDirection = 1f;

    [Header("Shooting Settings")]
    [SerializeField] private float coolDownProjectile = 2f;
    [SerializeField] private GameObject prefabProyectil;

    [Header("Behavior Flags")]
    public bool itsStop = true;
    public bool isShooter = false;
    public bool canShoot = true;

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private Animator animator;
    private Transform player;

    private bool isFacingRight = true;
    private float lastXPosition;

    private bool isDying = false;
    private bool isHit = false;
    private float deathTimer = 0f;
    private float deathDuration = 1.5f;

    private Vector3 originalScale;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null) Debug.LogError("Falta el Animator en: " + gameObject.name);

        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        lastXPosition = transform.position.x;
        startXPosition = transform.position.x;
        startYPosition = transform.position.y;

        leftLimit = startXPosition + leftLimitOffset;
        rightLimit = startXPosition + rightLimitOffset;

        originalScale = transform.localScale;

        if (player == null)
        {
            Debug.LogError("No se encontró al jugador. Asegúrate de que tiene el tag 'Player'");
        }
    }

    void Update()
    {
        if (player == null) return;

        if (isDying)
        {
            deathTimer += Time.deltaTime;
            if (deathTimer >= deathDuration)
            {
                Destroy(gameObject);
            }
            return;
        }

        FollowPlayerOrPatrol();

        if (IsWithinDetectionArea() && isShooter && canShoot)
        {
            animator.SetTrigger("Attack");
            StartCoroutine(ShootProjectile());
        }
        else
        {
            animator.ResetTrigger("Attack");
        }
    }

    void FollowPlayerOrPatrol()
    {
        if (isHit || isDying)
            return;

        float playerDistance = Vector2.Distance(transform.position, player.position);

        if (playerDistance <= radioDetection && playerDistance > distanciaMinima)
        {
            // Following the player
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += new Vector3(direction.x, 0f, 0f) * followVelocity * Time.deltaTime;

            // Flip strictly based on player position with threshold to avoid jitter
            if (direction.x < -0.1f && isFacingRight)
            {
                Flip();
            }
            else if (direction.x > 0.1f && !isFacingRight)
            {
                Flip();
            }
        }
        else
        {
            // Patrolling mode

            // Clamp position so enemy does not go out of bounds
            float clampedX = Mathf.Clamp(transform.position.x, leftLimit, rightLimit);
            if (transform.position.x != clampedX)
            {
                transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
            }

            // Change direction if at edges
            if (transform.position.x <= leftLimit)
                moveDirection = 1f;
            else if (transform.position.x >= rightLimit)
                moveDirection = -1f;

            // Flip based only on patrol moveDirection
            if (moveDirection > 0 && !isFacingRight)
                Flip();
            else if (moveDirection < 0 && isFacingRight)
                Flip();

            transform.position += new Vector3(moveDirection, 0f, 0f) * patrolSpeed * Time.deltaTime;
        }
    }


    bool IsWithinDetectionArea()
    {
        if (player == null) return false;
        float playerDistance = Vector2.Distance(transform.position, player.position);
        return playerDistance <= radioDetection && playerDistance > distanciaMinima;
    }

    IEnumerator ShootProjectile()
    {
        canShoot = false;

        if (prefabProyectil != null)
        {
            Vector2 shootDirection = (player.position - transform.position).normalized;
            GameObject proyectil = Instantiate(prefabProyectil, transform.position, Quaternion.identity);

            ProjectileEnemy projectileScript = proyectil.GetComponent<ProjectileEnemy>();
            if (projectileScript != null)
            {
                projectileScript.SetDirectionShoot(shootDirection);
            }
            else
            {
                Debug.LogWarning("El prefab del proyectil no tiene el script ProjectileEnemy");
            }
        }

        yield return new WaitForSeconds(coolDownProjectile);
        canShoot = true;
    }

    public void TakeDamage(int damage)
    {
        if (isDying) return;
        isHit = true;
        currentHealth -= damage;
        Debug.Log("Boss is taking damage");

        animator.SetTrigger("isAttacked");

        if (currentHealth > 0)
        {
            animator.SetTrigger("Hit");
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        isDying = true;
        GetComponent<Collider2D>().enabled = false;

        animator.ResetTrigger("isAttacked");
        animator.ResetTrigger("Hit");
        animator.ResetTrigger("attack");

        animator.SetTrigger("Die");
        StartCoroutine(WaitAndDestroy());
    }

    private IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(deathDuration);
        Destroy(gameObject);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x = isFacingRight ? Mathf.Abs(originalScale.x) : -Mathf.Abs(originalScale.x);
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDetection);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(startXPosition + leftLimitOffset, transform.position.y),
                        new Vector2(startXPosition + rightLimitOffset, transform.position.y));
    }
}
