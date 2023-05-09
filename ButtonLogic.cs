using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    public void LoadGame(){
        EventManager.TriggerEvent("LoadGame",null);
    }

    public void LoadMainMenu(){
        EventManager.TriggerEvent("LoadMainMenu",null);
    }

    public void Quit(){
        EventManager.TriggerEvent("Quit",null);
    }
}
