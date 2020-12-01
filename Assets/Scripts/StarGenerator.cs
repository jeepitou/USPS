using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Random = UnityEngine.Random;

public class StarGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _starPrefab;

    public float worldMinX;
    public float worldMaxX;
    public float worldMinY;
    public float worldMaxY;

    public float minDelta;
    public float maxDelta;

    public float minStarScale;
    public float maxStarScale;
    public float minIntensity;
    public float maxIntensity;

    private float _currentX;
    private float _currentY;

    private Light2D _light;

    private void Awake()
    {
        _light = _starPrefab.GetComponent<Light2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentX = worldMinX;
        _currentY = worldMaxY;

        while (_currentY > worldMinY)
        {
            while (_currentX < worldMaxX)
            {
                _currentX += Random.Range(minDelta, maxDelta);
                GenerateRandomStarAtPosition(new Vector2(_currentX, _currentY + Random.Range(-0.4f, 0.4f)));
            }

            _currentX = worldMinX + Random.Range(minDelta, maxDelta);
            _currentY -= Random.Range(minDelta, maxDelta);
        }
        
        Random.Range(minDelta, maxDelta);
    }

    void GenerateRandomStarAtPosition(Vector2 starPosition)
    {
        _light.intensity = Random.Range(minIntensity, maxIntensity);
        float scale = Random.Range(minStarScale, maxStarScale);
        _light.transform.localScale = new Vector3(scale, scale, 1);
        Instantiate(_starPrefab, starPosition, Quaternion.identity, transform);
    }

}
