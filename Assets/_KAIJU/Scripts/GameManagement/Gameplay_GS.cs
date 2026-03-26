using UnityEngine;

public class Gameplay_GS : GameState
{
    public GameplayData data;

    public override void StartState()
    {
        base.StartState();
        data.ResetData();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(data.EnemiesRemaining <= 0) 
        {
            //transition to Results Screen or something
        }
        else 
        {
            if(data.CityHealth <= 0) 
            {
                //transition to Results Screen or something
            }
            else 
            {
                //update jumbotrons
                //maybe tell spawners to do stuff
                //whatever
            }
        }

    }
}
