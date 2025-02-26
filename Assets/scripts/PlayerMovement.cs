using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed; // S'estableix que la seva velocitat s'indicarà a Unity
    private Rigidbody2D player; // S'estableix que tindrà físiques
    private void Awake()
    {
        // S'agafaran les dades del rigidbody2d del player a Unity
        player = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        // El jugador es mourà depennet de les tecles "A" (esquerra, cap a 1) i "D" (dreta, cap a -1)
        player.velocity = new Vector2(Input.GetAxis("Horizontal")*speed, player.velocity.y);
    }
}
