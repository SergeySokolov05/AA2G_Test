using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Conveyor _conveyor;
    
    private void Awake()
    {
        _conveyor = FindObjectOfType<Conveyor>();
    }

    public void ButtonPush()
    {
        _conveyor.PushTray();
    }
    
    public void ButtonStopPush()
    {
        _conveyor.StopPushTray();
    }
}
