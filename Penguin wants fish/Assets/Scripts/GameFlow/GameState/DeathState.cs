using UnityEngine;

public class DeathState : BaseState
{
    [SerializeField] private Vector3 KnockOutForce =  new Vector3(0,4,-3);
    public override void Construct()
    {
        motor.ani?.SetTrigger("Death");
    }
    public override Vector3 ProcessMotion()
    {
       Vector3 m=KnockOutForce;
        KnockOutForce = new Vector3(0,
            KnockOutForce.y -= motor.gravity * Time.deltaTime,
            KnockOutForce.z += 2.0f * Time.deltaTime);
       
        if(KnockOutForce.z>0)
        {
            KnockOutForce.z=0;
            GameManager.Instance.ChangeSate(GameManager.Instance.GetComponent<GameStateDeath>());
        }
        return KnockOutForce;


    }
}
