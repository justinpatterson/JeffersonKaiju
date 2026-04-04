using UnityEngine;

public class TractorBeamController : MonoBehaviour
{
    public LineRenderer myLineRenderer;
    public bool isActive;
    [SerializeField]
    Transform startTarget, endTarget;

    public float lastWarbleVal = 0f;
    public void StartTractorBeam(Transform start, Transform end) 
    {
        startTarget = start;
        endTarget = end;
        myLineRenderer.SetPosition(0, startTarget.position);
        myLineRenderer.SetPosition(1, startTarget.position);
        myLineRenderer.enabled = true;

    }
    public void UpdateTractorBeam() 
    {
        myLineRenderer.SetPosition(0, startTarget.position);
        myLineRenderer.SetPosition(1, Vector3.Lerp(myLineRenderer.GetPosition(1), endTarget.position, Time.deltaTime * 5f));
        
        
        float nextVal = Mathf.Lerp(-0.5f, 0.5f, Time.time % 1f);
        lastWarbleVal = nextVal;
        myLineRenderer.material.mainTextureOffset = new Vector2(0f, nextVal);
    }
    public void StopTractorBeam() 
    {

        myLineRenderer.enabled = false;
    }
}
