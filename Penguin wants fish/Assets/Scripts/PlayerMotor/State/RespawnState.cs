using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnState : BaseState
{
    [SerializeField] private float SpawnDistance = 25f;
    [SerializeField] private float RespawnTime = 1f;

    private float startTime;
    public override void Construct()
    {
        startTime=Time.time;
        motor.controller.enabled = false;
        motor.transform.position = new Vector3(0, SpawnDistance,motor.transform.position.z);
        motor.controller.enabled=true;

        motor.verticalVelocity = 0.0f;
        motor.currentLane = 0;
        motor.ani?.SetTrigger("Respawn");
     
    }
    public override void Destruct()
    {
        GameManager.Instance.ChangeCamera(GameManager.GameCamera.Game);
    }
    public override Vector3 ProcessMotion()
    {
        //Apply gravity 
        motor.ApplyGravity();

        //create our return vector
        Vector3 m=Vector3.zero;
        m.x = motor.SnapToLane();
        m.y=motor.verticalVelocity;
        m.z=motor.baseRunSpeed;

        return m;
    }
    public override void Transition()
    {
        if (motor.isGrounded && (Time.time - startTime)> RespawnTime) 
        {
            motor.ChangeState(GetComponent<RunningState>());
        }
        if (InputManager.Instance.SwipeLeft)
            motor.ChangeLane(-1);
        if (InputManager.Instance.SwipeRight) motor.ChangeLane(1);
    }

}
