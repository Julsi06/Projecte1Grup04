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

    private PlayerMovement playerMovement;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>(); // Inicializa playerMovement
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
            animator.SetTrigger("isReloading");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("isShooting");
        }
        else{}
    }

    public void SetAttackAnimation(int index)
    {
        Debug.Log("Iniciando ataque con índice: " + index); // Mensaje de depuración
                                                            // Reseteamos todos los triggers antes para evitar conflictos
        animator.ResetTrigger("attack1");
        animator.ResetTrigger("attack2");
        animator.ResetTrigger("attack3");
        animator.ResetTrigger("attack4");
        switch (index)
        {
            case 0:
                animator.SetTrigger("attack1");
                break;
            case 1:
                animator.SetTrigger("attack2");
                break;
            case 2:
                animator.SetTrigger("attack3");
                break;
            case 3:
                animator.SetTrigger("attack4");
                break;
            default:
                Debug.LogWarning("Índice de ataque inválido en SetAttackAnimation: " + index);
                break;
        }
    }

    public void FinishAttack()
    {
        Debug.Log("FinishAttack llamado."); // Mensaje de depuración
        if (playerMovement != null)
        {
            playerMovement.StartNextAttack();
        }
        else
        {
            Debug.LogWarning("playerMovement es null en FinishAttack.");
        }
    }
}