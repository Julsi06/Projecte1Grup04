using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed; // S'estableix que la seva velocitat s'indicarà a Unity
    private Rigidbody2D player; // S'estableix que tindrà físiques
    public HeartManager heartManager; // Relaciona el moviment del jugador amb el comptador de cors
        
    private void Awake()
    {
        // S'agafaran les dades del rigidbody2d del player a Unity
        player = GetComponent<Rigidbody2D>();
    }

    //update is called once per frame
    private void Update()
    {

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            player.velocity = new Vector2((0-speed), 0);
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            player.velocity = new Vector2(speed, 0);

        float horizontalSpeed = speed;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            player.velocity = new Vector2((0 - horizontalSpeed), 0);
        }
       
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            player.velocity = new Vector2(horizontalSpeed, 0);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("heart"))
        {
            heartManager.heartCounter++;
            Destroy(collision.gameObject);
        }
    }
}
