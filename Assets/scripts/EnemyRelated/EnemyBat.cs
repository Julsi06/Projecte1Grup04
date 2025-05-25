using System.Collections;
using UnityEngine;

public class EnemyBat : MonoBehaviour
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
    private float deathTimer = 0f;
    private float deathDuration = 1.5f;

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

        float xBatVelocity = Mathf.Abs(transform.position.x - lastXPosition) / Time.deltaTime;
        animator.SetFloat("xBatVelocity", xBatVelocity);
        lastXPosition = transform.position.x;

        if (IsWithinDetectionArea() && isShooter && canShoot)
        {
            StartCoroutine(ShootProjectile());
        }
    }

    void FollowPlayerOrPatrol()
    {
        float playerDistance = Vector2.Distance(transform.position, player.position);

        if (playerDistance <= radioDetection && playerDistance > distanciaMinima)
        {
            // Movimiento hacia el jugador
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(new Vector2(direction.x, 0f) * followVelocity * Time.deltaTime);

            if (direction.x > 0 && !isFacingRight)
                Flip();
            else if (direction.x < 0 && isFacingRight)
                Flip();
        }
        else
        {
            // Patrullaje entre límites
            transform.Translate(Vector2.right * moveDirection * patrolSpeed * Time.deltaTime);

            if (transform.position.x <= leftLimit)
            {
                if (!isFacingRight) Flip();
                transform.position = new Vector2(leftLimit, transform.position.y);
                moveDirection = -1f;
                
            }
            else if (transform.position.x >= rightLimit)
            {
                if (!isFacingRight) Flip();
                transform.position = new Vector2(rightLimit, transform.position.y);
                moveDirection = 1f;
                
            }
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

        currentHealth -= damage;

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
        animator.SetTrigger("Die");
        GetComponent<Collider2D>().enabled = false;

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
        transform.Rotate(0f, 180f, 0f);
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
