using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class EventSystem : MonoBehaviour
{
    public static EventSystem instance;

    public event EventHandler<string> soundTriggered, soundStopped;
    public event EventHandler<int> puzzleTriggered;
    public event EventHandler<bool> rotateObj, activateCross, pressEtoGrab, pressEtoRelease, holdRtoRotate;
    public event EventHandler<int[]> animationTriggered;
    public event Action settingsOpened, gameEnded, playStarted, passedPoint;
    void Awake()
    {

        if (instance == null)
            instance = this;

    }

    private void Start()
    {
        
    }

    public void ButtonAction(ButtonType btnType)
    {
        if(btnType == ButtonType.Play) 
        {
            StartGame();
        }
        else if(btnType == ButtonType.Settings) 
        {
            OpenSettings();
        }
        else if (btnType == ButtonType.Exit)
        {
            ExitGame();
        }
    }

    public void GameEnded()
    {
        gameEnded?.Invoke();
    }

    public void StartPlay()
    {
        playStarted?.Invoke();
    }

    public void RotateObject(bool isRotating)
    {
        rotateObj?.Invoke(this, isRotating);
    }

    public void TriggerPuzzle(int puzzleID)
    {
        puzzleTriggered?.Invoke(this, puzzleID);
    }

    public void TriggerAnimation(int[] animationID)
    {
        animationTriggered?.Invoke(this, animationID);
    }

    public void SetCrossActive(bool active)
    {
        activateCross?.Invoke(this, active);
    }
    public void GrabUI(bool active)
    {
        pressEtoGrab?.Invoke(this, active);
    }
    public void ReleaseUI(bool active)
    {
        pressEtoRelease?.Invoke(this, active);
    }
    public void RotateUI(bool active)
    {
        holdRtoRotate?.Invoke(this, active);
    }

    public void CloseCloset()
    {
        passedPoint?.Invoke();
    }
    public void GoToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        settingsOpened?.Invoke();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}