using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Tray : MonoBehaviour
{
    [SerializeField] private Material materialNormal;
    [SerializeField] private Material materialFill;
    [SerializeField] private MeshRenderer meshRenderer;

    private Rigidbody _rigidbody;
    private Vector3 _startPosition;
    private Quaternion _startQuaternion;
    private bool _isStartMove;

    public bool IsStartMove => _isStartMove;

    private void Start()
    {
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
        }

        _isStartMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        meshRenderer.material = materialFill;
    }

    private void OnTriggerExit(Collider other)
    {
        meshRenderer.material = materialNormal;
    }
}
