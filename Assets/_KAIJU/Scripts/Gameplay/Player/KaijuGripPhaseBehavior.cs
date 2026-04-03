using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;
using UnityEngine.Events;
public class KaijuGripPhaseBehavior : MonoBehaviour
{
    public enum GripPhase { None, Soft, Hard, Released }
    public GripPhase phase = GripPhase.None;
    [System.Serializable]
    public struct GripRangeSetting 
    {
        public GripPhase phase;
        public float min, max;
        public bool InRange(float val) 
        {
            return val > min && val <= max;
        }
    }
    [SerializeField]
    public GripRangeSetting[] GripRanges;

    [SerializeField]
    XRInputValueReader<float> m_TriggerInput = new XRInputValueReader<float>("Trigger");

    public UnityEvent OnGripPhaseChanged;
    private void Update()
    {

        float currVal = GetGripPercentage();
        GripPhase nextGripPhase = GripPhase.None;
        for (int i = 0; i < GripRanges.Length; i++) 
        {
            bool isInRange = GripRanges[i].InRange(currVal);
            if (isInRange) 
            {
                nextGripPhase = GripRanges[i].phase;
            }
        }
        if(nextGripPhase != phase) 
        {
            phase = nextGripPhase;
            OnGripPhaseChanged?.Invoke() ;
        }

    }

    public float GetGripPercentage() 
    {
        float val = m_TriggerInput.ReadValue();
        return val;
    }
}
