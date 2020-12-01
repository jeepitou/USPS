using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterInShip : MonoBehaviour
{
    public GameObject ship;
    public GameObject CommandShower;
    private bool _isTouchingShip = false;
    private PlayerStates _state;

    // Start is called before the first frame update
    void Start()
    {
        _state = GetComponentInParent<PlayerStates>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_state.GetState() != PlayerStates.States.WalkingOnPlanet)
        {
            return;
        }

        if (Input.GetButtonDown("Action"))
        {
            Debug.Log("ACTION");
            if (_isTouchingShip)
            {
                EnterShip();
            }
        }
    }

    void EnterShip()
    {
        if (_state.SetState(PlayerStates.States.InLandedShip))
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_state.GetState() != PlayerStates.States.WalkingOnPlanet)
        {
            return;
        }

        if (collision.tag == "ship")
        {
            _isTouchingShip = true;

            if (ControlIndicator.currentCommand != "TO  ENTER  SHIP" || !ControlIndicator.isActive)
            {
                CommandShower.GetComponent<ControlIndicator>().ShowCommand("TO  ENTER  SHIP");
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ship")
        {
            _isTouchingShip = false;

            if (ControlIndicator.currentCommand == "TO  ENTER  SHIP" && ControlIndicator.isActive)
            {
                CommandShower.GetComponent<ControlIndicator>().HideCommand();
            }
        }
    }

}
