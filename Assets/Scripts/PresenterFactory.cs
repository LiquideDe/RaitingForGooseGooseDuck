using Zenject;
using System;

public class PresenterFactory
{
    private DiContainer _diContainer;

    public PresenterFactory(DiContainer diContainer) => _diContainer = diContainer;

    public IPresenter Get(TypeScene type)
    {
        switch (type)
        {
            case TypeScene.MainMenu:
                return _diContainer.Instantiate<MainMenuPresenter>();

            case TypeScene.Player:
                return _diContainer.Instantiate<PlayerRatingPresenter>();

            case TypeScene.TotalRaitePlayers:
                return _diContainer.Instantiate<TotalRatingPresenter>();

            default:
                throw new ArgumentException(nameof(type));
        }
    }
}
