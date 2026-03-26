using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;
    public GameState activeState;
    public GameState[] allStates;

    private void Start()
    {
        StateTransition(allStates[0]);
    }

    public bool StateTransition(GameState nextState) 
    {
        if (activeState != null)
            activeState.EndState();
        activeState = nextState;
        activeState.StartState();
        return true;
    }
    private void Update()
    {
        if (activeState && !isPaused)
        {
            activeState.UpdateState();
        }
    }

}
