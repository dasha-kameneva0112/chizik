using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUpLossController : MonoBehaviour
{
    [SerializeField] private GameObject _popUpLossPrefab;
    [SerializeField] private GameObject _parentForPopUp;
    [SerializeField] private GameObject _blackout;

    private void Start()
    {
        FinishPipe.Instance.OnLevelFailed += CreatePopUpLoss;
    }

    private void CreatePopUpLoss()
    {
        HideAllPipes();
        _blackout.SetActive(true);
        var popUp = Instantiate(_popUpLossPrefab, _parentForPopUp.transform);

        popUp.GetComponent<PopUpLoss>().TryAgainClick += SwitchToTryAgain;
        popUp.GetComponent<PopUpLoss>().MainMenuPopUpLossClick += SwitchToMainMenu;
    }

    private void SwitchToTryAgain()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
    private void SwitchToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void HideAllPipes()
    {
        foreach (var pipe in GameObject.FindGameObjectsWithTag("Pipe"))
        {
            pipe.SetActive(false);
        }
    }
}
