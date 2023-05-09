using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /*
    Use Case for triggering an event with/without parameter
    EventManager.TriggerEvent("eventName",null); TO TRIGGER AN EVENT WITHOUT DATA
    EventManager.TriggerEvent("playSound",new Dictionary<string,object> {{"soundName","soundEffectName"}}); TO PASS DATA THROUGH AN EVENT
    */

    public static AudioManager instance;

    public Sound[] Sounds;

    private void OnEnable() => EventManager.StartListening("playSound",Play);

    private void OnDisable() => EventManager.StopListening("playSound",Play);

    void Awake(){
        if(instance != null && instance != this){
            Destroy(this);
        }else{
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        InitializeAudioSources();        
    }

    private void InitializeAudioSources(){
        foreach(Sound s in Sounds){
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            s.audioSource.volume = s.volume;
        }
    }

    private void Play(Dictionary<string, object> message){
        var soundToPlay = (string) message["soundName"];
        foreach(Sound s in Sounds){
            if(s.name.Equals(soundToPlay)){
                s.audioSource.Play();
            }
        }
    }

}
