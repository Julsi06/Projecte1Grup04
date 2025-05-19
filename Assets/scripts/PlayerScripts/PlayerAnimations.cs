using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Rigidbody2D player;
    private Animator animator;


    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalVelocity = player.velocity.y;
        animator.SetFloat("xVelocity", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("yVelocity", verticalVelocity);

        animator.SetBool("isRunning", Mathf.Abs(player.velocity.x) > 0.1f);

        PlayerMovement movement = GetComponent<PlayerMovement>();
        animator.SetBool("isDoubleJumping", movement.IsDoubleJumping);
        if(Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("isReloading", true);
        }
    }
}