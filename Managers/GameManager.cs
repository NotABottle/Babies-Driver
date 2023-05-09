using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState currentState;

    private void OnEnable(){
        EventManager.StartListening("WinCondition",Win);
        EventManager.StartListening("LoseCondition", Lose);
        EventManager.StartListening("LoadGame",LoadGame);
        EventManager.StartListening("LoadMainMenu",LoadMainMenu);
        EventManager.StartListening(eventName: "Quit",Quit);

    }

    private void OnDisable(){
        EventManager.StopListening("WinCondition",Win);
        EventManager.StopListening("LoseCondition", Lose);
    }

    private void Win(Dictionary<string, object> obj)
    {
        SwitchState(GameState.Win);
    }

    private void Lose(Dictionary<string, object> obj)
    {
        SwitchState(GameState.Lose);
    }

    private void Awake(){
        if(instance != null && instance != this){
            Destroy(this);
        }else{
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void SwitchState(GameState newState){
        switch(newState){
            case GameState.DEFAULT:
                HandleDefaultState();
                break;
            case GameState.MainMenu:
                HandleMainMenuState();
                break;
            case GameState.Paused:
                HandleMenuState();
                break;
            case GameState.Starting:
                HandleStartingState();
                break;
            case GameState.Playing:
                HandlePlayingState();
                break;
            case GameState.Win:
                HandleWinState();
                break;
            case GameState.Lose:
                HandleLoseState();
                break;
        }
    }

    /*
    Handler Methods
    */

    private void HandleStartingState()
    {
        currentState = GameState.Starting;
    }

    private void HandleMainMenuState()
    {
        currentState = GameState.MainMenu;
    }

    private void HandleLoseState()
    {
        currentState = GameState.Lose;
        Time.timeScale = 0f;
        Debug.Log("You Lost");
        EventManager.TriggerEvent(eventName: "Lose",null);
    }

    private void HandleWinState()
    {
        currentState = GameState.Win;
        Time.timeScale = 0f;
        Debug.Log("You Win");
        EventManager.TriggerEvent("Win",null);
    }

    private void HandleMenuState()
    {
        currentState = GameState.Paused;
    }

    private void HandlePlayingState()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;
    }

    private void HandleDefaultState()
    {
        currentState = GameState.DEFAULT;
    }

    public void LoadGame(Dictionary<string, object> obj){
        SceneManager.LoadScene("World");
        SwitchState(GameState.Playing);
    }

    public void LoadMainMenu(Dictionary<string, object> obj){
        SceneManager.LoadScene("MainMenu");
        SwitchState(GameState.MainMenu);
    }

    public void Quit(Dictionary<string, object> obj){
        Application.Quit();
    }
}


public enum GameState{
    DEFAULT,
    MainMenu,
    Starting,
    Paused,
    Playing,
    Win,
    Lose
}