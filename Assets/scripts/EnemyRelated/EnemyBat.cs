using System.Collections;
using UnityEngine;

public class EnemyBat : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float radioDetection = 5f;
    [SerializeField] private float followVelocity = 3f;
    [SerializeField] private float distanciaMinima = 2f;

    [Header("Shooting Settings")]
    [SerializeField] private float coolDownProjectile = 2f;
    [SerializeField] private GameObject prefabProyectil;

    [Header("Behavior Flags")]
    public bool itsStop = true;
    public bool isShooter = false;
    public bool canShoot = true;

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100; // integrado health de EnemyDamage
    private int currentHealth;

    private Animator animator;
    private Transform player;

    private bool isFacingRight = true;
    private float lastXPosition;

    private bool isDying = false; // controla estado de muerte
    private float deathTimer = 0f;
    private float deathDuration = 1.5f; // duración de animación muerte

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null) Debug.LogError("Falta el Animator en: " + gameObject.name);
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        lastXPosition = transform.position.x;

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
            return; // no hacer nada mientras muere
        }

        FollowPlayer();

        float xBatVelocity = Mathf.Abs(transform.position.x - lastXPosition) / Time.deltaTime;
        animator.SetFloat("xBatVelocity", xBatVelocity);
        lastXPosition = transform.position.x;

        if (IsWithinDetectionArea() && isShooter && canShoot)
        {
            StartCoroutine(ShootProjectile());
        }
    }

    void FollowPlayer()
    {
        float playerDistance = Vector2.Distance(transform.position, player.position);
        Vector2 direction = (player.position - transform.position).normalized;

        if (playerDistance <= radioDetection && playerDistance > distanciaMinima)
        {
            if (!itsStop || (itsStop && playerDistance <= radioDetection))
            {
                transform.Translate(direction * followVelocity * Time.deltaTime);
            }

            if (direction.x > 0 && !isFacingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && isFacingRight)
            {
                Flip();
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

    // Funcionalidad integrada de EnemyDamage para daño y muerte
    public void TakeDamage(int damage)
    {
        if (isDying) return; // ignorar daño si muriendo

        currentHealth -= damage;

        animator.SetTrigger("isAttacked"); // trigger integrado de EnemyDamage
        if (currentHealth > 0)
        {
            animator.SetTrigger("Hit"); // trigger previo de EnemyBat
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("Die");
        GetComponent<Collider2D>().enabled = false; // deshabilita collider

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
    }
}

