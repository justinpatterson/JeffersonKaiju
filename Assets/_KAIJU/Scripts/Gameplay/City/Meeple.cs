using UnityEngine;

public class Meeple : MonoBehaviour
{
    public enum MeepleState { Idle, Reserved, Abducting, Abducted, Dead }

    public MeepleState state = MeepleState.Idle;

    public Transform homePoint;
    public Transform currentTarget;

    public float beamLerpSpeed = 1f;
    private float lerpTime;

    public bool IsAbductable => state == MeepleState.Idle || state == MeepleState.Reserved;

    public delegate void MeepleStateChange();
    public MeepleStateChange OnMeepleStateChanged;

    public void SetHome(Transform home)
    {
        homePoint = home;
        transform.SetPositionAndRotation(home.position, home.rotation);
    }

    public void BeginReservation()
    {
        if (state != MeepleState.Idle) return;
        state = MeepleState.Reserved;
        OnMeepleStateChanged?.Invoke();
    }

    public void EndReservation()
    {
        if (state != MeepleState.Reserved) return;
        state = MeepleState.Idle;
        OnMeepleStateChanged?.Invoke();
    }

    public void BeginAbduction()
    {
        Debug.Log("STATE IS NOW ABDUCTING ON MEEPLE...");
        state = MeepleState.Abducting;
        OnMeepleStateChanged?.Invoke();
        lerpTime = 0f;
    }

    public void UpdateAbduction(Transform shipPoint, float dt)
    {
        if (state != MeepleState.Abducting || shipPoint == null) return;

        currentTarget = shipPoint;
        lerpTime += dt * beamLerpSpeed;
        transform.position = Vector3.Lerp(homePoint.position, shipPoint.position, lerpTime);
        transform.rotation = Quaternion.Slerp(homePoint.rotation, shipPoint.rotation, lerpTime);
    }

    public void CompleteAbduction()
    {
        state = MeepleState.Abducted;
        OnMeepleStateChanged?.Invoke();
        gameObject.SetActive(false);
    }

    public void AbortAbduction()
    {
        state = MeepleState.Idle;
        OnMeepleStateChanged?.Invoke();

        if (homePoint != null)
            transform.SetPositionAndRotation(homePoint.position, homePoint.rotation);
    }
}