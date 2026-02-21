using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public enum BehaviorState { Idle, Roaming, Abduct, GoTo, Dodge }
    public BehaviorState state;
    protected virtual BehaviorState PickNextBehavior() 
    {
        return BehaviorState.Idle;
    }
    protected virtual void StartBehavior() { }
    protected virtual void UpdateBehavior() { }
    protected virtual void StopBehavior() { }


}
