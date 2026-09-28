using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public enum pipeType
{
    Normal,
    Leak,
    Dirty
}

public class PipeSegment : MonoBehaviour
{
    public pipeType pipeType = pipeType.Normal;
    public Vector2Int[] baseDirections;

    public Vector3 getOutputDirection(Vector3 dropWorldPos, Vector3 currentDir)
    {
        Vector3 localDrop = transform.InverseTransformDirection(dropWorldPos);

        Vector2Int enteringDir;

        if (Mathf.Abs(localDrop.x) > Mathf.Abs(localDrop.y))
        {
            enteringDir = (localDrop.x > 0) ? Vector2Int.right : Vector2Int.left;
        }
        else
        {
            enteringDir = (localDrop.y > 0) ? Vector2Int.up : Vector2Int.down;
        }

        bool validEnter = false;
        foreach (var direction in baseDirections)
        {
            if (direction == enteringDir)
            {
                validEnter = true;
                break;
            }
        }

        if (!validEnter)
        {
            return currentDir.normalized;
        }

        foreach (var direction in baseDirections)
        {
            if (direction == -enteringDir) continue;

            Vector3 worldDir = transform.TransformDirection(new Vector3(direction.x, direction.y, 0));
            return worldDir.normalized;
        }

        return Vector3.zero;
    }
}