using System.Collections;
using UnityEngine;

public class JumpingState : BaseState
{
    public float jumpForce = 7.0f;
    private float gravityDelay = 0.5f; // Delay before gravity starts
    private float jumpStartTime; // Track jump start time

    public override void Construct()
    {
        motor.verticalVelocity = jumpForce; // Apply jump force
        jumpStartTime = Time.time; // Save the time when the jump starts
        motor.ani?.SetTrigger("Jump");
    }

    public override Vector3 ProcessMotion()
    {
        // Delay gravity application
        if (Time.time - jumpStartTime > gravityDelay)
        {
            motor.ApplyGravity(); // Apply gravity after 1 second
        }

        // Create movement vector
        Vector3 movement = Vector3.zero;
        movement.x = motor.SnapToLane();
        movement.y = motor.verticalVelocity;
        movement.z = motor.baseRunSpeed;

        return movement;
    }

    public override void Transition()
    {
        // Switch to FallingState when velocity is negative
        if (motor.verticalVelocity < 0)
        {
            motor.ChangeState(motor.GetComponent<FallingState>());
        }
       
    }
}
