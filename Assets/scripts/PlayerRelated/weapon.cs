using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    [SerializeField] private int bulletLoad = 3;
    private int actualBullet;

    private void Awake()
    {
        actualBullet = bulletLoad;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && actualBullet > 0)
        {
            Debug.Log("Tecla Q presionada"); // Mensaje de prueba
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            reload();
        }
    }


    void reload()
    {
        actualBullet = bulletLoad;
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
        actualBullet--;
    }

  
}