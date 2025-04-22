using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonDoor : MonoBehaviour
{
    public bool isClosed = true;
    [SerializeField] private GameObject door;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isClosed = false;
            Destroy(door);
        }
    }
}
