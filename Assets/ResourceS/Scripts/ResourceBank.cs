using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBank : MonoBehaviour
{
    public static int maxValue = 50;
    public static Dictionary<Resource, int> holdedResource = new Dictionary<Resource, int>();

    private void Start()
    {
        holdedResource = new Dictionary<Resource, int>();
    }

    public static void AddResource(Resource res, int amount)
    {
        if (holdedResource.ContainsKey(res))
        {
            if (holdedResource[res] + amount >= maxValue)
            {
                holdedResource[res] = maxValue;
            }
            else
            {
                holdedResource[res] += amount;
            }
            
        }
        else
        {
           holdedResource.Add(res, amount);
        }
    }


    public static int GetResourceValue(Resource res)
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

    public static void RemoveResource(Resource res, int amount)
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
}
