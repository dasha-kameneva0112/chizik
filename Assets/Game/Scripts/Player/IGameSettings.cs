using System;

public interface IGameSettings
{
    SkinData CurrentBackgroundSkin { get; }
    SkinData CurrentChizikSkin { get; }
    SkinData CurrentPipeSkin { get; }

    event Action SkinChanged;
}