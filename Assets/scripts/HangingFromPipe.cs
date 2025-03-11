using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HangingFromPipe : MonoBehaviour
{
    [SerializeField] private LayerMask pipeLayer;
    //[SerializeField] private float hangingSpeed;

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
        
    }

    private void CheckHangingFromPipe()
    {
        Vector2 raycastOrigin = transform.position + new Vector3(0f, 1.01f);
        isHanging = false;

        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.up, 0.1f, pipeLayer);
        if (hit.collider != null && hit.collider.gameObject.name == "Pipe")
        {
            isHanging = true;
        }
        if (isHanging)
        {
            player.gravityScale = -1f;
            // cal remodificar la gravetat quna no estigui tocant pipe
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 raycastOrigin = transform.position + new Vector3(0f, 1.01f);
        Debug.DrawRay(raycastOrigin, Vector2.up * 0.1f, Color.red);
    }
}
