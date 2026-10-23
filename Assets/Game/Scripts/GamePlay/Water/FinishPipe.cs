using System.Collections.Generic;
using UnityEngine;

public class FinishPipe : MonoBehaviour
{
    public static FinishPipe Instance;

    private int arrived = 0;
    public HashSet<PipeSegment> leakedPipes = new HashSet<PipeSegment>();

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
            Debug.Log("Result" + getFinalPercant() + "%");
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
