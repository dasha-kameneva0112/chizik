using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public event Action<SkinData> SelectionSkin;
    public static SkinManager Instance => _instance;
    public Dictionary<string, Status> Skins => _skins;    
    public List<SkinData> _chizikSkins = new();
    public List<SkinData> _backgroundSkins = new();
    public List<SkinData> _pipeSkins = new();

    [SerializeField] private SkinData[] _allSkins;

    //private List<SkinData> _dataBaseOfSkins = new List<SkinData>();
    private static SkinManager _instance;
    private IBank _bank;
    private Dictionary<string, Status> _skins = new Dictionary<string, Status>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        RegisterSkins(_allSkins);
        _bank = Bank.Instance;
    }

    public void RegisterSkins(SkinData[] data)
    {
        foreach(var skin in data)
        {
            string id = skin.id;

            if (!_skins.ContainsKey(id))
            {
                //_dataBaseOfSkins.Add(skin);
                if (skin.displayName == "Basic")
                {
                    _skins.Add(id, Status.Selected);
                    SelectionSkin?.Invoke(skin);
                }
                else
                {
                    _skins.Add(id, Status.NotBought);
                }

                if(skin.type == SkinType.Chizik)
                {
                    _chizikSkins.Add(skin);
                }
                else if(skin.type == SkinType.Background)
                {
                    _backgroundSkins.Add(skin);
                } 
                else
                {
                    _pipeSkins.Add(skin);
                }
            }
        }
        LoadSkinsFromPrefs();
    }

    public bool TryBuySkin(SkinData skin)
    {
        if (_bank.IsEnoughMoney(skin.price))
        {
            _bank.DebitMoney(skin.price);

            string id = skin.id;
            DeselectAllOfType(skin.type);

            // Покупаем и сразу выбираем
            if (_skins.ContainsKey(id))
            {
                _skins[id] = Status.Selected;
                SelectionSkin?.Invoke(skin);
            }
            SaveSkinsToPrefs();
            return true;
        }

        return false;
    }

    public void SelectSkin(SkinData skin)
    {
        string id = skin.id;
        DeselectAllOfType(skin.type);
        if (_skins.ContainsKey(id) && _skins[id] != Status.NotBought)
        {
            _skins[id] = Status.Selected;
            SelectionSkin?.Invoke(skin);
            SaveSkinsToPrefs();
        }
    }

    private void DeselectAllOfType(SkinType type)
    {
        // Сначала собираем все ключи, которые нужно поменять
        var keysToChange = new List<string>();
        foreach (var kvp in _skins)
        {
            SkinData s = FindSkinById(kvp.Key);
            if (s != null && s.type == type && kvp.Value == Status.Selected)
            {
                keysToChange.Add(kvp.Key);
            }
        }

        // Потом меняем статусы
        foreach (var key in keysToChange)
        {
            _skins[key] = Status.Purchased;
        }
    }

    private SkinData FindSkinById(string id)
    {
        //SkinData[] allSkins = Resources.LoadAll<SkinData>("Assets/Game/Configs/SkinSO");
        foreach (var skin in _allSkins)
        {
            if (skin.id == id) return skin;
        }
        return null;
    }

    public Status GetStatus(SkinData skin)
    {
        if (_skins.TryGetValue(skin.id, out Status status))
            return status;

        return Status.NotBought;
    }

    public SkinData GetSelectedSkin(SkinType type)
    {
        foreach (var kvp in _skins)
        {
            if (kvp.Value == Status.Selected)
            {
                SkinData s = FindSkinById(kvp.Key);
                if (s != null && s.type == type)
                {
                    return s;
                }
            }
        }
        return null;
    }

    public void SaveSkinsToPrefs()
    {
        foreach (var skin in _skins)
        {
            PlayerPrefs.SetString("Skin_" + skin.Key, skin.Value.ToString());
        }

        PlayerPrefs.Save();
    }

    public void LoadSkinsFromPrefs()
    {
        foreach (var key in _skins.Keys.ToList())
        {
            _skins[key] = Status.NotBought;
        }

        var keys = new List<string>(_skins.Keys);

        foreach (string id in keys)
        {
            string key = "Skin_" + id;

            if (PlayerPrefs.HasKey(key))
            {
                string saved = PlayerPrefs.GetString(key);
                if (Enum.TryParse(saved, out Status s))
                {
                    _skins[id] = s;
                }
            }
        }

        foreach (var kvp in _skins)
        {
            if (kvp.Value == Status.Selected)
            {
                SkinData s = FindSkinById(kvp.Key);
                if (s != null)
                {
                    SelectionSkin?.Invoke(s);
                }    
            }
        }
    }

    public void DeleteAllSkinsFromPrefs()
    {
        foreach (var id in _skins.Keys.ToList())
        {
            PlayerPrefs.DeleteKey("Skin_" + id);
        }
        PlayerPrefs.Save();

        foreach (var id in _skins.Keys.ToList())
        {
            SkinData skin = FindSkinById(id);

            if (skin != null && skin.displayName == "Basic")
                _skins[id] = Status.Selected;
            else
                _skins[id] = Status.NotBought;
        }

        foreach (var kvp in _skins)
        {
            if (kvp.Value == Status.Selected)
            {
                SkinData skin = FindSkinById(kvp.Key);
                if (skin != null)
                    SelectionSkin?.Invoke(skin);
            }
        }

        SaveSkinsToPrefs();
    }

#if UNITY_EDITOR
    [MenuItem("Tools/Skins/Reset All Skins")]
    public static void EditorResetAllSkins()
    {
        SkinManager manager = FindAnyObjectByType<SkinManager>();
        manager.DeleteAllSkinsFromPrefs();
    }
#endif

}

public enum Status
{
    Purchased, // приобретенные
    NotBought, // еще не купленные 
    Selected, // выбранный
}
