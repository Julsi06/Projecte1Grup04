using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UIElements;

public class PlataformaMovil : MonoBehaviour
{
    // Array para contener varios puntos
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float velocidadMovimiento = 3f;
    [SerializeField] private float stopTime = 2f;

    private int siguientePlataforma = 1;
    private bool ordenPlataformas = true;
    private bool waitingPlatform = false;


   // Manages the platforms' movement and the direction of the journey that they do
    private void Update()
    {
        // Doesn't do enything on the Update method if the platform is paused or no points have been assigned
        // to the platforms
        if (waitingPlatform || puntosMovimiento.Length == 0)
            return;

        if (ordenPlataformas && siguientePlataforma + 1 >= puntosMovimiento.Length)
        {
            ordenPlataformas = false;
        }

        if (!ordenPlataformas && siguientePlataforma <= 0)
        {
            ordenPlataformas = true;
        }

        transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[siguientePlataforma].position, velocidadMovimiento * Time.deltaTime);

        if(Vector2.Distance(transform.position, puntosMovimiento[siguientePlataforma].position) < 0.05f)
        {
            // Only checking if the platform is in the first/last point
            bool lastPoint = siguientePlataforma == puntosMovimiento.Length - 1;
            bool firstPoint = siguientePlataforma == puntosMovimiento.Length - puntosMovimiento.Length;

            if (lastPoint || firstPoint)
                StartCoroutine(WaitAndContinue());
            // If the condition isn't true, does the logic normally, and immediately changes the platform's point
            else
            {
                if (ordenPlataformas)
                {
                    siguientePlataforma++;
                }
                else
                {
                    siguientePlataforma--;
                }
            }
        }
    }

    private IEnumerator WaitAndContinue()
    {
        waitingPlatform = true;
        yield return new WaitForSeconds(stopTime);

        // After waiting, changes the platform's point
        if (ordenPlataformas)
        {
            siguientePlataforma++;
        }
        else
        {
            siguientePlataforma--;
        }

        waitingPlatform = false;
    }

    // Sets the platform as the player object's parent, and lets them move together
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform);
        }
    }

    // Disassociates the platform as the player object's parent
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}