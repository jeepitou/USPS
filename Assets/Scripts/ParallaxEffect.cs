using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public GameObject ObjectThatCameraIsFollowing;
    public float speedRatio;

    private Rigidbody2D _rigidBodyOfMovingObject;
    private Vector2? lastPos = null;
    private Vector2 newPos;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBodyOfMovingObject = ObjectThatCameraIsFollowing.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lastPos == null)
        {
            lastPos = ObjectThatCameraIsFollowing.transform.position;
            return;
        }

        newPos = ObjectThatCameraIsFollowing.transform.position;

        transform.position = (Vector2)transform.position + (newPos - (Vector2)lastPos) * speedRatio;

        lastPos = newPos;
    }
}
