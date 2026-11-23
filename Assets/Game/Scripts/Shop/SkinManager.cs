using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public event Action<SkinData> SelectionSkin;
    public static SkinManager Instance
    {
        get
        {
            // Если кто-то обратился, а инстанса ещё нет
            if (_instance == null)
            {
                GameObject go = new GameObject("[SkinManager]");
                _instance = go.AddComponent<SkinManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }
    public Dictionary<string, Status> Skins => _skins;

    private static SkinManager _instance; 
    private Dictionary<string, Status> _skins = new Dictionary<string, Status>();
    public SkinData CurrentSkin => _currentSkin;
    private SkinData _currentSkin;

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

    public void RegisterSkins(SkinData[] data)
    {
        foreach(var skin in data)
        {
            string id = skin.id;

            if (!_skins.ContainsKey(id))
            {
                if (skin.displayName == "Basic")
                {
                    _skins.Add(id, Status.Selected);
                }
                else
                {
                    _skins.Add(id, Status.NotBought);
                }
            }
        }
    }

    public bool TryBuySkin(SkinData skin)
    {
        if (Bank.Instance.SpendMoney(skin.price))
        {
            string id = skin.id;
            DeselectAllOfType(skin.type);

            // Покупаем и сразу выбираем
            if (_skins.ContainsKey(id))
            {
                _skins[id] = Status.Selected;
                SelectionSkin?.Invoke(skin);
            }

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
            _currentSkin = skin;
            SelectionSkin?.Invoke(skin);
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
        foreach (var skin in Resources.LoadAll<SkinData>(""))
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
}

public enum Status
{
    Purchased, // приобретенные
    NotBought, // еще не купленные 
    Selected, // выбранный
}
