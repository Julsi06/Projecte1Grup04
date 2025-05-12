using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : MonoBehaviour
{
    [SerializeField] private float radioDetection = 5f;
    float followVelocity = 3f;
    float distanciaMinima = 2f;
    public Transform player;
    [Header("Change movement")]
    public bool itsStop = true;

    void Update()
    {
        SeguirJugador();
    }

    void SeguirJugador()
    {
        Vector2 direccion = (player.position - transform.position).normalized;
        // Raycast en la dirección del jugador 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direccion, radioDetection);

        // Condición si el raycast alcanza el jugador, el objeto lo sigue
        if (hit.collider != null && hit.collider.CompareTag("Player") && !itsStop)
        {
            transform.Translate(direccion * followVelocity * Time.deltaTime);
        }

        float playerDistance = Vector2.Distance(transform.position, player.position);
        if (playerDistance <= radioDetection && playerDistance > distanciaMinima && itsStop)
        {
            transform.Translate(direccion * followVelocity * Time.deltaTime);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDetection);
    }
}