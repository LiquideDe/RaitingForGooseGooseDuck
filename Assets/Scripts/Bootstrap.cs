using System.Collections;
using System.Collections.Generic;
using System.EnterpriseServices;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;


public class Bootstrap : MonoBehaviour
{
    [SerializeField] private PlayersHolder _playersHolder;
    private LvlMediator _mediator;
    

    [Inject]
    private void Construct(LvlMediator lvlMediator) => _mediator = lvlMediator;
    // Start is called before the first frame update
    void Start()
    {        
        foreach (var player in _playersHolder.Players)
            player.IsPlayerShowing = true;
        _mediator.StartRating();
    }

    

}
