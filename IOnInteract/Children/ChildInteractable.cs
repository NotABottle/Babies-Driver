using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildInteractable : MonoBehaviour
{
    private PullLookingChild lookingChild;
    private PullHangingChild hangingChild;
    private PullGrappledChild grappledChild;

    private IOnInteract onInteract;

    private List<MonoBehaviour> interactionBehaviors;

    private void Awake(){
        lookingChild = GetComponent<PullLookingChild>();
        hangingChild = GetComponent<PullHangingChild>();
        grappledChild = GetComponent<PullGrappledChild>();
        interactionBehaviors = new List<MonoBehaviour>();
        interactionBehaviors.Add(lookingChild);
        interactionBehaviors.Add(hangingChild);
        interactionBehaviors.Add(grappledChild);
    }

    private void OnMouseDown(){
        Debug.Log("OnMouseDown");
        foreach(MonoBehaviour behaviour in interactionBehaviors){
            if(behaviour.enabled){
                onInteract = (IOnInteract) behaviour;
            }
        }

        if(onInteract == null) return;
        onInteract.Select();
        EventManager.TriggerEvent("OnSelectInteractable",null);
    }

    private void OnMouseUp(){
        if(onInteract == null) return;
        onInteract.DeSelect();
        onInteract = null;
        EventManager.TriggerEvent("OnDeselectInteractable",null);
    }
}
