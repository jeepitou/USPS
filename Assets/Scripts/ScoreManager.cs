using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float baseSpeed;
    public float speedIncrement;
    public float maxSpeed;

    private int _currentLevel = 1;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = baseSpeed;
    }

    public void LevelUp()
    {
        _currentLevel += 1;

        if (Time.timeScale == maxSpeed)
        { return; }

        if (Time.timeScale + speedIncrement >= maxSpeed)
        {
            Time.timeScale = maxSpeed;
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > 10)
        {
            LevelUp();
            _timer = 0;
        }


        _timer += Time.deltaTime;
    }
}
