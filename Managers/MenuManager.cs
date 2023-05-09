using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject pauseScreen;

    private void OnEnable(){
        EventManager.StartListening("Win",ShowWinScreen);
        EventManager.StartListening("Lose",ShowLoseScreen);
    }

    private void OnDisable(){
        EventManager.StopListening("Win",ShowWinScreen);
        EventManager.StopListening("Lose",ShowLoseScreen);
    }

    private void ShowLoseScreen(Dictionary<string, object> obj)
    {
        OnlyShowScreen(loseScreen);
    }

    private void ShowWinScreen(Dictionary<string, object> obj)
    {
        OnlyShowScreen(winScreen);
    }

    private void DisableAllScreens(){
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    private void OnlyShowScreen(GameObject screen){
        DisableAllScreens();
        screen.SetActive(true);
    }
}
