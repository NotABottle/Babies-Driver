using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private IOnInteract onInteract;

    private void Awake(){
        onInteract = GetComponent<IOnInteract>();
    }

    private void OnMouseDown(){
        //Declare item as selected
        //Lock Hand
        onInteract.Select();
        EventManager.TriggerEvent("OnSelectInteractable", null);
    }

    private void OnMouseUp(){
        //Declare item as not selected
        //Unlock Hand
        onInteract.DeSelect();
        EventManager.TriggerEvent("OnDeselectInteractable",null);
    }
    
}
