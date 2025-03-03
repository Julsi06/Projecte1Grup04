using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D player;
    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // If the player presses "space", the GameObject will jump
        // The GameObject jumps for as long as the player presses "space" (arreglar)
        if (Input.GetKeyDown(KeyCode.Space))
            player.velocity = new Vector2(player.velocity.x, speed);
    }
}
