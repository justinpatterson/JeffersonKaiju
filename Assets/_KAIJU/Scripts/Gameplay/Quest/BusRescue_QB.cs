using System;
using UnityEngine;

public class BusRescue_QB : QuestBehavior
{
    public Transform startZone;
    public Transform endZone;
    public GrabObjectBehavior grabTarget;
    public float completionThresholdDistance = 0.1f;
    private void Awake()
    {
        StartQuest();
    }
    public override void StartQuest()
    {   
        grabTarget.transform.position = startZone.transform.position;
        grabTarget.transform.rotation = startZone.transform.rotation;
        grabTarget.ReportGrab.AddListener(GrabListener);
        grabTarget.ReportRelease.AddListener(ReleaseListener);
        grabTarget.ReportDeath.AddListener(DeathListener);

        endZone.gameObject.SetActive(false);

        base.StartQuest();
    }

    private void DeathListener()
    {
    }

    private void ReleaseListener()
    {
    }

    private void GrabListener()
    {
        endZone.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (questActive) 
        {
            if (EvaluateQuestSuccess())
            {
                questSuccess = true;
                EndQuest();
            }
            else if (EvaluateQuestFailure())
            {
                questSuccess = false;
                EndQuest();
            }
        }
    }

    public override bool EvaluateQuestSuccess()
    {
        return Vector3.Distance(grabTarget.transform.position, endZone.position) < completionThresholdDistance;
    }
}
