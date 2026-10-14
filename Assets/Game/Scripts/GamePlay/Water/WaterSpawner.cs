using UnityEngine;

public class WaterSpawner : MonoBehaviour
{
    public GameObject waterDropPrefab;
    private float spawnRate = 0.1f; //частота

    private int totalDrops = 50;
    private int spawnedDrops = 0;

    private float timer = 0;
    private bool canSpawn = false;

    [Header("Settings Water Spawner for Leak Pipe")]
    public bool dropEnabled = false;
    private float dropInterval = 5f;
    private float secondTimer = 0f;

    public void startWaterSpawn()
    {
        canSpawn = true;
        timer = 0;
        spawnedDrops = 0;
    }

    public int getTotalDrops()
    {
        return totalDrops;
    }

    private void Update()
    {
        if (canSpawn)
        {
            if (spawnedDrops >= totalDrops) return;

            timer += Time.deltaTime;
        
            if (timer > spawnRate)
            {
                timer = 0;
                spawnDrop();
            }
        }

        if (dropEnabled) {
            secondTimer += Time.deltaTime;

            if (secondTimer >= dropInterval)
            {
                secondTimer = 0f;
                spawnSecondDrop();
            }
        
        }
        
    }

    void spawnSecondDrop()
    {
        Instantiate(waterDropPrefab, transform.position, Quaternion.identity);
    }

    void spawnDrop()
    {
        Instantiate(waterDropPrefab, transform.position, Quaternion.identity);  
        spawnedDrops++;
    }
}