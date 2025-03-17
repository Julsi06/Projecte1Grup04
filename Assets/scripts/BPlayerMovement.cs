using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed; // S'estableix que la seva velocitat s'indicar� a Unity
    private Rigidbody2D player; // S'estableix que tindr� f�siques
    private bool isFacingRight = true; // Indica si el personaje mira a la derecha

    private void Awake()
    {
        // S'agafaran les dades del rigidbody2d del player a Unity
        player = GetComponent<Rigidbody2D>();
        // Disable drag to avoid inertia
        player.drag = 0;
    }

    //update is called once per frame
    private void FixedUpdate()
    {
        // Captura la velocidad vertical actual para no afectar el salto
        float verticalVelocity = player.velocity.y;
        float horizontalVelocity = horizontalSpeed;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            horizontalVelocity = -horizontalSpeed;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            horizontalVelocity = horizontalSpeed;
        }
        else
        {
            // Detiene solo el movimiento horizontal (drag) cuando no se presionan teclas
            horizontalVelocity = 0;
        }
        
        // Aplica la velocidad manteniendo el eje Y (vertical) sin cambios
        player.velocity = new Vector2(horizontalVelocity, verticalVelocity);
    }

    private void flip()
    {
        isFacingRight = !isFacingRight; // Cambia la direcci�n
        //viene de fabrica del unity (x,y,z);
        transform.Rotate(0f, 180f, 0f);
    }
}


