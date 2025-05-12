using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : MonoBehaviour
{
    [SerializeField] private float radioDetection = 5f;
    [SerializeField] private float coolDownProjectile = 2f; // Variable faltante añadida
    float followVelocity = 3f;
    float distanciaMinima = 2f;
    public Transform player;

    public GameObject prefabProyectil;
    public bool canShoot = true;

    [Header("Change movement")]
    public bool itsStop = true;

    [Header("Change shoot")]
    public bool isShooter = false;

    void Update()
    {
        float playerDistance = Vector2.Distance(transform.position, player.position); // Movido aquí para actualización constante

        followPlayer();
        if (withinDetectionArea() && isShooter)
        {
            StartCoroutine(shootProjectile());
        }
    }

    void followPlayer()
    {
        float playerDistance = Vector2.Distance(transform.position, player.position);
        Vector2 direction = (player.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, radioDetection);

        if (hit.collider != null && hit.collider.CompareTag("Player") && !itsStop)
        {
            transform.Translate(direction * followVelocity * Time.deltaTime);
        }

        if (playerDistance <= radioDetection && playerDistance > distanciaMinima && itsStop)
        {
            transform.Translate(direction * followVelocity * Time.deltaTime);
        }
    }

    bool withinDetectionArea()
    {
        float playerDistance = Vector2.Distance(transform.position, player.position);
        return playerDistance <= radioDetection && playerDistance > distanciaMinima;
    }

    IEnumerator shootProjectile()
    {
        if (prefabProyectil != null && canShoot)
        {
            canShoot = false;
            GameObject proyectil = Instantiate(prefabProyectil, transform.position, Quaternion.identity);
            ProjectileEnemy scriptProyectil = proyectil.GetComponent<ProjectileEnemy>();
            scriptProyectil.SetDirectionShoot((player.position - transform.position).normalized);
            yield return new WaitForSeconds(coolDownProjectile);
            canShoot = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDetection);
    }
}