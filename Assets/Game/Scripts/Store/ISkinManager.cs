using System;
using System.Collections.Generic;

public interface ISkinManager
{
    Dictionary<string, Status> Skins { get; }

    event Action<SkinData> SelectionSkin;

    List<SkinData> Chiziks { get; }
    List<SkinData> BackGrounds { get; }
    List<SkinData> Pipes { get; }

    void DeleteAllSkinsFromPrefs();
    SkinData GetSelectedSkin(SkinType type);
    Status GetStatus(SkinData skin);
    void LoadSkinsFromPrefs();
    void RegisterSkins(SkinData[] data);
    void SaveSkinsToPrefs();
    void SelectSkin(SkinData skin);
    bool TryBuySkin(SkinData skin);
}