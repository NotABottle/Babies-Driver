using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{

    private void OnEnable() => EventManager.StartListening("RMBDown", RotateCam);
    private void OnDisable() => EventManager.StopListening("RMBDown", RotateCam);

    private void RotateCam(Dictionary<string, object> obj)
    {
        transform.RotateAround(transform.position,Vector3.up,180);
    }

}
