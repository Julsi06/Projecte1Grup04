using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonDoor : MonoBehaviour
{
    [SerializeField] private LayerMask doorLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
        }
    }
}
