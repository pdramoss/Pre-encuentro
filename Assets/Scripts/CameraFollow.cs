using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smooth = 0.125f; 
    public Vector3 offset; 


    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smooth * Time.deltaTime);
        if(smoothedPosition.y >= 0.08f){
            smoothedPosition.y = 0.08f;
        }
        else if(smoothedPosition.y <= -0.08f){
            smoothedPosition.y = -0.08f;
        }
        transform.position = smoothedPosition;
    }
}
