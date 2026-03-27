using UnityEngine;

public class Building : MonoBehaviour
{
    public MeepleSpawner meepleSpawner;
    public AbductionHandler abductionHandler;

    private void Awake()
    {
        if (meepleSpawner == null) meepleSpawner = GetComponent<MeepleSpawner>();
        if (abductionHandler == null) abductionHandler = GetComponent<AbductionHandler>();
    }
    private void Start()
    {
        if (meepleSpawner)
            meepleSpawner.SpawnMeeples();
    }

    public bool TryReserveMeeple(out Meeple meeple)
    {
        meeple = null;
        return abductionHandler != null && abductionHandler.TryReserveMeeple(out meeple);
    }

    public void CancelReservation(Meeple meeple)
    {
        if (abductionHandler != null) abductionHandler.CancelReservation(meeple);
    }
}