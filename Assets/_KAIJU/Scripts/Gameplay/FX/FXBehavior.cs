using System.Collections;
using UnityEngine;

public class FXBehavior : MonoBehaviour
{
    public float lifetime = 5f;
    public bool killOnEnd = true;

    private void Awake()
    {
        StartCoroutine(DoFX());
    }

    IEnumerator DoFX() 
    {
        yield return new WaitForSeconds(lifetime);
        if (killOnEnd)
            Destroy(this.gameObject);
    }
}
