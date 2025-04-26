using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingPortal : MonoBehaviour
{
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
        gameobjectToMove.transform.position = new Vector3(-19.26f, -32.62f, 0f);
    }
}
