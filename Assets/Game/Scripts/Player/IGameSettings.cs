using System;

public interface IGameSettings
{
    SkinData CurrentBackgroundSkin { get; }
    SkinData CurrentChizikSkin { get; }
    SkinData CurrentPipeSkin { get; }
    int CountOfBrush { get; }
    int CountOfHammer { get; }

    event Action SkinChanged;
}