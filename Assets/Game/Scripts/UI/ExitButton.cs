using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    [SerializeField] private GameObject confirmPanel;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick); 
       
        if (confirmPanel != null)
            confirmPanel.SetActive(false);
    }

    private void OnClick()
    {
        if (confirmPanel == null)
        {
            QuitGame();
        }
        else
        { 
            confirmPanel.SetActive(true);
        }
    }

    public void ConfirmQuit()
    {
        QuitGame();
    }

    public void CancelQuit()
    { 
        confirmPanel.SetActive(false);
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
