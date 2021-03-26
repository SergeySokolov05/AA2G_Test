using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    private float _speed;
    private List<Tray> _listTray;
    private bool _isPush;

    private void Start()
    {
        _listTray = new List<Tray>();
        _speed = GameManager.instance.SettingGame.SpeedConveyor;
        PoolTray();
    }

    private void Update()
    {
        if(!_isPush)
            return;
        
        for (var i = 0; i < _listTray.Count; i++)
        {
            if(_listTray[i].gameObject.activeSelf && _listTray[i].IsStartMove)
                _listTray[i].transform.position += Vector3.right * (Time.deltaTime * _speed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        for (var i = 0; i < _listTray.Count; i++)
        {
            if (!_listTray[i].gameObject.activeSelf )
            {
                _listTray[i].gameObject.SetActive(true);
                break;
            }
        }
    }

    public void PushTray()
    {
        _isPush = true;
    }

    public void StopPushTray()
    {
        _isPush = false;
    }

    public void FillingObjectTray(Items items)
    {
        var tempTray = _listTray.First(tray => tray.IsFilling);
        tempTray.SetItem(items);
    }

    private void PoolTray()
    {
        Transform parentTray = transform.GetChild(0);
        int countTray = 10;
        var tempPrefabs = GameManager.instance.PrefabsGame.PrefabsTray;
        
        while (countTray > 0)
        {
            countTray--;
            
            _listTray.Add(Instantiate(tempPrefabs,
                new Vector3(parentTray.position.x, parentTray.position.y + parentTray.localScale.y, parentTray.position.z), 
                Quaternion.identity,
                parentTray));

            _listTray[_listTray.Count - 1].GameManager = GameManager.instance;
            
            if(countTray != 0)
              _listTray[_listTray.Count - 1].gameObject.SetActive(false);
        }
    }
}
