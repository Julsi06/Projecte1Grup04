using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    [SerializeField] private int maxBullets = 6;
    private int bulletsInt;

    void Start()
    {
        bulletsInt = maxBullets; // Assign the max bullets value from the weapon object
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Tecla Q presionada"); // Mensaje de prueba
            if (bulletsInt != 0)
            {
                Shoot();
                bulletsInt--;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            bulletsInt = maxBullets;
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
