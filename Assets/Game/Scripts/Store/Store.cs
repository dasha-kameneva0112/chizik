using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [SerializeField] private StoreView _storeView;

    private StorePresenter _presenter;

    private void Awake()
    {
        _presenter = new StorePresenter(_storeView);
    }
}
