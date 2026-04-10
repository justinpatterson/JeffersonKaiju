using UnityEngine;
using UnityEngine.Events;

public class GrabObjectBehavior : MonoBehaviour
{
    public bool isGrabbed;
    Transform _followTarget;
    KaijuGripPhaseBehavior _targetGripBehavior;

    public UnityEvent ReportGrab;
    public UnityEvent ReportRelease;
    public UnityEvent ReportDeath;

    private void FixedUpdate()
    {
        if(_targetGripBehavior != null && isGrabbed) 
        {
            switch (_targetGripBehavior.phase)
            {
                case KaijuGripPhaseBehavior.GripPhase.None:
                case KaijuGripPhaseBehavior.GripPhase.Released:
                    //SHOULD RELEASE
                    Debug.Log("Ungrab me, Semour!");
                    isGrabbed = false;
                    _followTarget = null;
                    _targetGripBehavior = null;
                    transform.SetParent(null);
                    ReportRelease?.Invoke();
                    break;
                case KaijuGripPhaseBehavior.GripPhase.Soft:
                    break;
                case KaijuGripPhaseBehavior.GripPhase.Hard:
                    //SHOULD SQUISH
                    isGrabbed = false;
                    _followTarget = null;
                    _targetGripBehavior = null;
                    gameObject.SetActive(false);
                    transform.SetParent(null);
                    ReportDeath?.Invoke();
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("A trigger entered me");
        if (collision.gameObject.CompareTag("Grab_SOFT")) 
        {
            KaijuGripPhaseBehavior kgpb = collision.transform.parent.GetComponent<KaijuGripPhaseBehavior>();
            _targetGripBehavior = kgpb;


            Debug.Log("Grab me, Semour!");
            isGrabbed = true;
            _followTarget = collision.transform;
            transform.SetParent(_followTarget);
            ReportGrab?.Invoke();
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        Debug.Log("Exit heard!!!!");
        //TODO: WHEN SWITCHING TO "HARD GRAB" IT WILL ALSO CALL THIS GRAB SOFT RELEASE IF WE CARE
        if (collision.gameObject.CompareTag("Grab_SOFT") && isGrabbed && _followTarget == collision.transform)
        {
            Debug.Log("Ungrab me, Semour!");
            isGrabbed = false;
            _followTarget = null;
            transform.SetParent(null);
            ReportRelease?.Invoke();
        }
    }
}
