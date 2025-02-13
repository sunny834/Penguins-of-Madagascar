using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : BaseState
{
    public override void Construct()
    {
        motor.verticalVelocity = 0;
    }
    public override Vector3 ProcessMotion()
    {
        Vector3 motion = Vector3.zero;
        motion.x = motor.SnapTolane();
        motion.y = -1.0f;
        motion.z = motor.baseRunSpeed;

        return motion;  
    }

    public override void Transition()
    {
        if (InputManager.Instance.SwipeLeft)
        {
            motor.ChangeLane(-1);
        }

        if (InputManager.Instance.SwipeRight)
        {
            motor.ChangeLane(1);
        }
        if (InputManager.Instance.SwipeUp && motor.isGrounded)
        {
             motor.ChangeState(GetComponent<JumpingState>());
        }
        if (!motor.isGrounded)
        {
            motor.ChangeState(GetComponent<FallingState>());
        }
        if(InputManager.Instance.SwipeDown)
        {
            motor.ChangeState(GetComponent<SlidingState>());
        }
    }
}
