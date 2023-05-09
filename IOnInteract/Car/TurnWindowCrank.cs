using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnWindowCrank : MonoBehaviour, IOnInteract
{
    private InteractableType interactableType = InteractableType.Radial;

    private bool isSelected = false;
    
    private float maxAngle = 1800f;
    private float prevAngle;
    private float Angle;

    private float radius;

    public Transform handTransform;
    public Camera spriteCamera;

    public void DeSelect()
    {
        isSelected = false;
    }

    public void Select()
    {
        isSelected = true;
        radius = Vector2.Distance(handTransform.position,transform.position);
        prevAngle = Vector2.Angle(Vector2.up, spriteCamera.WorldToScreenPoint(handTransform.position) - spriteCamera.WorldToScreenPoint(transform.position));
    }

    public InteractableType GetInteractableType()
    {
        return interactableType;
    }

    public float GetValue()
    {
        return Angle/maxAngle;
    }

    public void Close(){
        Angle = maxAngle;
    }

    public void Open(){
        Angle = 0f;
    }

    public void MoveHand()
    {
        Vector2 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = spriteCamera.ScreenToWorldPoint(mouseScreenPosition);

        Vector3 centerToMouse = mouseWorldPosition - transform.position;
        Vector3 directionToHand = centerToMouse.normalized;
        Vector3 centerToCircumference = directionToHand * radius;
        Vector3 finalPosition = transform.position + centerToCircumference;
        
        handTransform.position = new Vector3(finalPosition.x,finalPosition.y,handTransform.position.z);
    }


    public void Update()
    {
        if(!isSelected) return;
        TurnWheel();
        MoveHand();
    }

    private void TurnWheel()
    {
        Vector3 wheelPositionInScreenSpace = spriteCamera.WorldToScreenPoint(transform.position);
        Vector3 handPositionInScreenSpace = spriteCamera.WorldToScreenPoint(handTransform.position);

        float newAngle = Vector2.Angle(Vector2.up,handPositionInScreenSpace - wheelPositionInScreenSpace);

        if(handPositionInScreenSpace.x < wheelPositionInScreenSpace.x){
            Angle += (newAngle - prevAngle);
        }else{
            Angle -= (newAngle - prevAngle);
            
        }

        Angle = Mathf.Clamp(Angle, 0,maxAngle);

        prevAngle = newAngle;

        transform.localEulerAngles = Vector3.back * Angle;
    }
}
