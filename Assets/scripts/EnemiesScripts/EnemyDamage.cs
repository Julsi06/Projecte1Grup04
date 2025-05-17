using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int health = 100; // Enemy health
    private Animator animator;

    void Start()
    {
        // Obtener referencia al componente Animator
        animator = GetComponent<Animator>();
        if (animator == null) Debug.LogError("Falta el Animator en: " + gameObject.name);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        animator.SetTrigger("isAttacked");
        if (health <= 0)
        {
            die(); // Call die() when health reaches zero
        }
    }

    private void die()
    {
        Destroy(gameObject); // Destroy enemy object
    }
}