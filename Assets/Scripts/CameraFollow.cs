using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smooth = 0.125f; 


    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, -10) ;
    }
}
