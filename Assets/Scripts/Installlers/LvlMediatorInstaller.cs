using UnityEngine;
using Zenject;

public class LvlMediatorInstaller : MonoInstaller
{
    [SerializeField] private PrefabHolder _prefabHolder;
    [SerializeField] private PlayersHolder _playersHolder;
    public override void InstallBindings()
    {
        Container.Bind<PrefabHolder>().FromInstance(_prefabHolder).AsSingle();
        Container.Bind<PlayersHolder>().FromInstance(_playersHolder).AsSingle();
        Container.Bind<LvlFactory>().AsSingle();
        Container.Bind<PresenterFactory>().AsSingle();
        Container.Bind<LvlMediator>().AsSingle();
    }
}
