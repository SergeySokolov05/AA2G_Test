using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button buttCircle;
    [SerializeField] private Button buttCubeGreen;
    [SerializeField] private Button buttCubeBlue;
    
    [Inject] public Conveyor Conveyor { get; set;}

    private void Start()
    {
        buttCircle.onClick.AddListener(ButtonItemCircle);
        buttCubeGreen.onClick.AddListener(ButtonItemCubeGreen);
        buttCubeBlue.onClick.AddListener(ButtonItemCubeBlue);
    }

    //Unity TriggerEventScene
    public void ButtonPush()
    {
        Conveyor.PushTray();
    }
    
    //Unity TriggerEventScene
    public void ButtonStopPush()
    {
        Conveyor.StopPushTray();
    }

    private void ButtonItemCircle()
    {
        Conveyor.FillingObjectTray(Items.circle);
    }
    
    private void ButtonItemCubeGreen()
    {
        Conveyor.FillingObjectTray(Items.cubeGreen);
    }
    
    private void ButtonItemCubeBlue()
    {
        Conveyor.FillingObjectTray(Items.cubeBlue);
    }
}
