using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    [SerializeField] private LevelTimer _timer;
    [SerializeField] private TMP_Text _text;

    private int _lastSecond;

    private void Start()
    {
        _lastSecond = Mathf.CeilToInt(_timer.GetTimeLeft());
        UpdateText(_lastSecond);
    }

    private void Update()
    {
        int currentSecond = Mathf.CeilToInt(_timer.GetTimeLeft());
        if (currentSecond != _lastSecond)
        {
            _lastSecond = currentSecond;
            UpdateText(currentSecond);
        }
    }

    private void UpdateText(int secondsLeft)
    {
        int minutes = secondsLeft / 60;
        int seconds = secondsLeft % 60;
        _text.text = $"{minutes:00}:{seconds:00}";
    }
}
