//using NUnit.Framework;
//using System;
//using System.Collections.Generic;

//public class SkinManagerTests
//{
//    private IBank _bank;
//    private SkinData[] _testSkins;
//    private SkinManager _skinManager;

//    // Переменные для отслеживания событий
//    private bool _eventTriggered;
//    private SkinData _selectedSkinInEvent;

//    [SetUp]
//    public void Setup()
//    {
//        _bank = new TestBank(200); // Начальный баланс 200

//        // Создаем тестовые скины
//        _testSkins = new SkinData[]
//        {
//            new SkinData
//            {
//                id = "basic_chizik",
//                displayName = "Basic",
//                type = SkinType.Chizik,
//                price = 0
//            },
//            new SkinData
//            {
//                id = "winter_chizik",
//                displayName = "Winter",
//                type = SkinType.Chizik,
//                price = 100
//            },
//            new SkinData
//            {
//                id = "summer_chizik",
//                displayName = "Summer",
//                type = SkinType.Chizik,
//                price = 150
//            },
//            new SkinData
//            {
//                id = "basic_background",
//                displayName = "Basic",
//                type = SkinType.Background,
//                price = 0
//            },
//            new SkinData
//            {
//                id = "forest_background",
//                displayName = "Forest",
//                type = SkinType.Background,
//                price = 200
//            },
//            new SkinData
//            {
//                id = "basic_pipe",
//                displayName = "Basic",
//                type = SkinType.Pipe,
//                price = 0
//            }
//        };

//        // Сбрасываем состояние для событий
//        ResetEventState();
//    }

//    [TearDown]
//    public void TearDown()
//    {
//        // Очищаем PlayerPrefs после каждого теста
//        foreach (var skin in _testSkins)
//        {
//            UnityEngine.PlayerPrefs.DeleteKey("Skin_" + skin.id);
//        }
//        UnityEngine.PlayerPrefs.Save();
//    }

//    // Метод для сброса состояния событий
//    private void ResetEventState()
//    {
//        _eventTriggered = false;
//        _selectedSkinInEvent = null;
//    }

//    // Метод-обработчик события
//    private void OnSkinSelected(SkinData skin)
//    {
//        _eventTriggered = true;
//        _selectedSkinInEvent = skin;
//    }

    

//    [Test]
//    public void Test2_NonBasicSkinNotBought()
//    {
//        // Подготовка
//        _skinManager = new SkinManager(_bank, _testSkins);

//        // Действие
//        SkinData winterSkin = _testSkins[1];
//        Status status = _skinManager.GetStatus(winterSkin);

//        // Проверка
//        Assert.AreEqual(Status.NotBought, status, "Не базовый скин должен быть NotBought");
//    }

//    [Test]
//    public void Test3_BuySkinWithSufficientBalance()
//    {
//        // Подготовка
//        _skinManager = new SkinManager(_bank, _testSkins);

//        SkinData winterSkin = _testSkins[1];
//        int initialMoney = ((TestBank)_bank).Money;
//        _skinManager.SelectionSkin += OnSkinSelected;

//        // Действие
//        bool result = _skinManager.TryBuySkin(winterSkin);
//        Status status = _skinManager.GetStatus(winterSkin);

//        // Проверка
//        Assert.IsTrue(result, "Покупка должна быть успешной при достаточном балансе");
//        Assert.AreEqual(Status.Selected, status, "Купленный скин должен быть выбран");
//        Assert.IsTrue(_eventTriggered, "Событие SelectionSkin должно быть вызвано");
//        Assert.AreEqual(winterSkin, _selectedSkinInEvent, "Выбранный скин должен быть купленным");
//        Assert.AreEqual(initialMoney - 100, ((TestBank)_bank).Money, "Деньги должны быть списаны");

//        // Очистка
//        _skinManager.SelectionSkin -= OnSkinSelected;
//    }

//    [Test]
//    public void Test4_BuySkinWithInsufficientBalance()
//    {
//        // Подготовка
//        _bank = new TestBank(90); // Меньше чем цена скина (100)
//        _skinManager = new SkinManager(_bank, _testSkins);

//        SkinData winterSkin = _testSkins[1];
//        int initialMoney = ((TestBank)_bank).Money;
//        _skinManager.SelectionSkin += OnSkinSelected;

//        // Действие
//        bool result = _skinManager.TryBuySkin(winterSkin);
//        Status status = _skinManager.GetStatus(winterSkin);

//        // Проверка
//        Assert.IsFalse(result, "Покупка должна быть неуспешной при недостаточном балансе");
//        Assert.AreEqual(Status.NotBought, status, "Статус скина должен остаться NotBought");
//        Assert.IsFalse(_eventTriggered, "Событие SelectionSkin не должно быть вызвано");
//        Assert.AreEqual(initialMoney, ((TestBank)_bank).Money, "Деньги не должны быть списаны");

//        // Очистка
//        _skinManager.SelectionSkin -= OnSkinSelected;
//    }

    

//    [Test]
//    public void Test7_TrySelectNotBoughtSkin()
//    {
//        // Подготовка
//        _skinManager = new SkinManager(_bank, _testSkins);
//        SkinData winterSkin = _testSkins[1];
//        _skinManager.SelectionSkin += OnSkinSelected;

//        // Действие
//        _skinManager.SelectSkin(winterSkin);
//        Status status = _skinManager.GetStatus(winterSkin);

//        // Проверка
//        Assert.AreEqual(Status.NotBought, status, "Статус некупленного скина должен остаться NotBought");
//        Assert.IsFalse(_eventTriggered, "Событие SelectionSkin не должно быть вызвано");

//        // Очистка
//        _skinManager.SelectionSkin -= OnSkinSelected;
//    }

//    [Test]
//    public void Test8_GetStatusOfPurchasedSkin()
//    {
//        // Подготовка
//        _skinManager = new SkinManager(_bank, _testSkins);
//        SkinData winterSkin = _testSkins[1];
//        _skinManager.TryBuySkin(winterSkin);

//        // Действие
//        Status status = _skinManager.GetStatus(winterSkin);

//        // Проверка
//        Assert.AreEqual(Status.Selected, status, "Купленный скин должен иметь статус Selected");
//    }

    

//    [Test]
//    public void Test10_ResetAllSkins()
//    {
//        // Подготовка
//        _skinManager = new SkinManager(_bank, _testSkins);

//        SkinData winterSkin = _testSkins[1];
//        _skinManager.TryBuySkin(winterSkin);

//        // Действие
//        _skinManager.DeleteAllSkinsFromPrefs();

//        Status winterStatus = _skinManager.GetStatus(winterSkin);
//        Status basicChizikStatus = _skinManager.GetStatus(_testSkins[0]);
//        Status basicBackgroundStatus = _skinManager.GetStatus(_testSkins[3]);

//        // Проверка
//        Assert.AreEqual(Status.NotBought, winterStatus, "Не базовый скин должен стать NotBought после сброса");
//        Assert.AreEqual(Status.Selected, basicChizikStatus, "Базовый скин Chizik должен быть Selected");
//        Assert.AreEqual(Status.Selected, basicBackgroundStatus, "Базовый скин Background должен быть Selected");
//    }

//    [Test]
//    public void Test11_LoadSkinsFromPlayerPrefs()
//    {
//        // Подготовка
//        SkinData winterSkin = _testSkins[1];
//        UnityEngine.PlayerPrefs.SetString("Skin_" + winterSkin.id, "Selected");
//        UnityEngine.PlayerPrefs.Save();

//        // Действие
//        _skinManager = new SkinManager(_bank, _testSkins);
//        Status status = _skinManager.GetStatus(winterSkin);

//        // Проверка
//        Assert.AreEqual(Status.Selected, status, "Статус скина должен быть восстановлен из PlayerPrefs");
//    }

//    [Test]
//    public void Test_RegisterSkins_DuplicateSkins()
//    {
//        // Подготовка
//        SkinData[] duplicateSkins = new SkinData[]
//        {
//            new SkinData { id = "test1", displayName = "Test1", type = SkinType.Chizik },
//            new SkinData { id = "test1", displayName = "Test1", type = SkinType.Chizik }
//        };

//        // Действие и проверка
//        Assert.DoesNotThrow(() =>
//        {
//            _skinManager = new SkinManager(_bank, duplicateSkins);
//        });
//    }

//    [Test]
//    public void Test_SelectSkin_EventFired()
//    {
//        // Подготовка
//        _skinManager = new SkinManager(_bank, _testSkins);
//        _skinManager.TryBuySkin(_testSkins[1]);

//        ResetEventState();
//        _skinManager.SelectionSkin += OnSkinSelected;

//        // Действие
//        _skinManager.SelectSkin(_testSkins[1]);

//        // Проверка
//        Assert.IsTrue(_eventTriggered, "Событие должно быть вызвано");
//        Assert.AreEqual(_testSkins[1], _selectedSkinInEvent, "Событие должно содержать правильный скин");

//        // Очистка
//        _skinManager.SelectionSkin -= OnSkinSelected;
//    }

//    [Test]
//    public void Test_GetSelectedSkin_ReturnsNullWhenNoSelected()
//    {
//        // Подготовка
//        SkinData[] singleSkin = new SkinData[]
//        {
//            new SkinData { id = "test", displayName = "Test", type = SkinType.Chizik, price = 100 }
//        };
//        _skinManager = new SkinManager(_bank, singleSkin);

//        // Действие
//        SkinData selected = _skinManager.GetSelectedSkin(SkinType.Background);

//        // Проверка
//        Assert.IsNull(selected, "Должен возвращать null для типа без выбранных скинов");
//    }

//    [Test]
//    public void Test_SaveAndLoadCycle()
//    {
//        // Подготовка
//        _skinManager = new SkinManager(_bank, _testSkins);

//        SkinData winterSkin = _testSkins[1];
//        _skinManager.TryBuySkin(winterSkin);

//        // Действие
//        _skinManager.SaveSkinsToPrefs();
//        SkinManager loadedManager = new SkinManager(_bank, _testSkins);
//        Status status = loadedManager.GetStatus(winterSkin);

//        // Проверка
//        Assert.AreEqual(Status.Selected, status, "Статус должен сохраниться после загрузки");
//    }

    
//}

//// Реализация IBank для тестов
//public class TestBank : IBank
//{
//    public int Money { get; private set; }

//    public TestBank(int initialMoney)
//    {
//        Money = initialMoney;
//    }

//    public bool IsEnoughMoney(int amount)
//    {
//        return Money >= amount;
//    }

//    public void DebitMoney(int amount)
//    {
//        Money -= amount;
//    }

//    public void DepositMoney(int money)
//    {
//        Money += money;
//    }
//}