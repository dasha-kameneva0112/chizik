using UnityEngine;
using System;

public class LevelTimer : MonoBehaviour
{
    public float Duration = 60f;
    private float _timeLeft;
    private bool _running = false;

    public event Action OnTimeUp;

    private void Start()
    {
        StartTimer();
    }

    private void StartTimer()
    {
        _timeLeft = Duration;
        _running = true;
    }

    public void StopTimer()
    {
        _running = false;
    }

    private void Update()
    {
        if (!_running) return;

        _timeLeft -= Time.deltaTime;

        if (_timeLeft <= 0)
        {
            _timeLeft = 0;
            _running = false;
            OnTimeUp?.Invoke();
        }
    }

    public float GetTimeLeft()
    {
        return _timeLeft;
    }
}
