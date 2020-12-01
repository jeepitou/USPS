using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject mainStars;
    public GameObject mapStars;
    private Camera mapCamera;
    private bool _mapIsShown = false;

    // Start is called before the first frame update
    void Start()
    {
        mapCamera = GetComponent<Camera>();
        mapCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            ToggleMap();
        }
    }

    public void ToggleMap()
    {
        if (_mapIsShown)
        {
            HideMap();
        }
        else
        {
            ShowMap();
        }
    }

    void HideMap()
    {
        _mapIsShown = false;
        mainCamera.enabled = true;
        mapCamera.enabled = false;
        mapStars.SetActive(false);
        mainStars.SetActive(true);
    }

    void ShowMap()
    {
        _mapIsShown = true;
        mainCamera.enabled = false;
        mapCamera.enabled = true;
        mapStars.SetActive(true);
        mainStars.SetActive(false);
    }
}
