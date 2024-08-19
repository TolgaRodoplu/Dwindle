using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSystem : MonoBehaviour
{
    public static EventSystem instance;

    public event EventHandler<string> soundTriggered, soundStopped;
    public event EventHandler<int> puzzleTriggered;
    public event EventHandler<bool> rotateObj, activateCross, pressEtoGrab, pressEtoRelease, holdRtoRotate;
    public event EventHandler<int[]> animationTriggered;
    public event Action gameStarted, playStarted, passedPoint;
    void Awake()
    {

        if (instance == null)
            instance = this;

    }

    private void Start()
    {
        Debug.Log("sa");
    }



    //public void StartPlay()
    //{
    //    FindObjectOfType<AudioManeger>().Play("DungeonBackground");
    //    playStarted?.Invoke();
    //}

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
}