using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    //public Vector3 offset;
    [Range(1, 10)] public float smoothFactor;

    private void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        //Vector3 targetPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, target.position, smoothFactor*Time.deltaTime);
        transform.position = smoothPosition;
    }
}
