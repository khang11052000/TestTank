using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, move, attack;
    public string currentState;
    public string currentAnimation;

    private Shooting _shooting;
    
    public float moveController;
    private float _turnController;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _turnSpeed = 100;

    public UnityEvent<float> OnSpeedChange = new UnityEvent<float>();
    
    // Start is called before the first frame update

    private void Awake()
    {
        _shooting = GetComponent<Shooting>();
    }

    void Start()
    {
        currentState = "idle";
        SetCharacterState(currentState);
    }

    // Update is called once per frame
    void Update()
    {
        TankMove();
        TankShoot();
    }

    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(currentAnimation))
        {
            return;
        }
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        currentAnimation = animation.name;
    }

    public void SetCharacterState(string state)
    {

        if (state.Equals("idle"))
        {
            SetAnimation(idle, true, 1f);
        }
        else if (state.Equals("move"))
        {
            SetAnimation(move, true, 2f);
        }else if (state.Equals("attack"))
        {
            SetAnimation(attack, true, 1f);
        }

        
    }

    public void TankMove()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        if ((horizontalInput != 0) || (verticalInput != 0))
        {
            SetCharacterState("move");
            moveController = verticalInput * _moveSpeed * Time.deltaTime;
            _turnController = horizontalInput * - _turnSpeed * Time.deltaTime;
            OnSpeedChange?.Invoke(this.moveController);
        }
        else
        {
            SetCharacterState("idle");
        }
        
    }

    public void TankShoot()
    {
        if (_shooting.currentDelay >= _shooting.reloadDelay/1.15)
        {
            SetCharacterState("attack");
            
        }
    }

    private void LateUpdate()
    {
        transform.Translate(0f, moveController, 0f);
        transform.Rotate(0f, 0f, _turnController);
    }
}
