using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingPortal : MonoBehaviour
{
    [SerializeField] private Transform pointDestination;
    GameObject gameobjectToMove;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("portal"))
        {
            MoveToNewPosition();
        }
    }

    private void MoveToNewPosition()
    {
        transform.position = pointDestination.position;
    }
}
