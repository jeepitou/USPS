using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public float gatherTime;
    public int spriteIncrement;
    public int availableResources;
    public Resource resourceType;
    public Action addedResource;
    public Action spawnedResource;

    private ResourcePlanet planet;
    private float _currentTime = 0f;
    private int _nextResourceToSpawn = 0;
    private int _maxResource;
    private bool _playerOnTopOfResource = false;

    // Start is called before the first frame update
    void Start()
    {
        _maxResource = transform.childCount*(spriteIncrement) ;

        availableResources = (int)(_maxResource / 2);

        transform.GetChild((int)(availableResources/spriteIncrement)).gameObject.SetActive(true);

        planet = GetComponentInParent<ResourcePlanet>();

    }

    // Update is called once per frame
    void Update()
    {
        if (planet.productionRate == 0)
        {
            return;
        }

        if (_playerOnTopOfResource)
        {
            return;
        }

        if (_currentTime >= 1/planet.productionRate)
        {
            SpawnNewResource();
            
        }

        _currentTime += Time.deltaTime;
    }

    public bool IsEmpty()
    {
        if (availableResources == 0)
        {
            return true;
        }
        return false;
    }



    void SpawnNewResource()
    {
        if (availableResources >= _maxResource ) 
        {
            return;
        }

        addedResource?.Invoke();

        if (availableResources == 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            spawnedResource?.Invoke();
        }

        availableResources += 1;

        _currentTime = 0f;
        
        if (availableResources % spriteIncrement == 1)
        {
            if (availableResources > 1)
            {
                transform.GetChild((availableResources-1)/spriteIncrement-1).gameObject.SetActive(false);
            }
            transform.GetChild((availableResources - 1) / spriteIncrement).gameObject.SetActive(true);
            spawnedResource?.Invoke();
        }
    }

    public int GatherResource(int quantity)
    {
        _currentTime = 0f;

        if (availableResources == 0)
        {
            return 0;
        }

        if (availableResources >= quantity)
        {
            for (int i=0; i<quantity; i++)
            {
                availableResources -= 1;
                if (availableResources % spriteIncrement == 0)
                {
                    if (availableResources/spriteIncrement >= 1)
                    {
                        transform.GetChild(availableResources / spriteIncrement - 1).gameObject.SetActive(true);
                        transform.GetChild(availableResources / spriteIncrement).gameObject.SetActive(false);
                    }
                    else
                    {
                        transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
            }
            return quantity;
        }
        else
        {
            int value = availableResources;
            
            for (int i = 0; i < value; i++)
            {
                availableResources -= 1;

                if (availableResources % spriteIncrement == 0)
                {
                    if (availableResources / spriteIncrement >= 1)
                    {
                        transform.GetChild(availableResources / spriteIncrement - 1).gameObject.SetActive(true);
                        transform.GetChild(availableResources / spriteIncrement).gameObject.SetActive(false);
                    }
                    else
                    {
                        transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
            }
            return value;
        }
    }

    public int AvailableResource()
    {
        return _nextResourceToSpawn;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            _playerOnTopOfResource = true;
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            _playerOnTopOfResource = false;
        }
    }
}
