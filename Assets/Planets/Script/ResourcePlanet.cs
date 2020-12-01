using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePlanet : Planet
{
    public GameObject PlanetStateTracker;
    public GameObject GameController;
    public Resource requiredResourceType;
    public Resource producingResourceType;
    public float resourceDepositRate;
    public int initResourceToConsume;
    public float refreshTime;
    public float basicProductionRate;
    public int maxResourceToGather;
    public int maxResourceToConsume;

    [HideInInspector]public float productionRate;

    private float _currentResourceToConsume;
    private int _currentResourceToGather;
    private Slider _slider;
    private PlanetStateTracker _stateTracker;
    private GameController _gameController;

    private float _counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        _currentResourceToConsume = initResourceToConsume;
        UpdateResourceQuantity();
        _slider = GetComponentInChildren<Slider>();
        _stateTracker = PlanetStateTracker.GetComponent<PlanetStateTracker>();
        _gameController = GameController.GetComponent<GameController>();
    }

    public void UpdateProductionRate()
    {
        if (_currentResourceToConsume == 0)
        {
            productionRate = 0;
            return;
        }

        if (_currentResourceToGather >= (int)(maxResourceToGather * 0.75f))
        {
            productionRate = 0.7f;
        }
        else if (_currentResourceToGather <= (int)(0.3f * maxResourceToGather) && _currentResourceToConsume >= 0.6f * maxResourceToConsume)
        {
            productionRate = 1.5f;
        }
        else
        {
            productionRate = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_counter >= refreshTime)
        {
            ConsumeResource();
            UpdateResourceQuantity();
            UpdateProductionRate();

            if (_characterOnPlanet)
            {
                ReceiveResource();
            }

            RefreshGauge();

            _counter = 0;
        }

        

        _counter += Time.deltaTime;
    }

    void RefreshGauge()
    {
        _slider.value = _currentResourceToConsume / maxResourceToConsume;
    }

    void UpdateResourceQuantity()
    {
        int resource = 0;

        for (int i = 0; i< transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<ResourceSpawner>() != null)
            {
                resource += transform.GetChild(i).GetComponent<ResourceSpawner>().availableResources;
            }
            
        }

        _currentResourceToGather = resource;
    }

    void ConsumeResource()
    {
        if (_currentResourceToConsume == 0)
        {
            return;
        }

        _currentResourceToConsume -= basicProductionRate * productionRate * refreshTime * 0.5f;

        if (_currentResourceToConsume <= 0)
        {
            _stateTracker.PlanetDied();

            _currentResourceToConsume = 0;
        }
    }

    void ReceiveResource()
    {
        if (ResourceBank.GetResourceValue(requiredResourceType) == 0)
        {
            return;
        }

        if (_currentResourceToConsume == 0)
        {
            _stateTracker.PlanetSaved();
        }

        
        int resourceToReceive = (int)(refreshTime * resourceDepositRate);
        if (ResourceBank.GetResourceValue(requiredResourceType) > resourceToReceive)
        {
            _currentResourceToConsume += resourceToReceive;
            ResourceBank.RemoveResource(requiredResourceType, resourceToReceive);
            _gameController.score += resourceToReceive * 5;
            _gameController.UpdateScore();
        }
        else if (ResourceBank.GetResourceValue(requiredResourceType) != 0)
        {
            _currentResourceToConsume += ResourceBank.GetResourceValue(requiredResourceType);
            _gameController.score += ResourceBank.GetResourceValue(requiredResourceType) * 5;
            ResourceBank.holdedResource[requiredResourceType] = 0;
            _gameController.UpdateScore();
        }
        
    }


}
