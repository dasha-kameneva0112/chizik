using UnityEngine;

public class TestBootLevels : MonoBehaviour
{
    [SerializeField] private LevelsView _levelsView;

    private void Start()
    {
        new LevelsPresenter(_levelsView);
    }
}
