using UnityEngine;

public class Spaceship_EB : EnemyBehavior
{
    public float stateTransitionTimer = 5f;
    public Transform spaceShipPivot;
    public Transform groundOffsetPivot;
    public float baseGroundOffset = 1.5f;
    [Range(0,5f)]
    public float sinFactor = 0.25f;
    [Range(0, 5f)]
    public float sinSpeed = 0.5f;

    public Rigidbody rb;
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabRef;

    public Vector3 roamingDirection = Vector3.zero;
    public float roamingSpeed = 1f;

    protected override BehaviorState PickNextBehavior()
    {
        int randomBehavior = Random.Range(0, 2);
        BehaviorState randomState = (BehaviorState) randomBehavior;
        return randomState;
    }
    protected override void StartBehavior()
    {
        base.StartBehavior();
        stateTransitionTimer = 5f;

        switch (state)
        {
            case BehaviorState.Idle:
                break;
            case BehaviorState.Roaming:
                roamingDirection = new Vector3
                (
                    Random.Range(-1f, 1f),
                    0f,
                    Random.Range(-1f,1f)
                );

                break;
            case BehaviorState.Abduct:
                break;
            case BehaviorState.GoTo:
                break;
            case BehaviorState.Dodge:
                break;
            case BehaviorState.Dead:
                break;
        }

    }
    protected override void StopBehavior()
    {
        base.StopBehavior();
    }
    protected override void UpdateBehavior()
    {
        base.UpdateBehavior();
        
        if (state == BehaviorState.Dead)
            return;

        HoverSinOffset();

        switch (state)
        {
            case BehaviorState.Idle:
                break;
            case BehaviorState.Roaming:
                transform.position = transform.position + roamingDirection.normalized * roamingSpeed * Time.deltaTime;
                break;
            case BehaviorState.Abduct:
                break;
            case BehaviorState.GoTo:
                break;
            case BehaviorState.Dodge:
                break;
            case BehaviorState.Dead:
                break;
        }


        StateTimerBehavior();
    }

    void HoverSinOffset() 
    {
        float currentTime = Time.time;
        float currentSin = Mathf.Sin(currentTime * sinSpeed);
        float currentYHeight = (currentSin * sinFactor) + baseGroundOffset;
        groundOffsetPivot.localPosition = Vector3.up * currentYHeight;
    }
    void StateTimerBehavior() 
    {
        stateTransitionTimer -= Time.deltaTime;
        if (stateTransitionTimer < 0)
        {
            StopBehavior();
            state = PickNextBehavior();
            StartBehavior();
        }
    }

    public override void DisableEnemy()
    {
        if (state == BehaviorState.Dead)
            return;

        base.DisableEnemy();
        rb.useGravity = true;
        grabRef.forceGravityOnDetach = true;
    }
}
