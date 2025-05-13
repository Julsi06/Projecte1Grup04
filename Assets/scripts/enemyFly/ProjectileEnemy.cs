using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : MonoBehaviour
{
    public float projectileVelocity = 10f; // Speed of the projectile
    public float lifeTime = 2f; // How long the projectile exists before auto-destroy
    private Vector2 shootDirection; // Stores shooting direction

    void Start()
    {
        Destroy(gameObject, lifeTime); // Schedule destruction after lifetime
    }

    public void SetDirectionShoot(Vector2 direction)
    {
        shootDirection = direction.normalized; // Set normalized direction
    }

    void Update() 
    {
        
        // lifeTime es un valor constante, Time.deltaTime es el tiempo entre frames
        transform.Translate(shootDirection * projectileVelocity * Time.deltaTime);
    }
}