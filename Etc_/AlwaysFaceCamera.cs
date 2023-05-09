using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFaceCamera : MonoBehaviour
{
    private void Update(){
        var vecFromToCamera = Camera.main.transform.position - transform.position;
        transform.forward = -vecFromToCamera;
    }
}
