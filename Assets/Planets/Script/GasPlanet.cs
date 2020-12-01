using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPlanet : Planet
{
    public float GasRefillRatePerSeconds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_characterOnPlanet)
        {
            _player.GetComponentInChildren<FuelController>().AddFuel(GasRefillRatePerSeconds * Time.deltaTime);
        }
    }
}
