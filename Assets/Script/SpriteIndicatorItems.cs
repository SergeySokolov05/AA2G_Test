using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteIndicatorItems", menuName = "Game/SpriteIndicatorItems")]
public class SpriteIndicatorItems : ScriptableObject
{
   [SerializeField] private Sprite sprite;
   [SerializeField] private Items items;
   
   public Sprite Sprite => sprite;
   public Items Items => items;
}
