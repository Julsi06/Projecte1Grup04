using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HangingFromPipe : MonoBehaviour
{
    [SerializeField] private LayerMask pipeLayer;

    private Rigidbody2D player;

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckHangingFromPipe();
        
    }

    private void CheckHangingFromPipe()
    {
        Vector2 raycastOrigin = transform.position + new Vector3(0f, 1.05f);

        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.up, 0.5f, pipeLayer);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Pipe"))
        {
            player.gravityScale = -1f;
            if (Input.GetKey(KeyCode.S))
            {
                player.gravityScale = 1.0f;
            }
        }
        else
        {
            player.gravityScale = 1.0f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 raycastOrigin = transform.position + new Vector3(0f, 1.5f);
        Debug.DrawRay(raycastOrigin, Vector2.up * 0.5f, Color.red);
    }
}
