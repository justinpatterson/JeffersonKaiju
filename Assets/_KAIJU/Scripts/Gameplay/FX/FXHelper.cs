using UnityEngine;

public class FXHelper : MonoBehaviour
{
    public GameObject FXPrefab;
    public Transform spawnTarget;
    public void TriggerFX() 
    {
        Instantiate(FXPrefab, spawnTarget ? spawnTarget.position : transform.position, spawnTarget ? spawnTarget.rotation:transform.rotation);
    }
}
