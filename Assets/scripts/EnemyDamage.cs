using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int health = 100; // Enemy health
    private Animator animator;


    void Start()
    {
        // Obtener referencia al componente Animator
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        
        animator.SetTrigger("isAttacked");

        Debug.Log($"Enemy took {damage} damage. Current health: {health}");
        if (health <= 0)
        {
            die(); // Call die() when health reaches zero
        }

        
    }

    private void die()
    {
        Destroy(gameObject); // Destroy enemy object
      
    }


    //lo pongo aqui, porque este es un script que todo tipo de enemigo tendra
    private void OnCollisionEnter2D(collision2D collision)
    {
        if(collision.gameObject.compareTag("Player"))
        {
            Vector2 direccionDanio = new Vector2(transform.position.x, 0);
            collision.gameObject.GetComponent<PlayerRecibeDaño>().RecibeDanio(direccionDanio, 1);
        }
    }
}

