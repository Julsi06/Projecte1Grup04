using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed;
    private Rigidbody2D player;
    private bool isFacingRight = true;
    // private Animator animator;

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        player.drag = 0;

       // animator = GetComponent<Animator>(); // ← Corregido
    }

    private void FixedUpdate()
    {
        float moveInput = Input.GetAxisRaw("Horizontal"); // -1, 0, 1
        float verticalVelocity = player.velocity.y;

        // Aplicar velocidad
        player.velocity = new Vector2(moveInput * horizontalSpeed, verticalVelocity);

        // Animaciones
       // animator.SetBool("isWalking", moveInput != 0);

        // Girar personaje si es necesario
        if (moveInput > 0 && !isFacingRight)
        {
            flip();
        }
        else if (moveInput < 0 && isFacingRight)
        {
            flip();
        }
    }

    private void flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }


}
