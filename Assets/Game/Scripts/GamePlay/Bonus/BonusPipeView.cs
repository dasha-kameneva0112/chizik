using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BonusPipeView : MonoBehaviour
{
    [SerializeField] private Button brushButton;
    [SerializeField] private Button hammerButton;
    [SerializeField] private TextMeshProUGUI brushText;
    [SerializeField] private TextMeshProUGUI hammerText;
    [SerializeField] private GameObject blockImage;

    public event Action BrushClicked;
    public event Action HammerClicked;

    private void Awake()
    {
        brushButton.onClick.AddListener(() => BrushClicked?.Invoke());
        hammerButton.onClick.AddListener(() => HammerClicked?.Invoke());
    }

    public void UpdateCounts(int brush, int hammer, bool isRemoveEnded)
    {
        brushText.text = brush.ToString();
        hammerText.text = hammer.ToString();

        brushButton.interactable = brush > 0;
        if(isRemoveEnded)
        {
            hammerButton.interactable = hammer > 0;
        } else hammerButton.interactable = false;
    }

    public void SetBrushActive(bool active)
    {
        brushButton.image.color = active ? Color.yellow : Color.white;
    }

    public void SetHammerActive(bool active)
    {
        hammerButton.image.color = active ? Color.yellow : Color.white;
    }

    public BonusApplyPipe ReplaceWithCleanPipe(GameObject cleanPrefab, BonusApplyPipe bonusPipe)
    {
        var newPipe = Instantiate(cleanPrefab, bonusPipe.transform.position, bonusPipe.transform.rotation);
        return newPipe.GetComponent<BonusApplyPipe>();
    }

}
