using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCursor : MonoBehaviour
{
    private SpriteRenderer handSpriteRenderer;
    [SerializeField]
    public Sprite openHandSprite;
    [SerializeField]
    public Sprite closedHandSprite;

    public Camera spriteCamera;

    public bool IsHandInUse = false;

    public float frontZ = 1f;
    public float backZ = -1.04f;
    public float maxHeight;

    private void OnEnable(){
        EventManager.StartListening("LMBUp",OpenHand);
        EventManager.StartListening("LMBDown",CloseHand);
        EventManager.StartListening("OnSelectInteractable", LockHand);
        EventManager.StartListening("OnDeselectInteractable",UnlockHand);
        EventManager.StartListening("RMBDown", SwapSides);
    }

    private void OnDisable(){
        EventManager.StopListening("LMBUp",OpenHand);
        EventManager.StopListening("LMBDown",CloseHand);
        EventManager.StopListening("OnSelectInteractable", LockHand);
        EventManager.StopListening("OnDeselectInteractable",UnlockHand);
        EventManager.StopListening("RMBDown", SwapSides);
    }

    private void SwapSides(Dictionary<string, object> obj)
    {
        //if in front move to back and flip sprite
        if(transform.position.z > 0f){
            transform.position = new Vector3(transform.position.x,transform.position.y,backZ);
            handSpriteRenderer.flipX = true;
        }
        //if in back move to front and flip sprite
        else if(transform.position.z < 0f){
            transform.position = new Vector3(transform.position.x,transform.position.y,frontZ);
            handSpriteRenderer.flipX = false;
        }
    }


    private void Awake(){
        handSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update(){
        //Move hand to follow cursor if the hand is not in use

        if(IsHandInUse) return;

        Vector2 mouseScreenPosition = Input.mousePosition;
        Vector2 mouseWorldPosition = spriteCamera.ScreenToWorldPoint(mouseScreenPosition);

        transform.position = new Vector3(mouseWorldPosition.x,mouseWorldPosition.y,transform.position.z);
    }

    private void CloseHand(Dictionary<string, object> obj)
    {
        handSpriteRenderer.sprite = closedHandSprite;
    }

    private void OpenHand(Dictionary<string, object> obj)
    {
        handSpriteRenderer.sprite = openHandSprite;
        IsHandInUse = false;
    }

    private void UnlockHand(Dictionary<string, object> obj)
    {
        IsHandInUse = false;
    }

    private void LockHand(Dictionary<string, object> obj)
    {
        IsHandInUse = true;
    }

}

[System.Serializable]
public enum InteractableType{
    Default,
    Vertical,
    Horizontal,
    Radial
}
