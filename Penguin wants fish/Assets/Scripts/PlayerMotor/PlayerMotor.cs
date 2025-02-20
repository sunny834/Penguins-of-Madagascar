using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [HideInInspector] public Vector3 moveVector;
    [HideInInspector] public float verticalVelocity;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public int currentLane = 0; // Default lane is center

    public float distanceInBetweenLanes = 3.0f;
    public float baseRunSpeed = 5.0f;
    public float baseSidewaySpeed = 10.0f;
    public float gravity = 14.0f;
    public float terminalVelocity = 20.0f;

    public CharacterController controller;
    public Animator ani;
    private BaseState state;
    private bool isPaused = true;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        ani = GetComponent<Animator>();
        state = GetComponent<RunningState>();

        if (state == null)
        {
            Debug.LogError("No RunningState found on Player!");
            return;
        }

        state.Construct();
    }

    private void Update()
    {
        if (!isPaused)
        {
            UpdateMotor();
        }
    }

    private void UpdateMotor()
    {
        // Check if the player is grounded
        isGrounded = controller.isGrounded;

        // Apply movement based on the state
        moveVector = state.ProcessMotion();

        // Smoothly move towards the lane position
        moveVector.x = SnapToLane();

        // Handle state transitions
        state.Transition();

        // Update animator
        ani?.SetBool("IsGrounded", isGrounded);
        ani?.SetFloat("Speed", Mathf.Abs(moveVector.z));

        // Move the player
        controller.Move(moveVector * Time.deltaTime);
    }

    public float SnapToLane()
    {
        float targetX = currentLane * distanceInBetweenLanes;
        float deltaToTarget = targetX - transform.position.x;

        // Prevent floating-point errors from causing jitter
        if (Mathf.Abs(deltaToTarget) < 0.05f)
        {
            transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
            return 0;
        }

        float r = Mathf.Sign(deltaToTarget) * baseSidewaySpeed;
        float actualMove = r * Time.deltaTime;

        // Prevent overshooting
        if (Mathf.Abs(actualMove) > Mathf.Abs(deltaToTarget))
        {
            return deltaToTarget / Time.deltaTime;
        }

        return r;
    }

    public void ChangeLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, -1, 1);
    }

    public void ChangeState(BaseState s)
    {
        if (state != null)
        {
            state.Destruct();
        }

        state = s;
        state.Construct();
    }

    public void ApplyGravity()
    {
        if (isGrounded)
        {
            if (verticalVelocity < 0)
                verticalVelocity = -2f; // Small value to stay grounded
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        verticalVelocity = Mathf.Clamp(verticalVelocity, -terminalVelocity, terminalVelocity);
    }

    public void PauseGame()
    {
        isPaused = true;
        moveVector = Vector3.zero; // Stop movement when paused
    }

    public void ResumeGame()
    {
        isPaused = false;
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        string hitLayerName= LayerMask.LayerToName(hit.gameObject.layer);
        if(hitLayerName=="Death")
        {
            ChangeState(GetComponent<DeathState>());
        }
    }

    public void RespawnPlayer()
    {
        Debug.Log("oy");
        ChangeState(GetComponent<RespawnState>());
        GameManager.Instance.ChangeCamera(GameManager.GameCamera.Respawn);
    }
    public void ResetPlayer()
    {
        currentLane = 0;
        PauseGame();
        transform.position = new Vector3(0, 0, -40);
        ani?.SetTrigger("Idle");
        ChangeState(GetComponent<RunningState>());
        


    }
}
