using UnityEngine;

public class InteractionBehaviorSet : MonoBehaviour
{
    public float targetScale = 1f;
    public float scaleSpeed = 2f;
    public void SetTargetScale(float t) 
    {  
        targetScale = t; 
    }

    public void Shrink() 
    {
        targetScale = 0.5f;
    }
    public void Grow() 
    {
        targetScale = 1f;
    }

    private void FixedUpdate()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * targetScale, Time.deltaTime * scaleSpeed);
    }
}
