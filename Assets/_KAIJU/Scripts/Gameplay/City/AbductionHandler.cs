using System.Collections.Generic;
using UnityEngine;

public class AbductionHandler : MonoBehaviour
{
    public MeepleSpawner spawner;
    private readonly HashSet<Meeple> reserved = new HashSet<Meeple>();
    [SerializeField]
    private Meeple activeTarget;

    private void Awake()
    {
        if (spawner == null) spawner = GetComponent<MeepleSpawner>();
    }
    public bool HasAvailableMeeple()
    {
        return spawner != null && activeTarget == null;  // Only if not busy
    }
    /*
    public bool HasAvailableMeeple()
    {
        if (spawner == null) return false;

        for (int i = 0; i < spawner.spawnedMeeples.Count; i++)
        {
            var m = spawner.spawnedMeeples[i];
            if (m != null && m.IsAbductable && !reserved.Contains(m)) return true;
        }

        return false;
    }
    */

    public bool TryReserveMeeple(out Meeple meeple)
    {
        meeple = null;
        if (activeTarget != null)
        {
            return false;
        }
        if (spawner == null) return false;

        for (int i = 0; i < spawner.spawnedMeeples.Count; i++)
        {
            var m = spawner.spawnedMeeples[i];
            if (m == null || !m.IsAbductable || reserved.Contains(m)) continue;

            reserved.Add(m);
            activeTarget = m;
            m.BeginReservation();
            meeple = m;
            return true;
        }

        return false;
    }

    public void CancelReservation(Meeple meeple)
    {
        if (meeple == null) return;

        if (reserved.Remove(meeple))
        {
            if (activeTarget == meeple) activeTarget = null;
            meeple.EndReservation();
        }
    }

    public void BeginAbduction(Meeple meeple)
    {
        Debug.Log("Abductor instruction received...");
        if (meeple == null) return;
        Debug.Log("Meeple not null...");
        activeTarget = meeple;
        meeple.BeginAbduction();
    }

    public void CompleteAbduction(Meeple meeple)
    {
        if (meeple == null) return;

        reserved.Remove(meeple);
        if (activeTarget == meeple) activeTarget = null;
        meeple.CompleteAbduction();
    }

    public void AbortAbduction(Meeple meeple)
    {
        if (meeple == null) return;

        reserved.Remove(meeple);
        if (activeTarget == meeple) activeTarget = null;
        meeple.AbortAbduction();
    }
}