public class PopUpWinModel
{
    public int CountOfReward => _countOfReward;
    public int NumberCurrentLevel => _numberCurrentLevel;

    private readonly IBank _bank;
    private readonly IGameSettings _gameSettings;
    private readonly ILevelProgress _levelProgress;
    private int _countOfReward;
    private int _numberCurrentLevel;
    private readonly PrizeData[] _prizeDatas;

    public PopUpWinModel(IBank bank, IGameSettings gameSettings, ILevelProgress levelProgress, int countOfReward, PrizeData[] prizeDatas)
    {
        _bank = bank;
        _gameSettings = gameSettings;
        _levelProgress = levelProgress;
        _numberCurrentLevel = _levelProgress.GetOpenLevel();
        _countOfReward = countOfReward;
        _prizeDatas = prizeDatas;
    }

    public void CompleteLevel()
    {
        _levelProgress.UpdateNumberLevel();
    }

    public void AddReward()
    {
        _bank.DepositMoney(CountOfReward);
    }

    public bool AbilityGetPrize()
    {
        if (_levelProgress.AbilityToGetPrize)
            return true;
        return false;
    }

    public CardsModel CreateCardsModel()
    {
        return new CardsModel(_bank, _gameSettings, _prizeDatas);
    }
}
