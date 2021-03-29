using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Guest : MonoBehaviour
{
    [SerializeField] private Image imageFilling;
    [SerializeField] private Image[] arrayImageItems;

    public event Action<Guest> _onUpdateGuest;
    private Vector3 _targetPosition;
    private Vector3 _startPoint;
    private float _timeExpectation;
    private List<Items> _listItems;
    private bool _isStartCoroutine;
    private Sequence _sequence;
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Tray") || !_isStartCoroutine)
            return;

        var tempTray = other.GetComponent<Tray>();
        var tempItemsTray = tempTray.GetItems();
        
        if(tempItemsTray.Count != _listItems.Count)
            return;
        
        for (var i = 0; i < _listItems.Count; i++)
        {
            if (tempItemsTray.Contains(_listItems[i]))
            {
                tempItemsTray.Remove(_listItems[i]);
                continue;
            }
            
            return;
        }
        
        StopAllCoroutines();
        tempTray.DestroyTray();
        MoveTarget(_startPoint);
        GameManager.instance.UiManager.UpdateScoreText(1);
    }

    public void InitializedBubble(Dictionary<Sprite, List<Items>> dictionarySpriteItems)
    {
        if (_sequence == null)
            _sequence = DOTween.Sequence();
        
        if(_listItems == null)
            _listItems = new List<Items>();
        
        for (var i = 0; i < arrayImageItems.Length; i++)
        {
            arrayImageItems[i].gameObject.SetActive(false);
        }
        
        _listItems.Clear();
        int indexItems = 0;
        
        for (var i = 0; i < dictionarySpriteItems.Count; i++)
        {
            var element = dictionarySpriteItems.ElementAt(i);

            for (var j = 0; j < element.Value.Count; j++)
            {
                arrayImageItems[indexItems].gameObject.SetActive(true);
                arrayImageItems[indexItems].sprite = element.Key;
                _listItems.Add(element.Value[j]);
                indexItems++;
            }
        }
    }

    public void RestartGame()
    {
        StopAllCoroutines();
        transform.position = _startPoint;
        MoveTarget(_targetPosition);
    }

    public void InitializedTime(float time)
    {
        _timeExpectation = time;
        imageFilling.fillAmount = 0;
        _isStartCoroutine = false;
    }

    public void MoveTarget(Vector3 target)
    {
        if (_startPoint == Vector3.zero)
        {
            _targetPosition = target;
            _startPoint = transform.position;
        }

        StartCoroutine(MoveToTarget(target));
    }

    private IEnumerator MoveToTarget(Vector3 target)
    {
        float time = 0;
        var tempStartPosition = transform.position;
        
        while (time < 6.3f)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(tempStartPosition, target,  time / 6.3f);
            yield return null;
        }
        
        if (target == _startPoint)
        {
            _onUpdateGuest?.Invoke(this);
            MoveTarget(_targetPosition);
            yield break;
        }
                    
        StartCoroutine(StartTime());
    }

    private IEnumerator StartTime()
    {
        _isStartCoroutine = true;
        float time = 0;
        
        while (time < _timeExpectation)
        {
            time += Time.deltaTime;
            imageFilling.fillAmount = time / _timeExpectation;
            yield return null;
        }
        
        GameManager.instance.UiManager.UpdateScoreText(-1);
        MoveTarget(_startPoint);
    }
}
