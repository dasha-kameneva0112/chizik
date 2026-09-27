using System.Collections.Generic;
using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    public Transform startPipe;
    public Transform targetPipe;

    public float waterAmount = 100f;
    public float resultAmount = 0f;

    private Dictionary<Vector2Int, PipeSegment> grid = new Dictionary<Vector2Int, PipeSegment>();

    void Start()
    {
        BuildGrid();
        resultAmount = Simulate(startPipe.GetComponent<PipeSegment>());
        Debug.Log("Water result = " + resultAmount + "%");
    }

    void BuildGrid()
    {
        PipeSegment[] pipes = FindObjectsOfType<PipeSegment>();
        foreach (var p in pipes)
        {
            Vector2Int cell = Vector2Int.RoundToInt(p.transform.position);

            if (!grid.ContainsKey(cell))
                grid[cell] = p;
        }
    }

    float Simulate(PipeSegment pipe)
    {
        HashSet<PipeSegment> visited = new HashSet<PipeSegment>();
        return Flow(pipe, visited, waterAmount);
    }

    float Flow(PipeSegment pipe, HashSet<PipeSegment> visited, float amount)
    {
        if (pipe == null) return 0;
        if (visited.Contains(pipe)) return 0;

        visited.Add(pipe);

        // грязная - вода останавливается
        if (pipe.pipeType == pipeType.Dirty)
            return 0;

        // утечка - -4%
        if (pipe.pipeType == pipeType.Leak)
            amount -= 4;

        // дошли до цели
        if (pipe.transform == targetPipe)
            return amount;

        // получаем все выходные направления
        List<Vector2Int> dirs = pipe.getOutputDirections();

        float bestResult = 0;

        Vector2Int cell = Vector2Int.RoundToInt(pipe.transform.position);

        foreach (var d in dirs)
        {
            Vector2Int nextCell = cell + d;

            // нет трубы - вода ушла (0%)
            if (!grid.ContainsKey(nextCell))
                continue;

            PipeSegment nextPipe = grid[nextCell];

            float r = Flow(nextPipe, visited, amount);

            if (r > bestResult)
                bestResult = r;
        }

        return bestResult;
    }
}
