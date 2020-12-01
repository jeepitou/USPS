using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceCollector : MonoBehaviour
{
    public static int resourceGatheredPerShot = 5;
    public Action<bool> isTouchingResource;
    public int resourceCollected = 0;
    public GameObject CommandShower;
    public GameObject collectText;
    private PlayerStates _state;
    private bool _isTouchingResource = false;
    private SpriteRenderer _spriteRenderer;
    private float _spriteHeight;
    private float _spriteWidth;
    private LayerMask _resourceLayer;
    private ResourceSpawner _currentResource;
    private float _timerSpentGathering;
    private string _type;
    private TextMeshProUGUI _animText;
    private Animator _animTextAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _state = GetComponentInParent<PlayerStates>();
        _spriteHeight = _spriteRenderer.bounds.size.y;
        _spriteWidth = _spriteRenderer.bounds.size.x;
        _resourceLayer = LayerMask.GetMask("resource");
        _animText = collectText.GetComponent<TextMeshProUGUI>();
        _animTextAnimator = collectText.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_state.GetState() != PlayerStates.States.WalkingOnPlanet)
        {
            return;
        }

        if (Input.GetButton("Action"))
        {
            if (_isTouchingResource)
            {
                GatherResource();
            }
        }

        VerifyIfTouchingResource();
    }

    void GatherResource()
    {

        if (_currentResource != null)
        {
            Resource type = _currentResource.resourceType;

            if (_timerSpentGathering >= _currentResource.gatherTime)
            {
                if (ResourceBank.GetResourceValue(type) > 45)
                {
                    _animText.text = "+" + (50 - ResourceBank.GetResourceValue(type)).ToString();
                }
                else
                {
                    _animText.text = "+5";
                }
               
                _animText.enabled = true;
                _animTextAnimator.SetBool("text", true);
                _animTextAnimator.Play("text", 0, 0);
                _currentResource.GatherResource(resourceGatheredPerShot);
                ResourceBank.AddResource(type, resourceGatheredPerShot);

                
                _timerSpentGathering = 0;
            }
            _timerSpentGathering += Time.deltaTime;
        }
    }

    private void VerifyIfTouchingResource()
    {
        Vector2 dir;
        
        if (_spriteRenderer.flipX)
        {
            dir = -transform.right;
        }
        else
        {
            dir = transform.right;
        }

        Debug.DrawRay(transform.position, dir);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, _spriteWidth/2+0.3f, _resourceLayer);

        // If it hits something...
       if (hit.collider != null)
        {
            if (hit.transform.tag == "resource")
            {
                _currentResource = hit.transform.gameObject.GetComponent<ResourceSpawner>();

                if (_currentResource == null)
                {
                    return;
                }
                if (!_currentResource.IsEmpty() && !_isTouchingResource)
                {
                    
                    _isTouchingResource = true;
                    ShowResourceCommand();
                    
                }

                if (!_currentResource.IsEmpty())
                {
                    if ((hit.point - (Vector2)transform.position).magnitude <= 0.2f)
                    {
                        isTouchingResource?.Invoke(true);
                    }
                }
                

                if (_currentResource.IsEmpty() && _isTouchingResource)
                {
                    isTouchingResource?.Invoke(false);
                    _isTouchingResource = false;
                    HideResourceCommand();
                }
                
                return;

            }
        }
       
        if (_isTouchingResource)
        {
            _isTouchingResource = false;
            isTouchingResource?.Invoke(false);
            HideResourceCommand();
            _currentResource = null;
            _timerSpentGathering = 0;
        }
        
    }

    private void ShowResourceCommand()
    {
        _type = _currentResource.resourceType.resourceName;
        if (ControlIndicator.currentCommand != "TO  GATHER  " + _type || !ControlIndicator.isActive)
        {
            CommandShower.GetComponent<ControlIndicator>().ShowCommand("TO  GATHER  " + _type);
        }
    }

    private void HideResourceCommand()
    {
        if (ControlIndicator.currentCommand == "TO  GATHER  " + _type && ControlIndicator.isActive)
        {
            CommandShower.GetComponent<ControlIndicator>().HideCommand();
        }
    }
}
