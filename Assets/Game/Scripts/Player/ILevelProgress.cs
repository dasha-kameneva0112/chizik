public interface ILevelProgress
{
    int CurrentLevelNumber { get; }
    bool AbilityToGetPrize { get; }

    int GetOpenLevel();
    void UpdateNumberLevel();
}