using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipCollision : MonoBehaviour
{
    public GameObject playerShip;
    public GameObject GameController;
    public float maxVelocityWithoutCrashing;
    public GameObject mainMusicController;
    private Rigidbody2D _playerRigidbody;
    private PlayerStates _state;
    private Animator _animator;
    public static GameObject Player;

    private bool _landing = false;
    private bool _crashing = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerRigidbody = playerShip.GetComponent<Rigidbody2D>();
        _state = playerShip.GetComponentInParent<PlayerStates>();
        _animator = playerShip.GetComponent<Animator>();
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Land(GameObject planet)
    {
        if (PlayerStates.canLand)
        {
            mainMusicController.GetComponent<DrumChanger>().RemoveDrum();
            _playerRigidbody.velocity = Vector3.zero;
            _playerRigidbody.angularVelocity = 0;
            float angle = Vector3.SignedAngle(Vector3.up, playerShip.transform.position - planet.transform.position, Vector3.forward);




            _playerRigidbody.SetRotation(angle);
            _state.SetState(PlayerStates.States.InLandedShip);
            _state.SetPlanet(planet);

            planet.GetComponent<Planet>().LandOnPlanet(transform.parent.parent.gameObject);

            Player.transform.parent = planet.transform;
        }
    }

    void Crash()
    {
        if (!ShipMovement.canCrash)
        {
            return;
        }

        _crashing = true;
        mainMusicController.GetComponent<DrumChanger>().StartFadeOut();
        GetComponent<AudioSource>().Play();
        _animator.SetTrigger("Crash");
        _state.SetState(PlayerStates.States.Crashed);
        GameController.GetComponent<GameController>().Crash();


    }

    public void LandingCollision(Collider2D other)
    {
        if (_crashing)
        {
            return;
        }

        Vector2 planetSpeed = Vector2.zero;
        PlanetOrbit plan = other.transform.gameObject.GetComponent<PlanetOrbit>();

        if (plan!= null)
        {
            planetSpeed = other.transform.gameObject.GetComponent<PlanetOrbit>().currentSpeed;
        }

        Debug.Log((_playerRigidbody.velocity - planetSpeed).magnitude);

        if ((playerShip.GetComponent<ShipMovement>().lastSpeed - planetSpeed).magnitude <= maxVelocityWithoutCrashing && other.transform.tag != "crash")
        {
            _landing = true;
            Land(other.gameObject);
            StartCoroutine(ResetLandingBool());
        }
        else
        {
            Debug.Log("Crashed landing too quick");
            Crash();
        }
    }

    IEnumerator ResetLandingBool()
    {
        yield return new WaitForSeconds(2);
        _landing = false;
    }

    public void CrashingCollision(Collider2D other)
    {
        if (_landing)
        {
            return;
        }

        if (_state.GetState() != PlayerStates.States.InLandedShip && _state.GetState() != PlayerStates.States.WalkingOnPlanet)
        {
            Debug.Log("Crashed crashing collider");
            
            Crash();
        }
    }
}
