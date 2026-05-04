using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUpPrefabs;
    public float minDistance = 4f;
    public float maxDistance = 8f;
    public float spawnInterval = 10f;

    void Start()
    {
        InvokeRepeating("SpawnPowerUp", 5f, spawnInterval);
    }

    void SpawnPowerUp()
    {
        if (powerUpPrefabs.Length == 0) return;

        int index = Random.Range(0, powerUpPrefabs.Length);

        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float distance = Random.Range(minDistance, maxDistance);

        Vector3 spawnPos = new Vector3(
            Mathf.Cos(angle) * distance,
            Mathf.Sin(angle) * distance,
            0
        );

        Instantiate(powerUpPrefabs[index], spawnPos, Quaternion.identity);
    }
}