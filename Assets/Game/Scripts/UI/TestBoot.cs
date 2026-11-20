using UnityEngine;

public class TestBoot : MonoBehaviour
{
    private IGameSettings _gameSettings;
    private LevelProgress _levelProgress;

    [SerializeField] private SceneView _sceneView;

    private void Start()
    {
        _gameSettings = GameSettings.Instance;
        _levelProgress = LevelProgress.Instance;

        new ScenePresenter(_gameSettings, _levelProgress, _sceneView);
    }
}
