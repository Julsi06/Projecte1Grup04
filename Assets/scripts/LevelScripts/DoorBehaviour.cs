using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    private buttonDoor doorState;
    private Collider2D doorCollider; 

    void Start()
    {
        doorState = GetComponent<buttonDoor>();
        doorCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (!doorState.doorIsClosed)
            doorCollider.enabled = false;
    }
}
