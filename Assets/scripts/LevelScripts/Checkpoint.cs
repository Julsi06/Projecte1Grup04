using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chechkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnManager.Instance.SetSpawnPoint(transform.position);
            Debug.Log("Checkpoint reached on: " + transform.position);
        }
    }
}
