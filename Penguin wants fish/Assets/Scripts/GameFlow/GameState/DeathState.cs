using UnityEngine;

public class DeathState : BaseState
{   
    //public float KnockBackForce;
    [SerializeField] private Vector3 KnockOutForce =  new Vector3(0,4,-3);
    private Vector3 currentKnockback;
    public override void Construct()
    {
        motor.ani?.SetTrigger("Death");
        currentKnockback= KnockOutForce;
    }
    public override Vector3 ProcessMotion()
    {
       //OrignalForce();
       Vector3 m= currentKnockback;
        currentKnockback = new Vector3(0,
            currentKnockback.y -= motor.gravity * Time.deltaTime,
            currentKnockback.z += 2.0f * Time.deltaTime);
       
        if(currentKnockback.z>0)
        {
            currentKnockback.z=0;
            GameManager.Instance.ChangeSate(GameManager.Instance.GetComponent<GameStateDeath>());
        }
       // knockforce();
        return KnockOutForce;

    }
   

}
