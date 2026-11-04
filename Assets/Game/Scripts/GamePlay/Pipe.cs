using UnityEngine;

public enum pipeType
{
    Normal,
    Leak,
    Dirty
}
public class Pipe : MonoBehaviour
{
    public Vector2Int Size = Vector2Int.one;
    public pipeType pipeType = pipeType.Normal;
    public Vector2Int[] baseDirections;

    [SerializeField] private SpriteRenderer _spriteRenderer;

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

    public void SetSkin(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    private void OnDrawGizmosSelected()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                Vector2 position = (Vector2)transform.position + new Vector2(x, y);
            }
        }
    }
}