using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, move;
    public string currentState;
    public string currentAnimation;

    //public Rigidbody2D m_Rigidbody2D;

    private float moveController;
    private float turnController;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _turnSpeed = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        currentState = "idle";
        SetCharacterState(currentState);
    }

    // Update is called once per frame
    void Update()
    {
        TankMove();
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
            turnController = horizontalInput * - _turnSpeed * Time.deltaTime;
        }
        else
        {
            SetCharacterState("idle");
        }
    }

    private void LateUpdate()
    {
        transform.Translate(0f, moveController, 0f);
        transform.Rotate(0f, 0f, turnController);
    }
}
