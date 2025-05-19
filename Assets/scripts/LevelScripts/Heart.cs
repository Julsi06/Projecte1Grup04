using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private LivesManager livesManager;

    private void Start()
    {
        livesManager = FindObjectOfType<LivesManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (livesManager != null)
            {
                livesManager.GainLife();
            }

            Destroy(gameObject);
        }
    }
}
