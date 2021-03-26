using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button buttCircle;
    [SerializeField] private Button buttCubeGreen;
    [SerializeField] private Button buttCubeBlue;

    private Conveyor _conveyor;

    private void Start()
    {
        _conveyor = GameManager.instance.Conveyor;
        buttCircle.onClick.AddListener(ButtonItemCircle);
        buttCubeGreen.onClick.AddListener(ButtonItemCubeGreen);
        buttCubeBlue.onClick.AddListener(ButtonItemCubeBlue);
    }

    //Unity TriggerEventScene
    public void ButtonPush()
    {
        _conveyor.PushTray();
    }
    
    //Unity TriggerEventScene
    public void ButtonStopPush()
    {
        _conveyor.StopPushTray();
    }

    private void ButtonItemCircle()
    {
        _conveyor.FillingObjectTray(Items.circle);
    }
    
    private void ButtonItemCubeGreen()
    {
        _conveyor.FillingObjectTray(Items.cubeGreen);
    }
    
    private void ButtonItemCubeBlue()
    {
        _conveyor.FillingObjectTray(Items.cubeBlue);
    }
}
