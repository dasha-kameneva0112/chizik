using UnityEngine;
using UnityEngine.UI;

public class SkinChangeController : MonoBehaviour
{
    [SerializeField] private Image _chizik;
    [SerializeField] private Image _background;
    [SerializeField] private GameObject _pipePrefab; 
    
    private void Start()
    {
        SkinManager.Instance.SelectionSkin += ChangeSkin;
    }

    private void ChangeSkin(SkinData skin)
    {
        if(skin.type == SkinType.Chizik)
        {
            _chizik.sprite = skin.icon;
        }
        if(skin.type == SkinType.Background)
        {
            _background.sprite = skin.icon;
        }
        //if( skin.type == SkinType.Pipe)
        //{
        //    _pipePrefab.GetComponentInChildren<Image>().sprite = skin.icon; 
        //}
    }
}
