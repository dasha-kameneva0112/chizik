using UnityEngine;

public class Pipe : MonoBehaviour
{
    public Vector2Int Size = Vector2Int.one;

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