using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOrbit : MonoBehaviour
{
    public GameObject centerPoint;
    public float movingSpeed;
    public float rotationSpeed;
    public Vector2 currentSpeed;


    private int _rotationSide;
    private float _radius;
    private float _currentAngle;
    private Vector2 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        ChooseRotationSide();
        CalculateRadius();

        _currentAngle = Random.Range(-180, 180);
        //_currentAngle = Vector3.SignedAngle(Vector3.up, transform.position - centerPoint.transform.position,
            //Vector3.forward);
    }

    void ChooseRotationSide()
    {
        if (Random.Range(-1, 1)>=0)
        {
            _rotationSide = 1;
        }
        else
        {
            _rotationSide = -1;
        }
    }

    void CalculateRadius()
    {
        _radius = (centerPoint.transform.position - transform.position).magnitude;
    }


    // Update is called once per frame
    void Update()
    {
        _currentAngle += _rotationSide * Time.deltaTime * movingSpeed;

        transform.position = centerPoint.transform.position + (_radius) * (Quaternion.AngleAxis(_currentAngle, Vector3.forward) * Vector3.up);

        currentSpeed = ((Vector2)transform.position - lastPos) / Time.deltaTime;

        lastPos = transform.position;
    }
}
