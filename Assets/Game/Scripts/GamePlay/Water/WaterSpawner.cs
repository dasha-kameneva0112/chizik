using UnityEngine;

public class WaterSpawner : MonoBehaviour
{
    public GameObject waterDropPrefab;
    public float spawnRate = 0.001f; // УВЕЛИЧЬТЕ частоту (меньше число = чаще)
    public int dropsPerSpawn = 5; // Капель за один раз

    void Start()
    {
        InvokeRepeating("SpawnWater", 0f, spawnRate);
    }

    void SpawnWater()
    {
        for (int i = 0; i < dropsPerSpawn; i++)
        {
            // Небольшой разброс в позиции для натуральности
            Vector2 randomOffset = new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
            Vector2 spawnPosition = (Vector2)transform.position + randomOffset;

            Instantiate(waterDropPrefab, spawnPosition, Quaternion.identity);
        }
    }
}