using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullGrappledChild : MonoBehaviour, IOnInteract
{
    private InteractableType interactableType = InteractableType.Horizontal;

    private bool isSelected = false;
    public float pullDistance = 1f;
    
    private float minHandX;
    private float maxHandX;
    private float prevHandX;

    public Transform handTransform;
    public Camera spriteCamera;
    private float maxChildX;
    private float minChildX;

    public void DeSelect()
    {
        isSelected = false;
    }

    public InteractableType GetInteractableType()
    {
        return interactableType;
    }

    public float GetValue()
    {
        if(isSelected) return 0f;

        float deltaChildX = Mathf.Abs(maxChildX - transform.position.x);
        return deltaChildX/1f;
    }

    public void MoveHand()
    {
        Vector2 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = spriteCamera.ScreenToWorldPoint(mouseScreenPosition);

        float newHandX = mouseWorldPosition.x;
        float clampedHandX;
        if(pullDistance < 0){
            clampedHandX = Mathf.Clamp(newHandX,maxHandX,minHandX);
        }
        else if(pullDistance > 0){
            clampedHandX = Mathf.Clamp(newHandX,minHandX,maxHandX);
        }else{
            clampedHandX = 0;
        }

        handTransform.position = new Vector3(clampedHandX,handTransform.position.y,handTransform.position.z);
    }

    public void Select()
    {
        isSelected = true;
        maxHandX = handTransform.position.x;
        minHandX = maxHandX - pullDistance;
        prevHandX = handTransform.position.x;
    }

    private void Awake(){
        maxChildX = transform.position.x;
        minChildX = maxHandX - pullDistance;
    }

    public void Update()
    {
        if(!isSelected) return;

        MoveHand();
        MoveChild();
    }

    private void MoveChild(){
        float newHandX = handTransform.position.x;
        float deltaHandX = newHandX - prevHandX;

        float currentChildX = transform.position.x;
        float proposedChildX = currentChildX + deltaHandX;

        float clampedChildX;
        if(proposedChildX > 0){
            clampedChildX = Mathf.Clamp(proposedChildX,minChildX,maxChildX);
        }
        else if(proposedChildX < 0){
            clampedChildX = Mathf.Clamp(proposedChildX,maxChildX,minChildX);
        }else{
            clampedChildX = 0;
        }

        transform.position = new Vector3(clampedChildX,transform.position.y,transform.position.z);

        prevHandX = newHandX;
    }
}
