using System;
using System.Collections.Generic;
using UnityEngine;

public class FinishPipe : MonoBehaviour
{
    public event Action<int,int> DropArrived;
    public event Action OnLevelFailed;
    public event Action OnLevelCompleted;

    public static FinishPipe Instance;

    public HashSet<Pipe> leakedPipes = new HashSet<Pipe>();
    private int arrived = 0;
    private int totalDrops = 0;
    private int failedDrops = 0;
    private bool resultat_shown = false;
    private AudioManager _audioManager;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _audioManager = AudioManager.Instance;
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
            DropArrived?.Invoke(arrived, totalDrops);

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
        if (proccessed >= totalDrops && !resultat_shown)
        {
            float result = getFinalPercant();
            resultat_shown = true;
            if (result >= 91f)
            {
                _audioManager.StopClickSound();
                OnLevelCompleted?.Invoke();
                return;
            }
            else
            {
                _audioManager.StopClickSound();
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
