using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject playerShip;
    public GameObject playerCharacter;
    public float updateTime;

    private PlayerStates _states;

    void Start()
    {
        _states = player.GetComponent<PlayerStates>();

        Vector3 newPos = playerShip.transform.position;
        newPos.z = transform.position.z;

        transform.position = newPos;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (_states.GetState() == PlayerStates.States.WalkingOnPlanet)
        {
            Vector3 newPos = _states.GetPlanet().transform.position;
            newPos.z = transform.position.z;

            transform.position = Vector3.MoveTowards(transform.position, newPos, 0.6f);
        }
        else
        {


            Vector3 newPos = playerShip.transform.position;
            newPos.z = transform.position.z;

            //transform.position = Vector3.MoveTowards(transform.position, newPos, 2);
            transform.position = newPos;

        }
    }
}
