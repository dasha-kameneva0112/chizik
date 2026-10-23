using UnityEngine;

public class WaterSpawner : MonoBehaviour
{
    public GameObject waterDropPrefab;
    private float spawnRate = 0.1f; //частота

    private int totalDrops = 50;
    private int spawnedDrops = 0;

    private float timer = 0;
    private bool canSpawn = false;

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
        if (!canSpawn) return;

        if (spawnedDrops >= totalDrops) return;

        timer += Time.deltaTime;
        
        if (timer > spawnRate)
        {
            timer = 0;
            spawnDrop();
        }
    }

    void spawnDrop()
    {
        Instantiate(waterDropPrefab, transform.position, Quaternion.identity);  
        spawnedDrops++;
    }
}