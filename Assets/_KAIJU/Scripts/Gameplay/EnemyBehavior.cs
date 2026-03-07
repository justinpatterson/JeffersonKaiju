using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public enum BehaviorState { Idle = 0, Roaming = 1, Abduct = 2, GoTo = 3, Dodge = 4, Entry = 5, Dead = -1}
    public BehaviorState state;
    protected virtual BehaviorState PickNextBehavior() 
    {
        return BehaviorState.Idle;
    }
    protected virtual void StartBehavior() 
    {
        
    }
    protected virtual void UpdateBehavior() { }
    protected virtual void StopBehavior() { }

    private void Start()
    {
        //state = PickNextBehavior();
        //StartBehavior();
    }
    private void Update()
    {
        UpdateBehavior();
    }
    public virtual void DisableEnemy() 
    {
        StopBehavior();
        state = BehaviorState.Dead;

        StartBehavior();
    }

    public virtual void EnableEnemy() 
    {
    
    }

}
