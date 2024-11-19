using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlMediator
{
    private LvlFactory _lvlFactory;
    private PresenterFactory _presenterFactory;

    public LvlMediator(LvlFactory lvlFactory, PresenterFactory presenterFactory)
    {
        _lvlFactory = lvlFactory;
        _presenterFactory = presenterFactory;        
    }

    public void StartRating() => MainMenu();

    private void MainMenu()
    {
        MainMenuView mainMenuView = _lvlFactory.Get(TypeScene.MainMenu).GetComponent<MainMenuView>();
        MainMenuPresenter menuPresenter = (MainMenuPresenter)_presenterFactory.Get(TypeScene.MainMenu);

        menuPresenter.GoToTotalRate += ShowTotalRate;
        menuPresenter.GoToGooseRate += ShowGooseRate;
        menuPresenter.GoToProperty += ShowProperty;

        menuPresenter.Initialize(mainMenuView);
    }

    private void ShowGooseRate()
    {
        PlayerRatingView view = _lvlFactory.Get(TypeScene.Player).GetComponent<PlayerRatingView>();
        PlayerRatingPresenter presenter = (PlayerRatingPresenter)_presenterFactory.Get(TypeScene.Player);
        
        presenter.Exit += MainMenu;
        presenter.Initialize(view);
    }

    private void ShowTotalRate()
    {
        TotalRatingView view = _lvlFactory.Get( TypeScene.TotalRaitePlayers).GetComponent<TotalRatingView>();
        TotalRatingPresenter presenter = (TotalRatingPresenter)_presenterFactory.Get(TypeScene.TotalRaitePlayers);

        presenter.Exit += MainMenu;
        presenter.Initialize(view);
    }

    private void ShowProperty()
    {
        PropertyView propertyView = _lvlFactory.Get(TypeScene.Property).GetComponent<PropertyView>();
        PropertyPresenter propertyPresenter = (PropertyPresenter)_presenterFactory.Get(TypeScene.Property);

        propertyPresenter.Exit += MainMenu;
        propertyPresenter.Initialize(propertyView);
    }
}
