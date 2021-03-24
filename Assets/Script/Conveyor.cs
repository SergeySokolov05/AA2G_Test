using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    [SerializeField] private Tray prefabsTray;
    [SerializeField] private float forge;
    
    private List<Tray> _listTray;
    private bool _isPush;
    
    private void Start()
    {
        _listTray = new List<Tray>();
        PoolTray();
    }

    private void Update()
    {
        if(!_isPush)
            return;
        
        for (var i = 0; i < _listTray.Count; i++)
        {
            if(_listTray[i].gameObject.activeSelf && _listTray[i].IsStartMove)
                _listTray[i].transform.position += Vector3.right * (Time.deltaTime * forge);
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

    private void PoolTray()
    {
        Transform parentTray = transform.GetChild(0);
        int countTray = 10;
        
        while (countTray > 0)
        {
            countTray--;
            
            _listTray.Add(Instantiate(prefabsTray,
                new Vector3(parentTray.position.x, parentTray.position.y + parentTray.localScale.y, parentTray.position.z), 
                Quaternion.identity,
                parentTray));
            
            if(countTray != 0)
              _listTray[_listTray.Count - 1].gameObject.SetActive(false);
        }
    }
}
