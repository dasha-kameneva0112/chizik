using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameField : MonoBehaviour
{
    [SerializeField] private Button StraightPipe;
    [SerializeField] private Button CornerPipe;
    [SerializeField] private Button TriangularPipe;
    [SerializeField] private Button CruciformPipe;

    [SerializeField] private List<Pipe> _startPipesOnFiels;

    // Размер игрового поля в клетках (ширина × высота)
    public Vector2Int GridSize = new Vector2Int(10, 10);

    // Смещение поля относительно мировых координат (0,0).
    public Vector2 GridOrigin = Vector2.zero;

    // Двумерный массив, хранит ссылки на размещённые трубы.(или null, если свободна).
    private Pipe[,] _grid;

    // Текущая "летающая" труба, которую игрок таскает мышкой
    private Pipe _flyingPipe;

    // Ссылка на главную камеру — нужна для преобразования позиции мыши в мир
    private Camera _camera;

    // Выполняется один раз при создании объекта
    private void Awake()
    {
        // Создаём пустую сетку заданного размера
        _grid = new Pipe[GridSize.x, GridSize.y];
        _camera = Camera.main; // Находим главную камеру на сцене
    }

    private void Start()
    {
        foreach (Pipe pipe in _startPipesOnFiels)
        {
            Vector3 worldPosition = pipe.transform.position;

            // Преобразуем мировые координаты в координаты клетки на сетке
            int gridX = Mathf.FloorToInt(worldPosition.x - GridOrigin.x);
            int gridY = Mathf.FloorToInt(worldPosition.y - GridOrigin.y);

            // Заполняем все клетки, которые занимает труба, ссылкой на неё
            // Это нужно, чтобы потом можно было проверить пересечения
            for (int x = 0; x < pipe.Size.x; x++)
            {
                for (int y = 0; y < pipe.Size.y; y++)
                {
                    _grid[gridX + x, gridY + y] = pipe;
                }
            }

            var finishPipe = pipe.GetComponent<FinishPipe>();
            if(finishPipe != null)
            {
                var view = pipe.GetComponent<BonusApplyPipe>();
                //view.Presenter.SetFinishPipe();
            }

            var bonus = pipe.GetComponent<BonusApplyPipe>();
            if (bonus != null)
            {
                bonus.SetAsPlayerPlaced();
            }
        }
    }

    

    // начать размещение новой трубы
    public void StartPlacingPipe(Pipe pipePrefab)
    {
        // Если уже есть летающая труба — удаляем её (чтобы не было дубликатов)
        if (_flyingPipe != null)
        {
            Destroy(_flyingPipe.gameObject);
        }

        // Создаём новую трубу из префаба и делаем её "летающей"
        _flyingPipe = Instantiate(pipePrefab);
        StraightPipe.interactable = false;
        CornerPipe.interactable = false;
        TriangularPipe.interactable = false;
        CruciformPipe.interactable = false;
    }

    // Обновляется каждый кадр
    private void Update()
    {
        // Если сейчас игрок таскает трубу
        if (_flyingPipe != null)
        {
            // Создаём бесконечную плоскость на уровне Z = 0 
            var groundPlane = new Plane(Vector3.forward, Vector3.zero);

            // Получаем текущую позицию мыши на экране (в пикселях)
            Vector2 mousePos = Mouse.current.position.ReadValue();

            // Преобразуем экранные координаты мыши в луч (ray) из камеры в мир
            var ray = _camera.ScreenPointToRay(new Vector3(mousePos.x, mousePos.y, 0));

            // Проверяем, пересекает ли луч нашу плоскость
            if (groundPlane.Raycast(ray, out float distance))
            {
                // Находим точку в мире, куда указывает мышь
                Vector3 worldPosition = ray.GetPoint(distance);

                // Преобразуем мировые координаты в координаты клетки на сетке
                int gridX = Mathf.FloorToInt(worldPosition.x - GridOrigin.x);
                int gridY = Mathf.FloorToInt(worldPosition.y - GridOrigin.y);

                // По умолчанию считаем, что место свободно
                bool available = true;

                // Проверяем, не выходит ли труба за границы поля (с учётом её размера!)
                if (gridX < 0 || gridX > GridSize.x - _flyingPipe.Size.x) available = false;
                if (gridY < 0 || gridY > GridSize.y - _flyingPipe.Size.y) available = false;

                // Если место в пределах поля — проверяем, не занято ли оно другой трубой
                if (available && IsPlaceTaken(gridX, gridY))
                    available = false;

                // Привязываем трубу к нижнему левому углу целевой клетки
                _flyingPipe.transform.position = new Vector2(
                    gridX + GridOrigin.x,
                    gridY + GridOrigin.y
                );

                // Если можно поставить и игрок кликнул левой кнопкой мыши в этом кадре
                if (available && Mouse.current.leftButton.wasPressedThisFrame)
                {
                    PlaceFlyingPipe(gridX, gridY); // Фиксируем трубу на поле
                    StraightPipe.interactable = true;
                    CornerPipe.interactable = true;
                    TriangularPipe.interactable = true;
                    CruciformPipe.interactable = true;
                }
            }
        }
    }

    // Проверяет, занята ли хоть одна клетка из области, куда хотим поставить трубу
    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < _flyingPipe.Size.x; x++)
        {
            for (int y = 0; y < _flyingPipe.Size.y; y++)
            {
                // Если в этой клетке уже есть труба — место занято
                if (_grid[placeX + x, placeY + y] != null)
                    return true;
            }
        }
        return false; // Все клетки свободны
    }


    // Окончательно размещает летающую трубу на поле
    private void PlaceFlyingPipe(int placeX, int placeY)
    {
        // Заполняем все клетки, которые занимает труба, ссылкой на неё
        // Это нужно, чтобы потом можно было проверить пересечения
        for (int x = 0; x < _flyingPipe.Size.x; x++)
        {
            for (int y = 0; y < _flyingPipe.Size.y; y++)
            {
                _grid[placeX + x, placeY + y] = _flyingPipe;
            }
        }

        BonusApplyPipe bonus = _flyingPipe.GetComponent<BonusApplyPipe>();
        if (bonus != null)
        {
            bonus.SetAsPlayerPlaced();
            bonus.ClickedPipe += BonusPipePresenter.Instance.OnPipeClicked;
        }
        // Труба больше не "летает" — игрок может взять новую
        _flyingPipe = null;
    }
}
