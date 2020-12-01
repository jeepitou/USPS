using System;
using UnityEngine;

public class FuelController : MonoBehaviour
{
    public int maxFuel;
    public float currentFuel;
    public Action TankIsEmpty;
    public GameObject fuelUIObject;
    private HealthBar _fuelBar;
    private Animator _fuelTankUIAnim;


    // Start is called before the first frame update
    void Start()
    {
        _fuelBar = fuelUIObject.GetComponent<HealthBar>();
        currentFuel = maxFuel;
        _fuelBar.SetMaxValue(maxFuel);
        _fuelBar.SetValue(maxFuel);
        _fuelTankUIAnim = fuelUIObject.GetComponent<Animator>();
    }

    public void ConsumeFuel(float fuel)
    {
        currentFuel -= fuel;

        _fuelBar.SetValue(currentFuel);

        if (currentFuel<= maxFuel*0.3f)
        {
            _fuelTankUIAnim.SetBool("LowGaz", true);
        }

        if (currentFuel <= 0)
        {
            currentFuel = 0;
            TankIsEmpty?.Invoke();
        }
    }

    public void AddFuel(float fuel)
    {
        currentFuel += fuel;

        _fuelBar.SetValue(currentFuel);

        if (currentFuel >= maxFuel * 0.3f)
        {
            _fuelTankUIAnim.SetBool("LowGaz", false);
        }

        if (currentFuel >= maxFuel)
        {
            currentFuel = maxFuel;
        }
    }

    public bool IsEmpty()
    {
        if (currentFuel == 0)
        {
            return true;
        }
        return false;
    }
}
