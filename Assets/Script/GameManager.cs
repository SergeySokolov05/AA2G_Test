using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private UIManager uiManager;
    [SerializeField] private GuestManager guestManager;
    
    private Conveyor _conveyor;
    private SettingGame _settingGame;
    private PrefabsGame _prefabsGame;

    public UIManager UiManager => uiManager;
    public Conveyor Conveyor => _conveyor;
    public SettingGame SettingGame => _settingGame;
    public PrefabsGame PrefabsGame => _prefabsGame;

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        
        instance = this;
        
        _settingGame = Resources.Load<SettingGame>("SettingGame");
        _prefabsGame = Resources.Load<PrefabsGame>("PrefabsGame");
        
        _conveyor = FindObjectOfType<Conveyor>();

    }

    public void StartGame()
    {
        uiManager.Initialization();
        _conveyor.Initialization();
        guestManager.Initialization();
    }
    
    public void RestartGame()
    {
        _conveyor.RestartGame();
        guestManager.RestartGame();
    }

    public IndicatorItems GetGameObjectItem(Items items)
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

    public List<SpriteIndicatorItems> GetSpritesItems()
    {
        return Resources.LoadAll<SpriteIndicatorItems>("Sprite_Items").ToList();
    }
}
