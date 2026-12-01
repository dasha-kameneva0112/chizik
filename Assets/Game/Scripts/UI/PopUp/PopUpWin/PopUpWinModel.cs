public class PopUpWinModel
{
    public int CountOfReward => _countOfReward;
    public int NumberCurrentLevel;
    private readonly IBank _bank;
    private readonly ILevelProgress _levelProgress;
    private int _countOfReward;

    public PopUpWinModel(IBank bank, ILevelProgress levelProgress, FinishPipe finishPipe, IGameSettings gameSettings, int countOfReward)
    {
        _bank = bank;
        _levelProgress = levelProgress;
        NumberCurrentLevel = _levelProgress.GetOpenLevel();
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
