using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Tecla Q presionada"); // Mensaje de prueba
            Shoot();
        }
    }

    void Shoot()
    {
        if (firePoint == null)
        {
            Debug.LogError("FirePoint no está asignado en el Inspector.");
            return;
        }

        if (bulletPrefab == null)
        {
            Debug.LogError("BulletPrefab no está asignado en el Inspector.");
            return;
        }

        Debug.Log("Disparando...");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}