using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataformasTemporal : MonoBehaviour
{
    // Tiempo que esperará la plataforma antes de caer
    [SerializeField] private float waitTime;

    // Referencia al componente Rigidbody2D para controlar la física
    private Rigidbody2D rb2D;

    // Velocidad de rotación (no está siendo utilizada en este código)
    [SerializeField] private float velocidadRotacion;

    // Variable booleana para indicar si la plataforma está en caída (no se usa actualmente)
    private bool caida = false;

    private void Start()
    {
        // Obtiene el Rigidbody2D del objeto al inicio
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Se ejecuta cuando la plataforma colisiona con otro objeto
    private void OnCollision2D(Collision2D other)
    {
        // Verifica si el objeto con el que colisiona tiene la etiqueta "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            // Inicia la corutina que hará caer la plataforma después de esperar un tiempo
            StartCoroutine(Caida(other));
        }
    }

    private IEnumerator Caida(Collision2D other)
    {
        // Espera la cantidad de segundos especificada en waitTime antes de ejecutar el resto del código
        yield return new WaitForSeconds(waitTime); // FUNCION DE UNITY

        rb2D.gravityScale = 1;

        // Aquí la plataforma comenzará a caer
    }
}
