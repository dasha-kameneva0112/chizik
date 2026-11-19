public class PopUpWinModel
{
    public int CountOfReward;
    public int NumberCurrentLevel;
    private readonly IBank _bank;
    private readonly ILevelProgress _levelProgress;

    public PopUpWinModel(IBank bank, ILevelProgress levelProgress, FinishPipe finishPipe, IGameSettings gameSettings, int countOfReward)
    {
        _bank = bank;
        _levelProgress = levelProgress;
        NumberCurrentLevel = _levelProgress.GetOpenLevel();
        CountOfReward = countOfReward;
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
