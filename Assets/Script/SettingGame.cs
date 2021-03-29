using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingGame", menuName = "Game/SettingGame")]
public class SettingGame : ScriptableObject
{
  [SerializeField] private float speedConveyor;
  [SerializeField] private int timeGameSeconds;

  public float SpeedConveyor => speedConveyor;
  public int TimeGameSeconds => timeGameSeconds;
}
