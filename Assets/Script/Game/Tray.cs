using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Tray : MonoBehaviour
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
    public bool IsFilling => _isFilling;

    private void Start()
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
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

    public void SetItem(Items items)
    {
        if(!_itemses.ContainsValue(null))
         return;

        var tempItem = _gameManager.SetGameObjectItems(items);

        for (var i = 0; i < arrayPositionGameObject.Length; i++)
        {
            if (_itemses[arrayPositionGameObject[i]] == null)
            {
                _itemses[arrayPositionGameObject[i]] = tempItem;
                tempItem.transform.SetParent(arrayPositionGameObject[i]);
                tempItem.transform.localPosition = Vector3.zero;
                break;
            }
        }
    }
}
