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
    private LivesManager livesManager;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>(); // Inicializa playerMovement
        livesManager = GetComponent<LivesManager>();
    }

    void Update()
    {
        float verticalVelocity = player.velocity.y;
        animator.SetFloat("xVelocity", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("yVelocity", verticalVelocity);

        animator.SetBool("isRunning", Mathf.Abs(player.velocity.x) > 0.1f);

        PlayerMovement movement = GetComponent<PlayerMovement>();
        animator.SetBool("isDoubleJumping", movement.IsDoubleJumping);

        if(livesManager.currentLives == 0)
        {
            animator.SetBool("isDying", true);
        }
    }

    public void SetAttackAnimation(int index)
    {
        Debug.Log("Iniciando ataque con índice: " + index); // Mensaje de depuración
                                                            // Reseteamos todos los triggers antes para evitar conflictos
        animator.SetBool("isAttacking", true);

        animator.ResetTrigger("attack1");
        animator.ResetTrigger("attack4");
        switch (index)
        {
        case 0:
              animator.SetTrigger("attack1");
              break;
        case 1:
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
        animator.SetBool("isAttacking", false);
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