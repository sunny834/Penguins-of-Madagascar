using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingState : BaseState
{
    public float slideDuration = 1.0f;

    //collider logic

    private Vector3 initialCenter;
    private float initialSize;
    private float slideStart;

    public override void Construct()
    {
        motor.ani?.SetTrigger("Slide");
        slideStart = Time.time; 
        initialCenter = motor.controller.center;
        initialSize=motor.controller.height;

        motor.controller.center = initialCenter*0.5f;
        motor.controller.height = initialSize*0.5f;
    }
    public override void Destruct()
    {
        motor.controller.height = initialSize;
        motor.controller.center = initialCenter;
        motor.ani?.SetTrigger("Running");
    }
    public override void Transition()
    {
        if(InputManager.Instance.SwipeLeft)
        {
            motor.ChangeLane(-1);
        }
        if(InputManager.Instance.SwipeRight)
            { motor.ChangeLane(1); }

        if(!motor.isGrounded)
            motor.ChangeState(GetComponent<FallingState>());
        if (InputManager.Instance.SwipeUp)
        {
            motor.ChangeState(GetComponent<JumpingState>());
        }
        if(Time.time - slideStart > slideDuration)
            motor.ChangeState(GetComponent<RunningState>());    
       
    }
    public override Vector3 ProcessMotion()
    {
        Vector3 m =Vector3.zero;

        m.x = motor.SnapToLane();
        m.y = -1.0f;
        m.z = motor.baseRunSpeed;
         return m;
    }

}
