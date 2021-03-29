using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IInitializationManager
{
    [SerializeField] private Button buttCircle;
    [SerializeField] private Button buttCubeGreen;
    [SerializeField] private Button buttCubeBlue;
    [SerializeField] private Button buttOk;
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private TextMeshProUGUI textGameOverScore;
    
    private int _timeGame;
    private int _countScore;
    private Conveyor _conveyor;

    public void Initialization()
    {
        GameManager gameManager = GameManager.instance;
        _conveyor = gameManager.Conveyor;
        _timeGame = gameManager.SettingGame.TimeGameSeconds;
        
        buttCircle.onClick.AddListener(ButtonItemCircle);
        buttCubeGreen.onClick.AddListener(ButtonItemCubeGreen);
        buttCubeBlue.onClick.AddListener(ButtonItemCubeBlue);
        
        UpdateScoreText(0);
        StartCoroutine(Timer());
    }
    

    public void UpdateScoreText(int value)
    {
        _countScore += value;
        textScore.text = "Score: " + _countScore;
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

    public void StartGame(GameObject gameObject)
    {
        buttCircle.transform.DOScale(Vector3.one, 0.3f);
        buttCubeGreen.transform.DOScale(Vector3.one, 0.3f);
        buttCubeBlue.transform.DOScale(Vector3.one, 0.3f);
        buttOk.transform.DOScale(Vector3.one, 0.3f);
        textScore.transform.DOScale(Vector3.one, 0.3f);
        textTime.transform.DOScale(Vector3.one, 0.3f);
        GameManager.instance.StartGame();
        gameObject.transform.DOScale(Vector3.zero, 0.3f);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        GameManager.instance.RestartGame();
        panelGameOver.gameObject.SetActive(false);
        _timeGame = GameManager.instance.SettingGame.TimeGameSeconds;
        _countScore = 0;
        UpdateScoreText(0);
        StartCoroutine(Timer());
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

    private void OpenPanelGameOver()
    {
        panelGameOver.SetActive(true);
        textGameOverScore.text = "Score: " + _countScore;
    }
    
    private void UpdateTextTime(float time)
    {
        var tempMinute = Mathf.FloorToInt(time / 60);
        var tempSeconds = Mathf.FloorToInt(time % 60);
        textTime.text = "Time: " + string.Format("{0:00}:{1:00}", tempMinute, tempSeconds);
    }

    private IEnumerator Timer()
    {
        while (_timeGame > 0)
        {
            UpdateTextTime(_timeGame);
            yield return new WaitForSeconds(1);
            _timeGame--;
        }

        OpenPanelGameOver();
        Time.timeScale = 0;
    }
}
