using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Items
{
   circle = 0,
   cubeGreen = 1,
   cubeBlue = 2
}

public class IndicatorItems : MonoBehaviour
{
   [SerializeField] private Items thisTypeItem;

   public Items ThisTypeItem => thisTypeItem;
}
