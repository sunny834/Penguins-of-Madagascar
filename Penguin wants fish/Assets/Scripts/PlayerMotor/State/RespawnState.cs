using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnState : BaseState
{
    [SerializeField] private float SpawnDistance = 25f;
    public override void Construct()
    {
       motor.transform.position = new Vector3(0, SpawnDistance,motor.transform.position.z);
    }
}
