using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEndGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider coll){
        if(coll.tag == "Player"){
            EventManager.TriggerEvent("WinCondition",null);
            Debug.Log("Win Condition");
        }
    }
}
