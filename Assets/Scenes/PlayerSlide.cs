using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rb.position += new Vector2(Input.GetAxis("Horizontal"), 0) * Time.deltaTime * speed;
        if (Mathf.Abs(rb.velocity.y) < 0.001f && Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
        }
    }
}