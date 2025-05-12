using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : MonoBehaviour
{
    [serialized] private float radioDetection = 5f;
    float followVelocity = 3f;
    public Transform player;


    void SeguirJugador()
    {
        vector2 direccion = (SeguirJugador, position - transform.position).normalized;
        //raycast en la direccion del jugador 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direccion, radioDetection);

        //condiciuon si el raycast alcanza el jugador, el objecte el segueix
        if(hit.collider != null && hit.Collider.CompareTag("Player"))
        {
            transform.Translate(direccion * followVelocity * Time.deltaTime);
        }
    }

}
