using UnityEngine;

public class TestBoot : MonoBehaviour
{
    private IGameSettings _gameSettings;
    private ILevelProgress _levelProgress;
    private IBank _bank;
    private ISkinManager _skinManager;

    [SerializeField] private SceneView _sceneView;
    [SerializeField] private LevelsView _levelsView;
    [SerializeField] private BankView _bankView;
    [SerializeField] private StoreView _storeView;
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private PanelManagerView _panelManagerView;

    [SerializeField] private readonly int _startMoney = 1000;
    [SerializeField] private SkinData[] _allSkins;

    private void Start()
    {
        _gameSettings = GameSettings.Instance;
        _levelProgress = LevelProgress.Instance;

        var bankModel = new BankModel(_startMoney);
        _bank = new BankPresenter(bankModel, _bankView);

        _skinManager = new SkinManager(_bank, _gameSettings, _allSkins);
        new PanelManager(_panelManagerView, _skinManager, _bank);

        new ScenePresenter(_gameSettings, _levelProgress, _sceneView);
        new LevelsPresenter(_levelsView);        

        new StorePresenter(_storeView, _mainMenu);
    }
}
