using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float force;
    public float maxSpeed;
    public float torque;
    public float fuelConsuptionPerSecond;
    public float fueldConsuptionPerSecondOnTurn;
    public float volumeChangeRate;
    public float maxVolume;
    public float minVolume;
    public GameObject mainMusicController;
    public static bool canCrash = true;
    public GameObject CommandShower;
    public Vector2 lastSpeed;

    private Rigidbody2D _rigidbody;
    private PlayerStates _playerStates;
    private FuelController _fuelController;
    private AudioSource _shipSound;
    private int _soundVolumeStatus = 0;
    private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerStates = GetComponentInParent<PlayerStates>();
        _fuelController = GetComponent<FuelController>();
        _shipSound = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _shipSound.volume = 0f;
    }

    void PlaySound()
    {

        _soundVolumeStatus = 1;

    }

    void StopSound()
    {

        _soundVolumeStatus = -1;

    }

    void UpdateSoundVolume()
    {
        if (_soundVolumeStatus == 0)
        {
            return;
        }
        float newVolume = _shipSound.volume + _soundVolumeStatus*Time.deltaTime * volumeChangeRate;
        if (newVolume > maxVolume)
        {
            _shipSound.volume = maxVolume;
            _soundVolumeStatus = 0;
        }
        else if (newVolume < 0)
        {
            _shipSound.volume = 0;
            _soundVolumeStatus = 0;
        }
        else
        {
            _shipSound.volume = newVolume;
        }
    }

     IEnumerator shipLeavingPlanetDelay()
    {
        yield return new WaitForSeconds(1f);
        canCrash = true;
        PlayerStates.canLand = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSoundVolume();

        if (_fuelController.IsEmpty())
        {
            _animator.SetBool("Accelerating", false);
            StopSound();
            return;
        }

        if (_playerStates.GetState() == PlayerStates.States.InLandedShip)
        {
            if (ControlIndicator.currentCommand != "TO  LEAVE  SHIP" || !ControlIndicator.isActive)
            {
                CommandShower.GetComponent<ControlIndicator>().ShowCommand("TO  LEAVE  SHIP");
            }
            if (Input.GetButtonDown("Vertical"))
            {
                if (Input.GetAxis("Vertical") > 0)
                {
                    PlayerShipCollision.Player.transform.parent = null;
                    PlayerStates.canLand = false;
                    _animator.SetBool("Accelerating", true);


                    if (_playerStates.SetState(PlayerStates.States.InFlyingShip))
                    {
                        if (ControlIndicator.currentCommand == "TO  LEAVE  SHIP" && ControlIndicator.isActive)
                        {
                            CommandShower.GetComponent<ControlIndicator>().HideCommand();
                        }

                        canCrash = false;
                        StartCoroutine(shipLeavingPlanetDelay());

                        mainMusicController.GetComponent<DrumChanger>().AddDrum();
                        PlaySound();

                        PlayerShipCollision.Player.transform.parent = null;

                        _rigidbody.AddForce(force*100 * transform.up*Time.deltaTime);
                        _fuelController.ConsumeFuel(fuelConsuptionPerSecond * Time.deltaTime * 125);


                        _playerStates.GetPlanet().GetComponent<Planet>().LeavePlanet();
                    }  
                }
            }
            else
            {
                _animator.SetBool("Accelerating", false);
                StopSound();
            }
        }

        if (_playerStates.GetState() == PlayerStates.States.InFlyingShip)
        {

            if (Input.GetButton("Vertical"))
            {
               
                PlaySound();

                _fuelController.ConsumeFuel(fuelConsuptionPerSecond * Time.deltaTime);

                if (Input.GetAxis("Vertical") > 0)
                {
                    _animator.SetBool("Accelerating", true);
                    _animator.SetBool("Back", false);

                    _rigidbody.AddForce(force * transform.up * Time.deltaTime);
                }
                else
                {
                    _animator.SetBool("Accelerating", false);
                    _animator.SetBool("Back", true);
                    _rigidbody.AddForce(-force * transform.up * Time.deltaTime);
                }

            }

            if (Input.GetButton("Horizontal"))
            {

                PlaySound();
                _fuelController.ConsumeFuel(fueldConsuptionPerSecondOnTurn * Time.deltaTime);

                if (Input.GetAxis("Horizontal") > 0)
                {
                    _rigidbody.AddTorque(-torque * Time.deltaTime);
                }
                else
                {
                    _rigidbody.AddTorque(torque * Time.deltaTime);
                }
            }

            if (_rigidbody.velocity.magnitude >= maxSpeed)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * maxSpeed;
            }

            if (!Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))
            {
                StopSound();
                _animator.SetBool("Accelerating", false);
                _animator.SetBool("Back", false);
            }
        }
        else
        {
            StopSound();
        }

        VerifyIfPlayersIsInbound();

    }

    private void FixedUpdate()
    {
        
            lastSpeed = _rigidbody.velocity;
        
    }

    void VerifyIfPlayersIsInbound()
    {
        UnityEngine.Vector2 newPos;
        if (transform.position.x > 105)
        {
            newPos = transform.position;
            newPos.x = (transform.position.x - 200);
            transform.position = newPos;
        }
        if (transform.position.x <-95)
        {
            newPos = transform.position;
            newPos.x = (transform.position.x + 200);
            transform.position = newPos;
        }
        if (transform.position.y > 90)
        {
            newPos = transform.position;
            newPos.y = (transform.position.y - 200);
            transform.position = newPos;
        }
        if (transform.position.y < -110)
        {
            newPos = transform.position;
            newPos.y = (transform.position.y + 200);
            transform.position = newPos;
        }

        
    }

    
}
