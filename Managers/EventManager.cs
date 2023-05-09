using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, Action<Dictionary<string,object>>> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance{
        get{
            if(!eventManager){
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if(!eventManager){
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene");
                }else{
                    eventManager.Init();

                    DontDestroyOnLoad(eventManager);
                }
            }
            return eventManager;
        }
    }

    /*
    Creates the eventDictionary in the case where it doesn't exist yet
    */
    private void Init(){
        if(eventDictionary == null){
            eventDictionary = new Dictionary<string, Action<Dictionary<string, object>>>();
        }
    }

    /*
    Takes "eventName" and then searches for it in the dictionary and takes the "listener" and has it listen for the event to be fired
    */
    public static void StartListening(string eventName, Action<Dictionary<string, object>> listener){
        Action<Dictionary<string,object>> thisEvent;

        if(instance.eventDictionary.TryGetValue(eventName, out thisEvent)){
            thisEvent += listener;
            instance.eventDictionary[eventName] = thisEvent;
        }else{
            thisEvent += listener;
            instance.eventDictionary.Add(eventName,thisEvent);
        }
    }

    /*
    Takes "eventName" and then searches for it in the dictionary and takes the "listener" and stops it from listening to the event
    */
    public static void StopListening(string eventName, Action<Dictionary<string,object>> listener){
        if(eventManager == null) return;
        Action<Dictionary<string,object>> thisEvent;
        if(instance.eventDictionary.TryGetValue(eventName,out thisEvent)){
            thisEvent -= listener;
            instance.eventDictionary[eventName] = thisEvent;
        }
    }

    /*
    Triggers event of "eventName" and can pass along paramters in dictionary struct through the "message" parameter
    */
    public static void TriggerEvent(string eventName, Dictionary<string,object> message){
        Action<Dictionary<string,object>> thisEvent;
        if(instance.eventDictionary.TryGetValue(eventName, out thisEvent)){
            thisEvent.Invoke(message);
        }
    }
}
