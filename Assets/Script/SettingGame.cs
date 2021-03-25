using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingGame", menuName = "Game/SettingGame")]
public class SettingGame : ScriptableObject
{
  [SerializeField] private float speedConveyor;

  public float SpeedConveyor => speedConveyor;
}
