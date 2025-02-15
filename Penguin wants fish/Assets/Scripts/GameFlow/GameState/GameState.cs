using System.Reflection.Emit;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    protected GameManager brain;
    private void Awake()
    {
        brain = GetComponent<GameManager>();
    }
    public virtual void Construct()
    {
        Debug.Log("Contructing:" + this.ToString());
    }
    public virtual void Destruct()
    {

    }
    public virtual void UpdateState()
    {

    }
}
