using System;
using UnityEngine;

public class MeepleAnimationBehavior : MonoBehaviour
{
    public Animator meepleAnimatorReference;
    public Meeple meepleReference;

    private void Awake()
    {
        if(meepleAnimatorReference != null && meepleReference != null) 
        {
            meepleReference.OnMeepleStateChanged += MeepleStateListener;
        }
    }

    private void MeepleStateListener()
    {
        switch (meepleReference.state)
        {
            case Meeple.MeepleState.Idle:
                meepleAnimatorReference.SetBool("IsInDanger", false);
                break;
            case Meeple.MeepleState.Reserved:
                meepleAnimatorReference.SetBool("IsInDanger", false);
                break;
            case Meeple.MeepleState.Abducting:
                meepleAnimatorReference.SetBool("IsInDanger", true);
                break;
            case Meeple.MeepleState.Abducted:
                meepleAnimatorReference.SetBool("IsInDanger", false);
                break;
            case Meeple.MeepleState.Dead:
                meepleAnimatorReference.SetBool("IsInDanger", false);
                break;
        }
    }
}
