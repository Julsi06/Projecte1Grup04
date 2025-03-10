using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed; // S'estableix que la seva velocitat s'indicarà a Unity
    private Rigidbody2D player; // S'estableix que tindrà físiques
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

        //funcio de unity
        float moveInput = Input.GetAxisRaw("Horizontal"); // -1 (izquierda), 0 (parado), 1 (derecha)


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

        // Verifica si hay que cambiar la dirección del personaje
        if (moveInput > 0 && !isFacingRight) // Se mueve a la derecha pero está mirando a la izquierda
        {
            flip();
        }
        else if (moveInput < 0 && isFacingRight) // Se mueve a la izquierda pero está mirando a la derecha
        {
            flip();
        }
        

        // Aplica la velocidad manteniendo el eje Y (vertical) sin cambios
        player.velocity = new Vector2(horizontalVelocity, verticalVelocity);
    }

    private void flip()
    {
        isFacingRight = !isFacingRight; // Cambia la dirección
        //viene de fabrica del unity (x,y,z);
        transform.Rotate(0f, 180f, 0f);
    }
}


