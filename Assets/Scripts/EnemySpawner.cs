using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Ajustes de Meteoritos")]
    public GameObject[] enemyPrefabs; 
    public float spawnRadius = 12f; 

    [Header("Dificultad Progresiva")]
    public float initialSpawnRate = 2f;
    public float minSpawnRate = 0.5f;
    public float difficultyFactor = 0.05f;
    
    private float currentSpawnRate;

    void Start()
    {
        currentSpawnRate = initialSpawnRate;
        StartCoroutine(SpawnRoutine());
    }

    System.Collections.IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnEnemy();
            
            currentSpawnRate = Mathf.Max(minSpawnRate, currentSpawnRate - difficultyFactor);
            
            yield return new WaitForSeconds(currentSpawnRate);
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0) return;

        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        Vector2 spawnPos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * spawnRadius;

        Instantiate(enemyPrefabs[randomIndex], spawnPos, Quaternion.Euler(0, 0, Random.Range(0, 360)));
    }
}