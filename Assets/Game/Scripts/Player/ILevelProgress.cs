public interface ILevelProgress
{
    int CurrentLevelNumber { get; }

    int GetOpenLevel();
    void UpdateNumberLevel();
}