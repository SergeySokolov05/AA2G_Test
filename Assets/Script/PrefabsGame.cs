using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabsGame", menuName = "Game/PrefabsGame")]
public class PrefabsGame : ScriptableObject
{
    [SerializeField] private Tray prefabsTray;
    [SerializeField] private IndicatorItems circle;
    [SerializeField] private IndicatorItems cubeGreen;
    [SerializeField] private IndicatorItems cubeBlue;
    [SerializeField] private Guest guest;
    
    public Tray PrefabsTray => prefabsTray;
    public IndicatorItems Circle => circle;
    public IndicatorItems CubeGreen => cubeGreen;
    public IndicatorItems CubeBlue => cubeBlue;
    public Guest Guest => guest;
}
