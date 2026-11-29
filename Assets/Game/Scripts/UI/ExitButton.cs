using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    [SerializeField] private GameObject _confirmPanel;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick); 
       
        if (_confirmPanel != null)
            _confirmPanel.SetActive(false);
    }

    private void OnClick()
    {
        if (_confirmPanel == null)
        {
            QuitGame();
        }
        else
        { 
            _confirmPanel.SetActive(true);
        }
    }

    public void ConfirmQuit()
    {
        QuitGame();
    }

    public void CancelQuit()
    { 
        _confirmPanel.SetActive(false);
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif   
    }
}
