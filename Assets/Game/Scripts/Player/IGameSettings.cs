using System;

public interface IGameSettings
{
    SkinData CurrentBackgroundSkin { get; }
    SkinData CurrentChizikSkin { get; }
    SkinData CurrentPipeSkin { get; }
    int CountOfBrush { get; }
    int CountOfHammer { get; }

    event Action SkinChanged;

    void ChangeCurrentSkin(SkinData skin);
    void UpdateCountOfSponge(int count);
    void UpdateCountOfHammer(int count);
}