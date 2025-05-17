using System.Collections;
using System.Collections.Generic;
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

    private Transform player;
    private bool isFacingRight = true;
    private float lastXPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastXPosition = transform.position.x;

        if (player == null)
        {
            Debug.LogError("No se encontró al jugador. Asegúrate de que tiene el tag 'Player'");
        }
    }

    void Update()
    {
        if (player == null) return;

        FollowPlayer();

        if (IsWithinDetectionArea() && isShooter && canShoot)
        {
            StartCoroutine(ShootProjectile());
        }
    }

    void FollowPlayer()
    {
        float playerDistance = Vector2.Distance(transform.position, player.position);
        Vector2 direction = (player.position - transform.position).normalized;

        // Movimiento solo si está en rango
        if (playerDistance <= radioDetection && playerDistance > distanciaMinima)
        {
            if (!itsStop || (itsStop && playerDistance <= radioDetection))
            {
                transform.Translate(direction * followVelocity * Time.deltaTime);
            }

            // Lógica mejorada de Flip (usa la dirección, no la posición anterior)
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