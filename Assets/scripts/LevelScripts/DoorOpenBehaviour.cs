using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenBehaviour : MonoBehaviour
{
    private Animator animator;
    private Collider2D doorCollider;
    private bool isOpen = false;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        doorCollider = GetComponent<Collider2D>();
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            animator.SetTrigger("Open");
            isOpen = true;
        }
    }

    public void DisableCollider()
    {
        doorCollider.enabled = false;
    }
}
