using UnityEngine;

public class CityHealthHelper : MonoBehaviour
{
    public GameplayData dataTarget;
    public int healthAmount;

    public void TriggerHelper() 
    {
        dataTarget.DamageCity(healthAmount);
    }
}
