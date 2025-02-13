using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : BaseState
{
    public float jumpForce = 7.0f;
    public override void Construct()
    {
        motor.verticalVelocity = jumpForce;
    }
    public override Vector3 ProcessMotion()
    {
        //Apply gravity
        motor.ApplyGravity();
        //create our return vector
        Vector3 m=Vector3.zero;
        m.x = motor.SnapTolane();
        m.y = motor.verticalVelocity;
        m.z = motor.baseRunSpeed;

        return m;
    }
    public override void Transition()
    {
        if(motor.verticalVelocity<0)
        {
            motor.ChangeState(GetComponent<FallingState>());
        }
    }
}
