using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    /*
    This window should have a respective window crank and should raise it's height
    when the crank's angle (GetValue) is closer to 1 and should lower when it gets
    closer to 0
    */

    public TurnWindowCrank windowCrank;

    private float closedHeight;
    private float openHeight;

    private float pixelToUnitRatio = 3f;

    private void Awake(){
        openHeight = transform.position.y;
        closedHeight = openHeight + pixelToUnitRatio;
    }

    private void Update(){
        var crankValue = windowCrank.GetValue();

        var raisedHeight = pixelToUnitRatio * crankValue;

        var proposedHeight = openHeight + raisedHeight;

        transform.position = new Vector3(transform.position.x,proposedHeight,transform.position.z);
    }

}
