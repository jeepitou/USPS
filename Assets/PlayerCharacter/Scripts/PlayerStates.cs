using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStates : MonoBehaviour
{
    public Dictionary<Resource, int> holdedResource;
    private States _state = States.InFlyingShip;
    private GameObject _planet = null;
    private float _timeSinceLastStateChange = 0.2f;
    public static bool canLand = true;

    public enum States
    {
        InFlyingShip,
        InLandedShip,
        WalkingOnPlanet,
        Crashed
    }

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {            
            _planet = GameObject.Find("Wood Planet");
        }
    }

    public void AddResource(Resource res, int amount)
    {
        if (holdedResource.ContainsKey(res))
        {
            holdedResource[res] = holdedResource[res] + amount;
        }
        else
        {
            holdedResource.Add(res, amount);
        }
    }


    public int GetResourceValue(Resource res, int amount)
    {
        if (holdedResource.ContainsKey(res))
        {
            return holdedResource[res];
        }
        else
        {
            return 0;
        }
    }

    public void RemoveResource(Resource res, int amount)
    {
        if (holdedResource.ContainsKey(res))
        {
            holdedResource[res] = holdedResource[res] - amount;
        }
        else
        {
            Debug.LogError("Tried to remove a ressource that we dont have yet");
        }
    }

    private void Update()
    {
        if (_timeSinceLastStateChange >= 0.5f)
        {
            return;
        }

        _timeSinceLastStateChange += Time.deltaTime;
    }

    public States GetState()
    {
        return _state;
    }

    public bool SetState(States newState)
    {
        if (_timeSinceLastStateChange >=0.1f)
        {
            _timeSinceLastStateChange = 0f;
            _state = newState;
            return true;
        }
        return false;
    }

    public void SetPlanet(GameObject planet)
    {
        _planet = planet;
    }

    public GameObject GetPlanet()
    {
        return _planet;
    }

}
