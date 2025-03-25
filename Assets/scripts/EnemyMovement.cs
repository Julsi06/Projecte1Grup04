using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player; // Indica la posición del player
    public float detectionRadius = 5.0f;
    public float speed = 15.0f;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Si el jugador está dentro del radio de detección
        if (distanceToPlayer < detectionRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            movement = new Vector2(direction.x, 0); // Se mueve solo en X
        }
        else
        {
            movement = Vector2.zero; // No se mueve
        }
    }

    // Mover el enemigo en FixedUpdate para que funcione bien con la física
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
