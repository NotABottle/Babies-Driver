using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnGearShift : MonoBehaviour, IOnInteract
{
    private InteractableType interactableType = InteractableType.Radial;

    private bool isSelected = false;
    private Vector2 previousMousePosition;

    private float maxAngle = 15f;
    private float prevAngle;
    private float prevFinal;
    private float Angle;
    private float finalAngle;
    private float radius;
    
    public Transform gearPivot;
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
        radius = Vector2.Distance(handTransform.position,gearPivot.position);
        prevAngle = Vector2.Angle(Vector2.up, spriteCamera.WorldToScreenPoint(handTransform.position) - spriteCamera.WorldToScreenPoint(gearPivot.position));
        prevFinal = maxAngle;
    }

    public float GetValue(){
        return finalAngle/maxAngle;
    }

    private void Awake(){
        gearPivot.localEulerAngles = Vector3.back * maxAngle;
    }

    public void Update()
    {

        if(!isSelected) return;

        TurnWheel();
        MoveHand();
    }

    public void MoveHand()
    {

        Vector2 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = spriteCamera.ScreenToWorldPoint(mouseScreenPosition);

        Vector3 centerToMouse = mouseWorldPosition - gearPivot.position;
        Vector3 directionToHand = centerToMouse.normalized;
        Vector3 centerToCircumference = directionToHand * radius;
        Vector3 finalPosition = gearPivot.position + centerToCircumference;
        
        handTransform.position = new Vector3(finalPosition.x,finalPosition.y,handTransform.position.z);
    }

    private void TurnWheel()
    {
        Vector3 wheelPositionInScreenSpace = spriteCamera.WorldToScreenPoint(gearPivot.position);
        Vector3 handPositionInScreenSpace = spriteCamera.WorldToScreenPoint(handTransform.position);

        float newAngle = Vector2.Angle(Vector2.up,handPositionInScreenSpace - wheelPositionInScreenSpace);

        if(handPositionInScreenSpace.x > wheelPositionInScreenSpace.x){
            Angle += (newAngle - prevAngle);
        }else{
            Angle -= (newAngle - prevAngle);
        }

        Angle = Mathf.Clamp(Angle, -maxAngle,maxAngle);

        finalAngle = Angle;
        if(Angle < 0){
            finalAngle = -maxAngle;
        }else if(Angle > 0){
            finalAngle = +maxAngle;
        }else{
            finalAngle = prevFinal;
        }

        prevAngle = newAngle;
        prevFinal = finalAngle;

        gearPivot.localEulerAngles = Vector3.back * finalAngle;
    }
}
