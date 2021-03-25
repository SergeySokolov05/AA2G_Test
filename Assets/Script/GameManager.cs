using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SettingGame _settingGame;
    private PrefabsGame _prefabsGame;

    public SettingGame SettingGame => _settingGame;
    public PrefabsGame PrefabsGame => _prefabsGame;

    private void Awake()
    {
        _settingGame = Resources.Load<SettingGame>("SettingGame");
        _prefabsGame = Resources.Load<PrefabsGame>("PrefabsGame");
    }

    public IndicatorItems SetGameObjectItems(Items items)
    {
        IndicatorItems tempGameObject;
        
        switch (items)
        {
            case Items.circle:
                tempGameObject = _prefabsGame.Circle;
                break;
            case Items.cubeGreen:
                tempGameObject = _prefabsGame.CubeGreen;
                break;
            case Items.cubeBlue:
                tempGameObject = _prefabsGame.CubeBlue;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(items), items, null);
        }

        return Instantiate(tempGameObject);
    }
}
