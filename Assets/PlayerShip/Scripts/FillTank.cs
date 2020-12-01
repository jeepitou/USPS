using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillTank : MonoBehaviour
{
    public GameObject resourceSpawner;

    private Slider _slider;


    // Start is called before the first frame update
    void Start()
    {

        resourceSpawner.GetComponent<ResourceSpawner>().addedResource += FillTankABit;
        resourceSpawner.GetComponent<ResourceSpawner>().spawnedResource += SwitchTank;
        _slider = GetComponent<Slider>();
    }

    void FillTankABit()
    {
        _slider.value += 1f / 4f;
    }

    void SwitchTank()
    {
        _slider.value = 0;
    }
}
