using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloseBehaviour : MonoBehaviour
{
    private Animator animator;
    private Collider2D doorCollider;
    private bool isOpen = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        doorCollider = GetComponent<Collider2D>();
        doorCollider.enabled = false;
    }

    public void CloseDoor()
    {
        if (isOpen)
        {
            animator.SetTrigger("Close");
            isOpen = false;
            doorCollider.enabled = true;
        }
    }

    public void EnableCollider()
    {
        doorCollider.enabled = true;
    }
}
