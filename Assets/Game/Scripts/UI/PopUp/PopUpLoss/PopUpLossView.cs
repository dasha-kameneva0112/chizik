using System;
using UnityEngine;

public class PopUpLossView : MonoBehaviour
{
    public event Action TryAnainClicked;
    public event Action MainMenuPopUpLossClicked;

    [SerializeField] private GameObject _popUpLossPrefab;
    [SerializeField] private GameObject _parentForPopUp;
    [SerializeField] private GameObject _blackout;

    public void HideAllPipes()
    {
        foreach (var pipe in GameObject.FindGameObjectsWithTag("Pipe"))
        {
            pipe.SetActive(false);
        }
    }

    public void CreatePopUpLoss()
    {
        _blackout.SetActive(true);
        var popUp = Instantiate(_popUpLossPrefab, _parentForPopUp.transform);

        popUp.GetComponent<PopUpLoss>().TryAgainClick += ( () => TryAnainClicked?.Invoke() );
        popUp.GetComponent<PopUpLoss>().MainMenuPopUpLossClick += ( () => MainMenuPopUpLossClicked?.Invoke() );
    }
}
