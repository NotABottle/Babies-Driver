using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void LoadGame(){
        EventManager.TriggerEvent("LoadGame",null);
    }

    public void Quit(){
        Application.Quit();
    }
}
