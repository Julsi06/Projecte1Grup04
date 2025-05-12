using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float lastXPosition;
    public Transform player; // Indica la posición del player
    public float detectionRadius = 5.0f;
    public float speed = 15.0f;
    private bool isFacingRight = false; // Indica si el personaje mira a la derecha
    
    private Rigidbody2D rb;
    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastXPosition = transform.position.x;
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
        // Verifica si hay que cambiar la dirección del personaje
        if (transform.position.x > lastXPosition && !isFacingRight) // Se mueve a la derecha pero está mirando a la izquierda
        {
            flip();
        }
        else if (transform.position.x < lastXPosition && isFacingRight) // Se mueve a la izquierda pero está mirando a la derecha
        {
            flip();
        }

        // Actualizamos la posición para el siguiente frame
        lastXPosition = transform.position.x;
    }

    // Mover el enemigo en FixedUpdate para que funcione bien con la física
    void FixedUpdate()
    {
        rb.velocity = direction * speed * Time.fixedDeltaTime;
        //rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    private void flip()
    {
        isFacingRight = !isFacingRight; // Cambia la dirección
        Debug.Log("flip enemy");                        //viene de fabrica del unity (x,y,z);
        transform.Rotate(0f, 180f, 0f);
    }

}
