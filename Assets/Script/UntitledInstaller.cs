using UnityEngine;
using Zenject;

public class UntitledInstaller : MonoInstaller
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Conveyor conveyor;
    [SerializeField] private UIManager uiManager;

    public override void InstallBindings()
    {
        Container.BindInstance(_gameManager).AsSingle();
        Container.BindInstance(conveyor).AsSingle();
        Container.BindInstance(uiManager);
    }
}