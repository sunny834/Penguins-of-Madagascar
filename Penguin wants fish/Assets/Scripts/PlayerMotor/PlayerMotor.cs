using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [HideInInspector] public Vector3 moveVector;
    [HideInInspector] public float verticalVelocity;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public int currentLane;

    public float distanceInBetweenLenes = 3.0f;
    public float baseRunSpeed = 5.0f;
    public float baseSidewaySpeed = 10.0f;
    public float gravity = 14.0f;
    public float terminalVelocity = 20.0f;

    public CharacterController controller;
    private BaseState state;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        state = GetComponent<RunningState>();
        state.Construct();
    }
    private void Update()
    {
        UpdateMotor();
    }

    private void UpdateMotor()
    {
        //check if we're grounded
        isGrounded = controller.isGrounded;
        // How should we be moving right now? based on state
        moveVector = state.ProcessMotion();
        //are we trying to change state ?
        state.Transition();
        //move player
        controller.Move(moveVector * Time.deltaTime);

    }

    public float SnapTolane()
    {
        float r = 0.0f;
        // if we are not directly on top of a lane
        if (transform.position.x != (currentLane * distanceInBetweenLenes))
        {
            float deltaToDesiredPosition = (currentLane * distanceInBetweenLenes) - transform.position.x;
            r = (deltaToDesiredPosition > 0) ? 1 : -1;
            r *= baseSidewaySpeed;
            float actualDistance = r * Time.deltaTime;
            if (Math.Abs(actualDistance) < Math.Abs(deltaToDesiredPosition))
            {
                r = deltaToDesiredPosition * (1 / Time.deltaTime);
            }
        }
        else
        {
            r = 0;
        }

            return r;
        
    }

    public void ChangeLane(int direction)
    {
        currentLane=Mathf.Clamp(currentLane + direction, -1, 1);
    }
    public void ChangeState(BaseState s)
    {
        state.Destruct();
        state = s;
        state.Construct();
    }
    public void ApplyGravity()
    {
        verticalVelocity -=gravity*Time.deltaTime;
        if(verticalVelocity < -terminalVelocity)
            verticalVelocity = -terminalVelocity;
    }

}
