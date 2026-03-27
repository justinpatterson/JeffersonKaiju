using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MeepleVariant
{
    public GameObject prefab;
    [Range(0f, 1f)] public float spawnWeight = 1f;
}

public class MeepleSpawner : MonoBehaviour
{
    public MeepleVariant[] meepleVariants;
    public Transform[] roofSpawnPoints;
    [Range(1, 10)] public int minMeeples = 1;
    [Range(1, 10)] public int maxMeeples = 10;

    public readonly List<Meeple> spawnedMeeples = new List<Meeple>();

    
    public void SpawnMeeples()
    {
        ClearMeeples();
        if (meepleVariants == null || meepleVariants.Length == 0) return;

        int count = Random.Range(minMeeples, maxMeeples + 1);
        for (int i = 0; i < count; i++)
        {
            GameObject prefab = PickWeightedVariant();
            if (prefab == null) continue;

            Transform p = roofSpawnPoints[Random.Range(0, roofSpawnPoints.Length)];
            GameObject obj = Instantiate(prefab, p.position, p.rotation, transform);

            Meeple m = obj.GetComponent<Meeple>();
            if (m != null)
            {
                m.SetHome(p);
                spawnedMeeples.Add(m);
            }
        }
    }

    private GameObject PickWeightedVariant()
    {
        float totalWeight = 0f;
        foreach (var v in meepleVariants) totalWeight += v.spawnWeight;

        float pick = Random.Range(0f, totalWeight);
        foreach (var v in meepleVariants)
        {
            if (pick < v.spawnWeight) 
                return v.prefab;
            pick -= v.spawnWeight;
        }
        return meepleVariants[0]?.prefab;
    }


    public void ClearMeeples()
    {
        for (int i = spawnedMeeples.Count - 1; i >= 0; i--)
        {
            if (spawnedMeeples[i] != null) Destroy(spawnedMeeples[i].gameObject);
        }
        spawnedMeeples.Clear();
    }
}