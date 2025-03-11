using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HangingFromPipe : MonoBehaviour
{
    [SerializeField] private LayerMask pipeLayer;
    [SerializeField] private float hangingSpeed;

    private Rigidbody2D player;
    private bool isHanging = false;

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckHangingFromPipe();
        while (isHanging)
        {
            player.gravityScale = 0f;
        }
    }

    private void CheckHangingFromPipe()
    {
        Vector2 raycastOrigin = transform.position + new Vector3(0f, 1.01f);
        isHanging = false;

        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.up, 0.5f, pipeLayer);
        if (hit.collider != null && hit.collider.gameObject.name == "Pipe")
        {
            isHanging = true;
            Debug.Log("Hanging from pipe");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 raycastOrigin = transform.position + new Vector3(0f, 1.01f);
        Debug.DrawRay(raycastOrigin, Vector2.up * 0.5f, Color.red);
    }
}
