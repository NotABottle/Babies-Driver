using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSteeringWheel : MonoBehaviour, IOnInteract
{
    private InteractableType interactableType = InteractableType.Radial;

    private bool isSelected = false;
    private Vector2 previousMousePosition;

    private float maxAngle = 360f;
    private float prevAngle;
    private float Angle;

    [Range(0,100f)]
    public float returnToNeutralSpeed = 100f;

    private float radius;
    
    public Transform handTransform;
    public Camera spriteCamera;


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
        radius = Vector2.Distance(handTransform.position,transform.position);
        prevAngle = Vector2.Angle(Vector2.up, spriteCamera.WorldToScreenPoint(handTransform.position) - spriteCamera.WorldToScreenPoint(transform.position));
    }

    public float GetValue(){
        return Angle/maxAngle;
    }

    public void Update()
    {

        if(!isSelected && !Mathf.Approximately(0f,Angle)){
            float deltaAngle = returnToNeutralSpeed * Time.deltaTime;
            if(Mathf.Abs(deltaAngle) > Mathf.Abs(Angle)){
                Angle = 0f;
            }            
            else if(Angle > 0f){
                Angle -= deltaAngle;
            }
            else{
                Angle += deltaAngle;
            }
            
            transform.localEulerAngles = Vector3.back * Angle;
            return;
        }else if(!isSelected) return;

        TurnWheel();
        MoveHand();
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

    private void TurnWheel()
    {
        Vector3 wheelPositionInScreenSpace = spriteCamera.WorldToScreenPoint(transform.position);
        Vector3 handPositionInScreenSpace = spriteCamera.WorldToScreenPoint(handTransform.position);

        float newAngle = Vector2.Angle(Vector2.up,handPositionInScreenSpace - wheelPositionInScreenSpace);

        if(handPositionInScreenSpace.x > wheelPositionInScreenSpace.x){
            Angle += (newAngle - prevAngle);
        }else{
            Angle -= (newAngle - prevAngle);
            
        }

        Angle = Mathf.Clamp(Angle, -maxAngle,maxAngle);

        prevAngle = newAngle;

        transform.localEulerAngles = Vector3.back * Angle;
    }
}
