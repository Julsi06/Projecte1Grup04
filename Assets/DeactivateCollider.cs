using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DeactivateCollider : MonoBehaviour
{
    private Collider2D invisibleWall;
    private bool isActive = true;

    void Start()
    {
        invisibleWall = GetComponent<Collider2D>();
    }

    public void OpenWall()
    {
        if (isActive)
        {
            isActive = false;
            invisibleWall.enabled = false;
        }
    }
}
