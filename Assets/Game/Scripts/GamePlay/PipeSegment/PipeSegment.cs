using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public enum pipeType
{
    Straight,
    Corner,
    Tee,
    Cross,
    Dirty,
    Leak,
    Offmap,
    NotConnected
}

public class PipeSegment : MonoBehaviour
{
    public pipeType pipeType;
    public Vector2Int mainFlowDirection;
    void Start()
    {
        mainFlowDirection = GetMainDirectionFromRotation();
    }

    Vector2Int GetMainDirectionFromRotation()
    {
        int rot = Mathf.RoundToInt(transform.eulerAngles.z) % 360;

        if (pipeType == pipeType.Straight)
        {
            if (rot == 0) return Vector2Int.right;
            if (rot == 90) return Vector2Int.down;
            if (rot == 180) return Vector2Int.left;
            if (rot == 270) return Vector2Int.up;
        }

        if (pipeType == pipeType.Corner)
        {
            if (rot == 0) return Vector2Int.right;
            if (rot == 90) return Vector2Int.down;
            if (rot == 180) return Vector2Int.left;
            if (rot == 270) return Vector2Int.up;
        }

        if (pipeType == pipeType.Tee)
            return Vector2Int.down;

        if (pipeType == pipeType.Cross)
            return Vector2Int.down;

        return Vector2Int.down;
    }

    public List<Vector2Int> getOutputDirections()
    {
        List<Vector2Int> dirs = new List<Vector2Int>();

        int rot = Mathf.RoundToInt(transform.eulerAngles.z);
        rot = (rot % 360 + 360) % 360;

        switch (pipeType)
        {
            case pipeType.Straight:
                if (rot == 0 || rot == 180)
                {
                    dirs.Add(Vector2Int.left);
                    dirs.Add(Vector2Int.right);
                }
                else
                {
                    dirs.Add(Vector2Int.up);
                    dirs.Add(Vector2Int.down);
                }
                break;
            case pipeType.Corner:
                if (rot == 0) dirs.AddRange(new[] { Vector2Int.up, Vector2Int.right });
                if (rot == 90) dirs.AddRange(new[] { Vector2Int.right, Vector2Int.down });
                if (rot == 180) dirs.AddRange(new[] { Vector2Int.down, Vector2Int.left });
                if (rot == 270) dirs.AddRange(new[] { Vector2Int.left, Vector2Int.up });
                break;
            case pipeType.Tee:
                if (rot == 0) dirs.AddRange(new[] { Vector2Int.left, Vector2Int.up, Vector2Int.right });
                if (rot == 90) dirs.AddRange(new[] { Vector2Int.up, Vector2Int.right, Vector2Int.down });
                if (rot == 180) dirs.AddRange(new[] { Vector2Int.right, Vector2Int.down, Vector2Int.left });
                if (rot == 270) dirs.AddRange(new[] { Vector2Int.down, Vector2Int.left, Vector2Int.up });
                break;
            case pipeType.Cross:
                dirs.AddRange(new[]
                {
                    Vector2Int.left,
                    Vector2Int.right,
                    Vector2Int.up,
                    Vector2Int.down,
                });
                break;
        }

        return dirs;
    }
}