using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UIElements;

public class PlataformaMovil : MonoBehaviour
{
    // Parameters for the platforms
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed;
    [SerializeField] private float stopTime;
    private bool waitingPlatform = false;
    private Vector3 nextPosition;

    private void Start()
    {
        nextPosition = pointB.position;
    }

    // Manages the platforms' movement and the direction of the journey that they do
    private void Update()
    {
        // Doesn't do anything on the Update method if the platform is paused or no points have been assigned
        // to the platforms
        if (waitingPlatform)
            return;

        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

        // Checks which point is next
        if (transform.position == nextPosition)
        {
            nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;
            StartCoroutine(WaitAndContinue());
        }
    }

    private IEnumerator WaitAndContinue()
    {
        // Tell the platform to wait
        waitingPlatform = true;
        yield return new WaitForSeconds(stopTime);
        // And then continue
        waitingPlatform = false;
    }
}