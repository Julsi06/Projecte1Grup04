using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    [SerializeField] private int bulletLoad = 3;
    private int actualBullet;
    public GameObject bulletUI;
    public GameObject bulletUI1;
    public GameObject bulletUI2;


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
        bulletUI.SetActive(true);
        bulletUI1.SetActive(true);
        bulletUI2.SetActive(true);
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

        // Oculta las balas según las que queden
        if (actualBullet == 2)
            bulletUI2.SetActive(false);
        else if (actualBullet == 1)
            bulletUI1.SetActive(false);
        else if (actualBullet == 0)
            bulletUI.SetActive(false);
    }


}