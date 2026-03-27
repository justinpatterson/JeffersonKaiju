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

    [Header("Data References")]
    public GameplayData cityDataRef;

    [Header("Abduction")]
    public float abductionDuration = 8f;           // How long beam runs
    public float abductionReachDistance = 15f;     // How close to start
    public Transform abductionBeamPoint;           // Visual beam endpoint

    private Building targetBuilding;
    private Meeple targetMeeple;
    private bool hasValidTarget;
    public Vector3 targetDirection;  // New: for GoTo/Abduct targeting


    protected override BehaviorState PickNextBehavior()
    {
        if (FindAbductionTarget())
            return BehaviorState.GoTo;

        int randomBehavior = Random.Range(0, 3);
        return (BehaviorState)randomBehavior;
    }
    private bool FindAbductionTarget()
    {
        
        Building[] buildings = FindObjectsOfType<Building>();
        float closestDist = float.MaxValue;
        Building best = null;

        foreach (var b in buildings)
        {
            if (b.abductionHandler == null || !b.abductionHandler.HasAvailableMeeple()) continue;

            float dist = Vector3.Distance(transform.position, b.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                best = b;
            }
        }

        targetBuilding = best;
        return best != null;
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
                stateTransitionTimer = abductionDuration;  // Just set timer
                break;
            case BehaviorState.GoTo:
                if (targetBuilding != null)
                {
                    targetDirection = (targetBuilding.transform.position - transform.position).normalized;
                }
                break;
            case BehaviorState.Dodge:
                break;
            case BehaviorState.Dead:
                break;
            case BehaviorState.Entry:
                groundOffsetPivot.localPosition = Vector3.up * 10f;
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


        switch (state)
        {
            case BehaviorState.Entry:
                stateTransitionTimer -= Time.deltaTime * 2f;
                float percentage = (stateTransitionTimer / 5f);
                float nextYPosition = baseGroundOffset + ((10f - baseGroundOffset) * percentage);
                groundOffsetPivot.localPosition = Vector3.up * nextYPosition;
                if(stateTransitionTimer <= 0f)
                {
                    StateTimerBehavior();

                }
                break;
            case BehaviorState.Idle:

                HoverSinOffset();
                StateTimerBehavior();
                break;
            case BehaviorState.Roaming:

                HoverSinOffset();
                transform.position = transform.position + roamingDirection.normalized * roamingSpeed * Time.deltaTime;
                StateTimerBehavior();
                break;
            case BehaviorState.Abduct:
                HoverSinOffset();

                if (targetMeeple != null)
                {
                    targetMeeple.UpdateAbduction(abductionBeamPoint, Time.deltaTime);

                    if (targetMeeple.state == Meeple.MeepleState.Abducted)
                    {
                        cityDataRef?.DamageCity(2);
                        if (targetBuilding != null)
                            targetBuilding.abductionHandler.CompleteAbduction(targetMeeple);
                        targetMeeple = null;
                    }
                }
                StateTimerBehavior();
                break;
            case BehaviorState.GoTo:
                HoverSinOffset();
                transform.position += targetDirection * roamingSpeed * Time.deltaTime;

                if (targetBuilding != null &&
                    Vector3.Distance(transform.position, targetBuilding.transform.position) < abductionReachDistance)
                {
                    if (targetBuilding.TryReserveMeeple(out targetMeeple))
                    {
                        state = BehaviorState.Abduct;
                        StartBehavior();
                    }
                    else
                    {
                        StateTimerBehavior();
                    }
                }
                else
                {
                    StateTimerBehavior();  // Fallback if no target or too far
                }
                break;
            case BehaviorState.Dodge:
                StateTimerBehavior();
                break;
            case BehaviorState.Dead:
                break;
        }


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
        if (targetBuilding != null && targetMeeple != null)
        {
            targetBuilding.abductionHandler.AbortAbduction(targetMeeple);
            targetMeeple = null;
        }
        rb.useGravity = true;
        grabRef.forceGravityOnDetach = true;
        base.DisableEnemy();
    }

    public override void EnableEnemy()
    {
        base.EnableEnemy();
        state = BehaviorState.Entry;
        StartBehavior();
    }

    private void OnDestroy()
    {
        cityDataRef.ReportEnemyDefeated();
    }
}
