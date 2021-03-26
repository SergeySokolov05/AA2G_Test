using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Guest : MonoBehaviour
{
    [SerializeField] private Image imageFilling;
    [SerializeField] private Image[] arrayImageItems;

    public void InitializedBubble()
    {
        
    }

    public void MoveTarget(Transform target)
    {
        transform.DOMove(target.position, 6.3f);
    }
}
