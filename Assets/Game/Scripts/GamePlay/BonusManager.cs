using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BonusManager : MonoBehaviour
{
    public static BonusManager Instance => _instance;

    private static BonusManager _instance;

    [Header("UI")]
    public Button brushButton;        // Кнопка щётки
    public Button hammerButton;       // Кнопка молотка
    public TMPro.TextMeshProUGUI brushCountText;
    public TMPro.TextMeshProUGUI hammerCountText;

    private int brushCount = 5;
    private int hammerCount = 5;

    private bool isBrushActive = false;
    private bool isHammerActive = false;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateUI();

        brushButton.onClick.AddListener(() =>
        {
            isBrushActive = !isBrushActive;
            isHammerActive = false;
            UpdateButtonColor();
        });

        hammerButton.onClick.AddListener(() =>
        {
            isHammerActive = !isHammerActive;
            isBrushActive = false;
            UpdateButtonColor();
        });
    }

    public bool IsBonusActive()
    {
        return isBrushActive || isHammerActive;
    }

    public void OnPipeClicked(PipeSegment pipe)
    { 
        if (!IsBonusActive()) return;

        if (isBrushActive && pipe.IsDirty())
        {
            pipe.ClearDirty();
            brushCount--;
            DeactivateAll();
        }
        else if (isHammerActive && pipe.isPlayerPlaced)
        {
            hammerCount--;
            //Добавление обратно в инвентарь
            // InventoryManager.Instance?.ReturnPipe(pipe._pipeType);
            Destroy(pipe.gameObject);
            DeactivateAll();
        }
        UpdateUI();
    }

    private void DeactivateAll()
    {
        isBrushActive = false;
        isHammerActive = false;
        UpdateButtonColor();
    }

    private void UpdateButtonColor()
    {
        brushButton.image.color = isBrushActive ? Color.yellow : Color.white;
        hammerButton.image.color = isHammerActive ? Color.yellow : Color.white;
    }

    private void UpdateUI()
    {
        brushCountText.text = brushCount.ToString();
        hammerCountText.text = hammerCount.ToString();

        brushButton.interactable = brushCount > 0;
        hammerButton.interactable = hammerCount > 0;
    }
}
