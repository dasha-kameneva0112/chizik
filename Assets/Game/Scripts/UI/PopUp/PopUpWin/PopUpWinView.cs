using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpWinView : MonoBehaviour
{
    public event Action RewardClicked;
    public event Action NextLevelClicked;
    public event Action MainMenuPopUpWinClicked;

    [SerializeField] private GameObject _popUpWinPrefab;
    [SerializeField] private GameObject _parentForPopUp;
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _blackout;
    [SerializeField] private Button _rewardButton;
    [SerializeField] private GameObject _inventory;

    public void HideAllPipes()
    {
        foreach (var pipe in GameObject.FindGameObjectsWithTag("Pipe"))
        {
            pipe.SetActive(false);
        }
    }

    public void SetWinBackground(Sprite background)
    {
        _background.GetComponent<Image>().sprite = background;
        _inventory.gameObject.SetActive(false);
    }

    public void ShowReward(int reward)
    {
        _rewardButton.gameObject.SetActive(true);
        _rewardButton.GetComponentInChildren<TextMeshProUGUI>().text = "+" + reward;
        _rewardButton.onClick.AddListener( () => RewardClicked?.Invoke() );
    }

    public void HideReward()
    {
        _rewardButton.gameObject.SetActive(false);
    }

    public void CreatePopUpWin(int level)
    {
        _blackout.SetActive(true);
        var popUp = Instantiate(_popUpWinPrefab, _parentForPopUp.transform);
        popUp.GetComponent<PopUpWin>().SetLevel(level);

        popUp.GetComponent<PopUpWin>().NextLevelClick += ( () => NextLevelClicked?.Invoke() );
        popUp.GetComponent<PopUpWin>().MainMenuPopUpWinClick += ( () => MainMenuPopUpWinClicked?.Invoke() );
    }
}
