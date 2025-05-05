using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToxicWater : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("toxicWater"))
            Death();
    }

    private void Death()
    {
        gameObject.transform.position = respawnPoint.position;
    }
}
