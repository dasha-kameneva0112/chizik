using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaterFillingBar : MonoBehaviour
{
    [SerializeField] private Image _fill;
    [SerializeField] private TMP_Text _percent;

     void Start()
    {
        FinishPipe.Instance.DropArrived += UpdateBar;
    }

    private  void UpdateBar(int arrived, int total)
    {
        float fill = (float)arrived / total;
        _fill.fillAmount = fill;
        float percent = FinishPipe.Instance.getFinalPercant();
        _percent.text = percent.ToString("F1") + "%";
    }
}
