using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    protected bool _characterOnPlanet = false;
    protected GameObject _player;

    public void LandOnPlanet(GameObject Player)
    {
        _characterOnPlanet = true;
        _player = Player;
    }

    public void LeavePlanet()
    {
        _characterOnPlanet = false;
        _player = null;
    }
}
