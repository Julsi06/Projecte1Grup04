using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 1;
    private Rigidbody2D rb;

    void Start()
    {
        // Asigna automáticamente el Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
      
        // Mueve la bala hacia la derecha con la velocidad indicada
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Printeo para saber si todo va bien
        EnemyBat enemy = hitInfo.GetComponent<EnemyBat>();

        if (enemy != null)
        {
            if(hitInfo.gameObject.CompareTag("enemy")) enemy.TakeDamage(damage);
        }

        // Destruye la bala al colisionar
        Destroy(gameObject);
    }
}




        