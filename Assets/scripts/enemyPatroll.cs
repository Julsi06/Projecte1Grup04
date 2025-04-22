using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Transform[] puntosMovimientoEnemigo;

    [SerializeField] private float velocidadMovimientoEnemigo;

    private bool isFacingRight = true; // Indica si el personaje mira a la derecha

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
    private void flip()
    {
        isFacingRight = !isFacingRight; // Cambia la dirección
                                        //viene de fabrica del unity (x,y,z);
        transform.Rotate(0f, 180f, 0f);
    }

}
