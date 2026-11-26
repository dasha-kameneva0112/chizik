using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BonusManager : MonoBehaviour
{
    public static BonusManager Instance => _instance;
    private static BonusManager _instance;

    [Header("UI")]
    [SerializeField] private Button _brushButton;
    [SerializeField] private Button _hammerButton;
    [SerializeField] private TMPro.TextMeshProUGUI _brushCountText;
    [SerializeField] private TMPro.TextMeshProUGUI _hammerCountText;

    [Header("Чистые префабы")]
    [SerializeField] private GameObject _cleanStraightPipePrefab;   
    [SerializeField] private GameObject _cleanCornerPipePrefab;

    [Header("Блокировка инвентаря")]
    [SerializeField] private GameObject image;

    private int brushCount = 5;
    private int hammerCount = 5;

    private bool isBrushActive = false;
    private bool isHammerActive = false;

    private bool isHammerLocked;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateUI();

        _brushButton.onClick.AddListener(() =>
        {
            isBrushActive = !isBrushActive;
            isHammerActive = false;
            UpdateButtonColor();
        });

        _hammerButton.onClick.AddListener(() =>
        {
            isHammerActive = !isHammerActive;
            isBrushActive = false;
            UpdateButtonColor();
        });

        if (AnyLevel.Instance != null)
        {
            AnyLevel.Instance.RemoveCountUpdate += OnRemoveCountUpdate;
        }

        FinishPipe.Instance.OnLevelCompleted += (() => gameObject.SetActive(false));
    }

    private void OnRemoveCountUpdate(int newCount)
    {
        isHammerLocked = AnyLevel.Instance.removeCount == 0;
        if (AnyLevel.Instance.removeCount == 0)
        {
            _hammerButton.interactable = true;
        }
        else
        {
            _hammerButton.interactable = false;
        }
    }
    public bool IsBonusActive() => isBrushActive || isHammerActive;

    public void OnPipeClicked(BonusPipePresenter pipe)
    {
        if (!IsBonusActive()) return;

        if (isBrushActive && pipe.IsDirty())
        {
            GameObject cleanPrefab = null;

            if (pipe.GetPipeType() == PipeType.DirtyStraightPipe)
                cleanPrefab = _cleanStraightPipePrefab;
            else if (pipe.GetPipeType() == PipeType.DirtyCornerPipe)
                cleanPrefab = _cleanCornerPipePrefab;

            if (cleanPrefab != null)
            {
                Instantiate(cleanPrefab, pipe.GetPosition(), pipe.GetRotation());
            }

            brushCount--;
            DeactivateAll();
            UpdateUI();
            pipe.DestroyPipe();
            return;
        }

        if (isHammerActive)
        {

            if (pipe.GetPipeType() == PipeType.DirtyStraightPipe || pipe.GetPipeType() == PipeType.DirtyCornerPipe)
                return;

            if (Keyboard.current.backspaceKey.wasPressedThisFrame)
            {
                Vector2 mousePos = Mouse.current.position.ReadValue();
                Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

                Collider2D[] allColliders = GetComponents<Collider2D>();

                foreach (Collider2D col in allColliders)
                {
                    if (col.bounds.Contains(worldPos))
                    {
                        Destroy(gameObject);
                    }
                }
            }

            hammerCount--;
            Inventory.Instance.Delete(pipe.GetPipeType()); 
            DeactivateAll();
            pipe.DestroyPipe();
            UpdateUI();
            return;
        }
    }

    private void DeactivateAll()
    {
        isBrushActive = false;
        isHammerActive = false;
        UpdateButtonColor();

       image.SetActive(false);
    }

    private void UpdateButtonColor()
    {
        _brushButton.image.color = isBrushActive ? Color.yellow : Color.white;
        _hammerButton.image.color = isHammerActive ? Color.yellow : Color.white;

        bool bonusActive = isBrushActive || isHammerActive;
        image.SetActive(bonusActive);
    }

    private void UpdateUI()
    {
        _brushCountText.text = brushCount.ToString();
        _hammerCountText.text = hammerCount.ToString();
        _brushButton.interactable = brushCount > 0;
        _hammerButton.interactable = hammerCount > 0 && isHammerLocked;
    }
}