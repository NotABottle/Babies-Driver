using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildStateHandler : MonoBehaviour
{
    /*
    This handler should handle the various behaviours of each child state
    and transition the child from state to state after a set amount of time
    has passed
    */

    public TurnWindowCrank windowCrank;
    public TapZombie tapZombie;

    public ChildState childState;

    private float timeOfLastStateChange;
    private float minTimeBetweenStateChanges = 6f;
    private float maxTimeBetweenStateChanges = 10f;

    private SpriteRenderer childSpriteRenderer;

    private PullLookingChild lookingChild;
    private PullHangingChild hangingChild;
    private PullGrappledChild grappledChild;

    private List<MonoBehaviour> interactionBehaviors;

    public Sprite sittingSprite;
    public Sprite lookingOutSprite;
    public Sprite hangingOutSprite;
    public Sprite grappledSprite;
    public Sprite goneSprite;
    private Vector3 neutralPosition;

    private void Awake(){
        childSpriteRenderer = GetComponent<SpriteRenderer>();
        lookingChild = GetComponent<PullLookingChild>();
        hangingChild = GetComponent<PullHangingChild>();
        grappledChild = GetComponent<PullGrappledChild>();
        interactionBehaviors = new List<MonoBehaviour>();
        interactionBehaviors.Add(lookingChild);
        interactionBehaviors.Add(hangingChild);
        interactionBehaviors.Add(grappledChild);
        neutralPosition = transform.position;
    }

    private void Start(){
        SwitchState(ChildState.Sitting);
    }

    private void Update(){
        SittingStateBehavior();
        LookingOutStateBehavior();
        HangingOutStateBehavior();
        GrappledStateBehavior();
    }

    private void DisableAllInteractions(){
        foreach(MonoBehaviour behavior in interactionBehaviors){
            behavior.enabled = false;
        }
    }

    private void DisableZombie(){
        tapZombie.enabled = false;
        tapZombie.gameObject.SetActive(false);
    }

    private void ResetChildPosition()
    {
        transform.position = neutralPosition;
    }

//-------------------------------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------------------

    private void SittingStateBehavior()
    {
        //If not this state
        if(childState != ChildState.Sitting) return;

        var timeSinceLastStateChange = Time.timeSinceLevelLoad - timeOfLastStateChange;
        var timeBetweenStateChanges = UnityEngine.Random.Range(minTimeBetweenStateChanges,maxTimeBetweenStateChanges);

        //If not time to change state
        if(timeSinceLastStateChange < timeBetweenStateChanges) return;

        //If time to change state
        timeOfLastStateChange = Time.timeSinceLevelLoad;
        SwitchState(ChildState.LookingOut);
    }

    private void LookingOutStateBehavior()
    {
        //If not this state
        if(childState != ChildState.LookingOut) return;

        var timeSinceLastStateChange = Time.timeSinceLevelLoad - timeOfLastStateChange;
        var timeBetweenStateChanges = UnityEngine.Random.Range(minTimeBetweenStateChanges,maxTimeBetweenStateChanges);

        //If task completed, switch to correct state and reset timeOfLastChange
        var taskValue = lookingChild.GetValue();
        if(Mathf.Approximately(1,taskValue)){
            SwitchState(ChildState.Sitting);
            timeOfLastStateChange = Time.timeSinceLevelLoad;
        }

        //If not time to change state
        if(timeSinceLastStateChange < timeBetweenStateChanges) return;

        //If time to change state
        timeOfLastStateChange = Time.timeSinceLevelLoad;
        SwitchState(ChildState.HangingOut);
    }

    private void HangingOutStateBehavior()
    {
        //If not this state
        if(childState != ChildState.HangingOut) return;

        var timeSinceLastStateChange = Time.timeSinceLevelLoad - timeOfLastStateChange;
        var timeBetweenStateChanges = UnityEngine.Random.Range(minTimeBetweenStateChanges,maxTimeBetweenStateChanges);

        //If task completed, switch to correct state and reset timeOfLastChange
        var taskValue = hangingChild.GetValue();
        if(Mathf.Approximately(1,taskValue)){
            SwitchState(ChildState.LookingOut);
            timeOfLastStateChange = Time.timeSinceLevelLoad;
        }

        //If not time to change state
        if(timeSinceLastStateChange < timeBetweenStateChanges) return;

        //If time to change state
        timeOfLastStateChange = Time.timeSinceLevelLoad;
        SwitchState(ChildState.Grappled);
    }

    private void GrappledStateBehavior()
    {
        //If not this state
        if(childState != ChildState.Grappled) return;

        var timeSinceLastStateChange = Time.timeSinceLevelLoad - timeOfLastStateChange;
        var timeBetweenStateChanges = UnityEngine.Random.Range(minTimeBetweenStateChanges,maxTimeBetweenStateChanges);

        //If task completed, switch to correct state and reset timeOfLastChange
        var taskValue = hangingChild.GetValue();
        var zombieTapped = tapZombie.GetValue();
        if(Mathf.Approximately(1,taskValue) && Mathf.Approximately(1,zombieTapped)){
            SwitchState(ChildState.Sitting);
            timeOfLastStateChange = Time.timeSinceLevelLoad;
        }

        //If not time to change state
        if(timeSinceLastStateChange < timeBetweenStateChanges) return;

        //If time to change state
        timeOfLastStateChange = Time.timeSinceLevelLoad;
        SwitchState(ChildState.Gone);
    }

//-------------------------------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------------------------------

    private void SwitchState(ChildState newState){
        DisableAllInteractions();
        DisableZombie();
        ResetChildPosition();
        switch(newState){
            case ChildState.Default:
                HandleDefaultState();
                break;
            case ChildState.Sitting:
                HandleSittingState();
                break;
            case ChildState.LookingOut:
                HandleLookingOutState();
                break;
            case ChildState.HangingOut:
                HandleHangingOutState();
                break;
            case ChildState.Grappled:
                HandleGrappledState();
                break;
            case ChildState.Gone:
                HandleGoneState();
                break;
        }
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------------------------------------------------------

    private void HandleDefaultState(){
        childState = ChildState.Default;
        Debug.LogError("ChildStateHandler - WHY AM I IN THE DEFAULT STATE");
    }

    private void HandleSittingState(){
        childState = ChildState.Sitting;
        
        //Set appropriate sprite
        childSpriteRenderer.sprite = sittingSprite;

        //Enable Crank Script
        windowCrank.enabled = true;

    }

    private void HandleLookingOutState()
    {
        childState = ChildState.LookingOut;

        // childSpriteRenderer.sprite = lookingOutSprite;

        //Open Window
        windowCrank.Open();

        //Change activated IOnInteract script
        lookingChild.enabled = true;

        //Disable CrankScript
        windowCrank.enabled = false;
    }

    private void HandleHangingOutState(){
        childState = ChildState.HangingOut;
        
        //Set appropriate sprite
        childSpriteRenderer.sprite = hangingOutSprite;

        //Change activated IOnInteract script
        hangingChild.enabled = true;

    }

    private void HandleGrappledState(){
        childState = ChildState.Grappled;
                
        //Set appropriate sprite
        childSpriteRenderer.sprite = grappledSprite;

        //Change activated IOnInteract script
        grappledChild.enabled = true;

        tapZombie.enabled = true;
        tapZombie.gameObject.SetActive(true);

    }

    private void HandleGoneState(){
        childState = ChildState.Gone;
                
        //Set appropriate sprite
        childSpriteRenderer.sprite = goneSprite;

        EventManager.TriggerEvent("LoseCondition",null);
        Debug.Log("Lose Condition Met");
    }

}

[System.Serializable]
public enum ChildState{
    Default,
    Sitting,
    LookingOut,
    HangingOut,
    Grappled,
    Gone
}
