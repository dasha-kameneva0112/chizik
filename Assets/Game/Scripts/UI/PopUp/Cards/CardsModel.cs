public class CardsModel
{
    public PrizeData GetPrize(int index) => _currentPrizes[index-1];

    private readonly IBank _bank;
    private readonly IGameSettings _gameSettings;
    private readonly PrizeData[] _prizeDatas;

    private PrizeData[] _currentPrizes;

    public CardsModel(IBank bank, IGameSettings gameSettings, PrizeData[] prizeDatas)
    {
        _bank = bank;
        _gameSettings = gameSettings;
        _prizeDatas = prizeDatas;

        SetRandomPrizeData();
    }

    private void SetRandomPrizeData()
    {
        PrizeData[] prizeDatas = _prizeDatas;
        Shuffle(prizeDatas);
        _currentPrizes = prizeDatas;
    }

    private static void Shuffle(PrizeData[] prizeDatas)
    {
        for (int i = 0; i < prizeDatas.Length; i++)
        {
            int rnd = UnityEngine.Random.Range(i, prizeDatas.Length);
            (prizeDatas[i], prizeDatas[rnd]) = (prizeDatas[rnd], prizeDatas[i]);
        }
    }

    public void UpdateGameSettings(int index)
    {
        var prize = GetPrize(index);
        if(prize.type == PrizeType.Brush)
        {
            _gameSettings.UpdateCountOfSponge(prize.count);
        }
        if(prize.type == PrizeType.Hammer)
        {
            _gameSettings.UpdateCountOfHammer(prize.count);
        }
        if(prize.type == PrizeType.Money)
        {
            _bank.DepositMoney(prize.count);
        }
    }
}
