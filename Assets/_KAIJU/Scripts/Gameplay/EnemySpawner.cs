using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] enemyPrefabSet;
    public float nextSpawnCheck = 5f;
    public List<GameObject> currentEnemyList = new List<GameObject>();
    public int maxEnemiesSpawned = 3;

    private void Update()
    {
        nextSpawnCheck = nextSpawnCheck - Time.deltaTime;
        if(nextSpawnCheck <= 0) 
        {
            if(currentEnemyList.Count >= maxEnemiesSpawned) { }
            else 
            {
                GameObject nextSpawnPoint = SelectNextSpawnPoint();
                GameObject nextSpawnEnemy = SelectNextEnemyPrefab();
                if (nextSpawnPoint != null && nextSpawnEnemy != null) 
                {
                    GameObject newEnemy = SpawnNewEnemy(nextSpawnEnemy, nextSpawnPoint);
                    currentEnemyList.Add(newEnemy);
                    Debug.Log("Spawning " + nextSpawnEnemy.name);

                    newEnemy.GetComponent<EnemyBehavior>().EnableEnemy();

                }
            }
            nextSpawnCheck = 5f;
        }


    }

    protected GameObject SpawnNewEnemy(GameObject enemy, GameObject spawnLocation)
    {
        GameObject enemyInstance = Instantiate(enemy, spawnLocation.transform.position, Quaternion.identity);

        return enemyInstance;
    }
    protected GameObject SelectNextEnemyPrefab() 
    {
        int randomIndex = Random.Range(0, enemyPrefabSet.Length);
        return enemyPrefabSet[randomIndex];
    }
    protected GameObject SelectNextSpawnPoint()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex];

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach(GameObject s in spawnPoints) 
        {
            Gizmos.DrawWireSphere(s.transform.position, 1f);
        }
    }

}
