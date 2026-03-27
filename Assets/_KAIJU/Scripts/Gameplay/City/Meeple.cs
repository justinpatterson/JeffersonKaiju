using UnityEngine;

public class Meeple : MonoBehaviour
{
    public enum MeepleState { Idle, Reserved, Abducting, Abducted, Dead }

    public MeepleState state = MeepleState.Idle;

    public Transform homePoint;
    public Transform currentTarget;

    public float beamLerpSpeed = 1f;
    private float beamT;

    public bool IsAbductable => state == MeepleState.Idle || state == MeepleState.Reserved;

    public void SetHome(Transform home)
    {
        homePoint = home;
        transform.SetPositionAndRotation(home.position, home.rotation);
    }

    public void BeginReservation()
    {
        if (state != MeepleState.Idle) return;
        state = MeepleState.Reserved;
    }

    public void EndReservation()
    {
        if (state != MeepleState.Reserved) return;
        state = MeepleState.Idle;
    }

    public void BeginAbduction()
    {
        state = MeepleState.Abducting;
        beamT = 0f;
    }

    public void UpdateAbduction(Transform shipPoint, float dt)
    {
        if (state != MeepleState.Abducting || shipPoint == null) return;

        currentTarget = shipPoint;
        beamT += dt * beamLerpSpeed;
        transform.position = Vector3.Lerp(homePoint.position, shipPoint.position, beamT);
        transform.rotation = Quaternion.Slerp(homePoint.rotation, shipPoint.rotation, beamT);
    }

    public void CompleteAbduction()
    {
        state = MeepleState.Abducted;
        gameObject.SetActive(false);
    }

    public void AbortAbduction()
    {
        state = MeepleState.Idle;

        if (homePoint != null)
            transform.SetPositionAndRotation(homePoint.position, homePoint.rotation);
    }
}