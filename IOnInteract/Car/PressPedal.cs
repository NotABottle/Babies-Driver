using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressPedal : MonoBehaviour, IOnInteract
{
    private InteractableType interactableType = InteractableType.Vertical;

    private bool isSelected = false;
    public float pullDistance = 1f;

    [Range(0,1f)]
    public float returnToNeutralSpeed = 1f;
    private float maxHandHeight;
    private float minHandHeight;
    private float prevHandHeight;
    public Transform handTransform;
    public Camera spriteCamera;
    private float maxPedalHeight;
    private float minPedalHeight;

    public InteractableType GetInteractableType()
    {
        return interactableType;
    }

    public void DeSelect()
    {
        isSelected = false;
    }

    public void Select()
    {
        isSelected = true;
        maxHandHeight = handTransform.position.y;
        minHandHeight = maxHandHeight - pullDistance;
        prevHandHeight = handTransform.position.y;
    }

    public float GetValue()
    {
        float deltaPedalHeight = maxPedalHeight - transform.position.y;
        return deltaPedalHeight/1f;
    }

    public void Update()
    {
        float deltaPedalHeight = maxPedalHeight - transform.position.y;
        if(!isSelected && !Mathf.Approximately(0,deltaPedalHeight)){
            float deltaHeight = returnToNeutralSpeed * Time.deltaTime;
            if(Mathf.Abs(deltaHeight) > Mathf.Abs(deltaPedalHeight)){
                deltaPedalHeight = 0f;
            }
            else if(deltaPedalHeight > 0f){
                deltaPedalHeight -= deltaHeight;
            }

            transform.position = new Vector3(transform.position.x,maxPedalHeight - deltaPedalHeight,transform.position.z);
            return;
        }
        else if(!isSelected) return;

        MoveHand();
        MovePedal();
    }

    public void MoveHand()
    {
        Vector2 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = spriteCamera.ScreenToWorldPoint(mouseScreenPosition);

        float newHandHeight = mouseWorldPosition.y;
        float clampedHandHeight = Mathf.Clamp(newHandHeight,minPedalHeight,maxPedalHeight);

        handTransform.position = new Vector3(handTransform.position.x,clampedHandHeight,handTransform.position.z);
    }

    private void Start(){
        maxPedalHeight = transform.position.y;
        minPedalHeight = maxPedalHeight - pullDistance;
    }

    private void MovePedal()
    {
        //Get new hand height
        float newHandHeight = handTransform.position.y;
        //get change in hand height
        float deltaHandHeight = newHandHeight - prevHandHeight;
        //add change to current height in temp variable
        float currentPedalHeight = transform.position.y;
        float proposedPedalHeight = currentPedalHeight + deltaHandHeight;
        //Clamp within bounds
        float clampedPedalHeight = Mathf.Clamp(proposedPedalHeight,minPedalHeight,maxPedalHeight);
        //Set height
        transform.position = new Vector3(transform.position.x,clampedPedalHeight,transform.position.z);
        //Set previous
        prevHandHeight = newHandHeight;
    }
}
