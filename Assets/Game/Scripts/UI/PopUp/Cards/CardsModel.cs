public class CardsModel
{
    private IBank _bank;
    private IGameSettings _gameSettings;

    public CardsModel(IBank bank, IGameSettings gameSettings)
    {
        _bank = bank;
        _gameSettings = gameSettings;
    }
}
