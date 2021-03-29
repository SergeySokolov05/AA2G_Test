using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Tray : MonoBehaviour, IInitializationManager
{
    [SerializeField] private Material materialNormal;
    [SerializeField] private Material materialFill;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Transform[] arrayPositionGameObject;

    private GameManager _gameManager;
    private Dictionary<Transform, IndicatorItems> _itemses;
    private Rigidbody _rigidbody;
    private Vector3 _startPosition;
    private Quaternion _startQuaternion;
    private bool _isStartMove;
    private bool _isFilling;

    public GameManager GameManager
    {
        set => _gameManager = value;
    }
    
    public bool IsStartMove => _isStartMove;

    public bool IsFilling
    {
        get
        {
            return _isFilling;
        }
        set
        {
            _isFilling = value;
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            GameManager.instance.UiManager.UpdateScoreText(-1);
            DestroyTray();
        }

        _isStartMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Conveyor"))
            return;
        
        meshRenderer.material = materialFill;
        _isFilling = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("Conveyor"))
            return;
        
        meshRenderer.material = materialNormal;
        _isFilling = false;
    }
    
    public void Initialization()
    {
        _itemses = new Dictionary<Transform, IndicatorItems>();
        
        for (var i = 0; i < arrayPositionGameObject.Length; i++)
        {
            _itemses.Add(arrayPositionGameObject[i], null);
        }
        
        _rigidbody = GetComponent<Rigidbody>();
        _startPosition = transform.position;
        _startQuaternion = transform.rotation;
    }

    public void SetItem(Items items)
    {
        if(!_itemses.ContainsValue(null))
         return;

        var tempItem = _gameManager.GetGameObjectItem(items);
        
        for (var i = 0; i < arrayPositionGameObject.Length; i++)
        {
            if (_itemses[arrayPositionGameObject[i]] == null)
            {
                _itemses[arrayPositionGameObject[i]] = tempItem;
                tempItem.transform.SetParent(arrayPositionGameObject[i]);
                tempItem.transform.localPosition = Vector3.zero;
                
                var tempScale = tempItem.transform.localScale;
                tempItem.transform.localScale = Vector3.zero;
                tempItem.transform.DOScale(tempScale, 0.25f).SetEase(Ease.InBounce);
                break;
            }
        }
    }

    public List<Items> GetItems()
    {
        var tempListItems = new List<Items>();

        for (var i = 0; i < _itemses.Count; i++)
        {
            var valuePair = _itemses.ElementAt(i);
            if(valuePair.Value == null)
                continue;
            
            tempListItems.Add(valuePair.Value.ThisTypeItem);
        }

        return tempListItems;
    }

    public void DestroyTray()
    {
        gameObject.SetActive(false);
        transform.position = _startPosition;
        transform.rotation = _startQuaternion;
        _isStartMove = false;
        _rigidbody.Sleep();
            
        for (var i = 0; i < arrayPositionGameObject.Length; i++)
        {
            if (_itemses[arrayPositionGameObject[i]] != null)
            {
                Destroy(_itemses[arrayPositionGameObject[i]].gameObject);
                _itemses[arrayPositionGameObject[i]] = null;
            }
        }
    }


}
