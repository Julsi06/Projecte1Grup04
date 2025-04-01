using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Transform[] puntosMovimientoEnemigo;

    [SerializeField] private float velocidadMovimientoEnemigo;


    private int siguienteEnemigo = 1;
    private bool ordenEnemigos = true;


    // este método maneja el movimiento del enemigo y la dirección del recorrido.
    private void Update()
    {
        if (ordenEnemigos && siguienteEnemigo + 1 >= puntosMovimientoEnemigo.Length)
        {
            ordenEnemigos = false;
        }

        if (!ordenEnemigos && siguienteEnemigo <= 0)
        {
            ordenEnemigos = true;
        }

        if (Vector2.Distance(transform.position, puntosMovimientoEnemigo[siguienteEnemigo].position) < 0.1f)
        {
            if (ordenEnemigos)
            {
                siguienteEnemigo += 1;
            }
            else
            {
                siguienteEnemigo -= 1;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, puntosMovimientoEnemigo[siguienteEnemigo].position, velocidadMovimientoEnemigo * Time.deltaTime);
    }
}
