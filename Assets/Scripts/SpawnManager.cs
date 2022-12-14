using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private float _enemySpawnDelay = 5.0f;
    [SerializeField]
    private GameObject[] powerups; 
    [SerializeField]
    private EnemyBehaviour[] enemys;
    private bool _stopSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            int randomEnemy = Random.Range(0, enemys.Length);
            EnemyBehaviour newEnemy = Instantiate(enemys[randomEnemy], posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_enemySpawnDelay);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(Random.Range(3, 7));    
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 6.5f, 0);
            int randomPowerup = Random.Range(0, powerups.Length);
            Instantiate(powerups[randomPowerup], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 7));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
