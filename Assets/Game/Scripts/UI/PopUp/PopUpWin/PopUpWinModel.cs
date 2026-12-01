public class PopUpWinModel
{
    public int CountOfReward => _countOfReward;
    public int NumberCurrentLevel => _numberCurrentLevel;

    private readonly IBank _bank;
    private readonly ILevelProgress _levelProgress;
    private int _countOfReward;
    private int _numberCurrentLevel;

    public PopUpWinModel(IBank bank, ILevelProgress levelProgress, int countOfReward)
    {
        _bank = bank;
        _levelProgress = levelProgress;
        _numberCurrentLevel = _levelProgress.GetOpenLevel();
        _countOfReward = countOfReward;
    }

    public void CompleteLevel()
    {
        _levelProgress.UpdateNumberLevel();
    }

    public void AddReward()
    {
        _bank.DepositMoney(CountOfReward);
    }
}
