using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private void Update(){
        if(Input.GetMouseButtonDown(1)) EventManager.TriggerEvent("RMBDown",null);

        if(Input.GetMouseButtonDown(0)) EventManager.TriggerEvent("LMBDown",null);

        if(Input.GetMouseButtonUp(0))   EventManager.TriggerEvent("LMBUp",null);

    }
}
