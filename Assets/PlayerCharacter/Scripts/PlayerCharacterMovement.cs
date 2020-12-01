using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacterMovement : MonoBehaviour
{
    public float speed;
    public float spriteOffset;
    public GameObject currentPlanet;
    public GameObject playerShip;
    public GameObject CommandShower;

    public Sprite magmaWalk;
    public Sprite wetsuit;
    public Sprite lumberjack;
    public Sprite yukon;
    private SpriteRenderer _spriteRenderer;
    private float currentAngle;
    private Rigidbody2D _rigidbody;
    private float _planetRadius;
    private PlayerStates _playerStates;
    private bool _isTouchingResource = false;
    private Animator _animator;
    private string _type;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ResourceCollector>().isTouchingResource += SetIsTouchingResource;

        _animator = GetComponent<Animator>();


        _playerStates = GetComponentInParent<PlayerStates>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        currentAngle = Vector3.SignedAngle(Vector3.up, transform.position - currentPlanet.transform.position, 
            Vector3.forward);
        _rigidbody.rotation = currentAngle;
        _planetRadius = currentPlanet.GetComponent<CircleCollider2D>().radius * currentPlanet.transform.localScale.x;

        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            StartWalking();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerStates.GetState() == PlayerStates.States.InFlyingShip)
        {
            return;
        }

        

        if (Input.GetButtonDown("Action"))
        {
            if (_playerStates.GetState() == PlayerStates.States.InLandedShip)
                {
                    if (ControlIndicator.currentCommand == "TO  LEAVE  SHIP" && ControlIndicator.isActive)
                    {
                    CommandShower.GetComponent<ControlIndicator>().HideCommand();
                    }

                StartWalking();
                }
        }

        if (Input.GetButton("Horizontal"))
        {
            if (_playerStates.GetState() == PlayerStates.States.WalkingOnPlanet)
            {
                FlipSprite(Input.GetAxis("Horizontal"));

                if (_isTouchingResource)
                {
                    StopWalkingAnim();
                    return;
                }

                StartWalkingAnim();
                currentAngle -= Input.GetAxis("Horizontal") * speed * Time.deltaTime;

                _rigidbody.rotation = currentAngle;
                transform.position = currentPlanet.transform.position + (spriteOffset+_planetRadius) * (Quaternion.AngleAxis(currentAngle, Vector3.forward) * Vector3.up);
            }
        }
        else
        {
            StopWalkingAnim();
        }
    }

    void FlipSprite(float horizontalAxis)
    {
        if (horizontalAxis > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }
    }

    void StartWalking()
    {
        if (_playerStates.SetState(PlayerStates.States.WalkingOnPlanet))
        {
            transform.position = playerShip.transform.position;
        
            GetComponent<SpriteRenderer>().enabled = true;

            currentPlanet = _playerStates.GetPlanet();
            if (currentPlanet.GetComponent<ResourcePlanet>() != null)
            {
                ResetAllAnimationBool();
                SetAnimationType(currentPlanet.GetComponent<ResourcePlanet>().producingResourceType.resourceName);
            }
            else
            {
                SetAnimationType("Wood");
            }

            currentAngle = Vector3.SignedAngle(Vector3.up, transform.position - currentPlanet.transform.position,
                Vector3.forward);

            _rigidbody.rotation = currentAngle;
            _planetRadius = currentPlanet.GetComponent<CircleCollider2D>().radius * currentPlanet.transform.localScale.x;

            transform.position = currentPlanet.transform.position + (spriteOffset + _planetRadius) * (Quaternion.AngleAxis(currentAngle, Vector3.forward) * Vector3.up);
        }   
    }

    void SetIsTouchingResource(bool b)
    {
        _isTouchingResource = b;
    } 

    void SetAnimationType(string type)
    {
        _type = type;
        if (type == "Ice")
        {
            _animator.SetBool("yukonWalking", true);
            
        }
        else if (type =="Magma")
        {
            _animator.SetBool("LavaWalking", true);
            
        }
        else if (type == "Water")
        {
            _animator.SetBool("wetsuitWalking", true);
            
        }
        else if (type == "Wood")
        {
            _animator.SetBool("lumberjackWalking", true);
            
        }

        _animator.SetBool("isWalking", true);
    }

    void StopWalkingAnim()
    {
        if (_type == "Ice")
        {
            _animator.Play("yukonWalking", 0, 0.0f);
        }
        else if (_type == "Magma")
        {
            _animator.Play("LavaWalking", 0, 0.0f);

        }
        else if (_type == "Water")
        {
            _animator.Play("wetsuitWalking", 0, 0.0f);

        }
        else if (_type == "Wood")
        {
            _animator.Play("lumberjackWalking", 0, 0.0f);
        }

        _animator.speed = 0;
    }

    void StartWalkingAnim()
    {
        _animator.speed = 1f;
    }

    void ResetAllAnimationBool()
    {
        _animator.SetBool("LavaWalking", false);
        _animator.SetBool("yukonWalking", false);
        _animator.SetBool("wetsuitWalking", false);
        _animator.SetBool("lumberjackWalking", false);
    }
}
