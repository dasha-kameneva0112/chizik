using UnityEngine;

public class Pipe : MonoBehaviour
{
    public Vector2Int Size = Vector2Int.one;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void SetSkin(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
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