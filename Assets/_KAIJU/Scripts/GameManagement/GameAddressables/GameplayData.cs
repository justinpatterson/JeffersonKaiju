using UnityEngine;
// Use the CreateAssetMenu attribute to allow creating instances of this ScriptableObject from the Unity Editor.
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameplayData", order = 1)]
public class GameplayData : ScriptableObject
{
    public int CityHealth = 100;
    public int EnemiesRemaining = 10;

    public void ResetData() 
    {
        CityHealth = 100;
        EnemiesRemaining = 10;
    }

    public void DamageCity(int amt) 
    {
        CityHealth -= amt;
        CityHealth = Mathf.Clamp(CityHealth, 0, 100);
    }
    public void ReportEnemyDefeated() 
    {
        EnemiesRemaining--;
        EnemiesRemaining = Mathf.Clamp(EnemiesRemaining, 0, 10);
    }
}