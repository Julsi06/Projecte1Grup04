using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadius : MonoBehaviour
{
    public Transform player; // Indica la posición del player
    public float detectionRadius = 5.0f;
    public float speed = 15.0f;

    private Rigidbody2D rb;
    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        Debug.Log(distanceToPlayer);
        // Si el jugador está dentro del radio de detección
        if (distanceToPlayer < detectionRadius)
        {

            Debug.Log("Entro");
            Vector2 tmpDirection = (player.position - transform.position).normalized;
            direction = new Vector2(tmpDirection.x, 0); // Se mueve solo en X
        }
        else
        {
            direction = Vector2.zero; // No se mueve
        }
    }

    // Mover el enemigo en FixedUpdate para que funcione bien con la física
    void FixedUpdate()
    {
        rb.velocity = direction * speed * Time.fixedDeltaTime;
        //rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
}
