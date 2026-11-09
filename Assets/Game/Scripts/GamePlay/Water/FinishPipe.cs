using System;
using System.Collections.Generic;
using UnityEngine;

public class FinishPipe : MonoBehaviour
{
    public event Action OnLevelFailed;
    public event Action OnLevelCompleted;

    public static FinishPipe Instance;

    private int arrived = 0;
    public HashSet<Pipe> leakedPipes = new HashSet<Pipe>();

    private int totalDrops = 0;
    private int failedDrops = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        WaterSpawner spawner = FindObjectOfType<WaterSpawner>();
        if (spawner)
        {
            totalDrops = spawner.getTotalDrops();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WaterDrop d = collision.GetComponent<WaterDrop>();
        if (d)
        {
            arrived++;  

            Destroy(d.gameObject);

            tryShowResult();
        }
    }

    public void notifyDropDestroyed()
    {
        failedDrops++;
        tryShowResult();
    }

    private void tryShowResult()
    {
        int proccessed = arrived + failedDrops;
        if (proccessed >= totalDrops)
        {
            float result = getFinalPercant();
            if (result >= 91f)
            {
                OnLevelCompleted?.Invoke();
                return;
            }
            else
            {
                OnLevelFailed?.Invoke();
                return;
            }
        }
    }

    public float getFinalPercant()
    {
        if (arrived == 0) return 0;
        int leaks = leakedPipes.Count;
        float loss = Mathf.Clamp01(1f - 0.04f * leaks);

        return ((float)arrived/(float)totalDrops) * loss * 100f;
    }
}
