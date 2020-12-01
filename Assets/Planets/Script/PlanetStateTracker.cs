using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetStateTracker : MonoBehaviour
{
    public GameObject GameController;
    public int planetAlive = 4;

    private GameController _gameController;

    // Start is called before the first frame update
    void Start()
    {
        _gameController = GameController.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlanetDied()
    {
        planetAlive -= 1;
        if (planetAlive <= 0)
        {
            _gameController.GameOver("OOPS! One of the planet ran out of resource \n PRESS  ANY  KEY  TO  PLAY  AGAIN");
        }
    }

    public void PlanetSaved()
    {
        planetAlive += 1;
    }
}
