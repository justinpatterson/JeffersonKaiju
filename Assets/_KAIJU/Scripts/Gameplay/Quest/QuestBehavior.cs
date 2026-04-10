using UnityEngine;

public class QuestBehavior : MonoBehaviour
{
    public bool questActive = false;
    public bool questSuccess = false;
    public virtual void StartQuest() 
    {
        questActive = true;
    }
    public virtual void EndQuest() 
    {
        questActive = false;
    }
    public virtual bool EvaluateQuestSuccess() { return false; }
    public virtual bool EvaluateQuestFailure() { return false; }
}
