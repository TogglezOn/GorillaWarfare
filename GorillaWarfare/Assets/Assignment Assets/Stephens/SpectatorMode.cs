using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorMode : MonoBehaviour
{
    public Transform target;  // the target object to follow
    public float distance = 10f;  // the distance to keep from the target
    public float height = 5f;  // the height to keep above the target
    public float smoothSpeed = 0.125f;  // the speed at which to smoothly follow the target

    private Vector3 offset;

    void Start()
    {
        // calculate the initial offset based on the target's position and the desired distance and height
        offset = new Vector3(0f, height, -distance);
    }

    void LateUpdate()
    {
        // smoothly move the camera to follow the target
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // always look at the target
        transform.LookAt(target);
    }
}
