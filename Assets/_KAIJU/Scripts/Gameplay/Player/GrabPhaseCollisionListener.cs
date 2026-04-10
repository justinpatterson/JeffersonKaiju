using System;
using UnityEngine;

public class GrabPhaseCollisionListener : MonoBehaviour
{
    [System.Serializable]
    public struct GrabCollisionInfo
    {
        public KaijuGripPhaseBehavior.GripPhase phase;
        public Collider collider;
    }
    [SerializeField]
    public GrabCollisionInfo[] collisionInfo;
    public KaijuGripPhaseBehavior targetGripPhaseBehavior;


    private void Awake()
    {
        if (targetGripPhaseBehavior != null)
            targetGripPhaseBehavior.OnGripPhaseChanged.AddListener(GripPhaseListener);
    }

    private void GripPhaseListener(KaijuGripPhaseBehavior.GripPhase phase)
    {
        Debug.Log("Phase is " + phase);
        RefreshCollidersForPhase(phase);

    }
    void RefreshCollidersForPhase(KaijuGripPhaseBehavior.GripPhase phase) 
    {
        foreach(GrabCollisionInfo c in collisionInfo) 
        {
            c.collider.enabled = (phase == c.phase);
        }
    }
}
