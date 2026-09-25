using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    public float pipeSpeed = 3f;
    private Rigidbody2D rb;
    private bool isInPipe = false;
    private Vector2 pipeDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe") && !isInPipe)
        {
            Debug.Log("💧 Вошла в трубу!");
            isInPipe = true;

            rb.gravityScale = 0f;

            // Берём нормаль столкновения и превращаем в направление движения
            pipeDirection = GetPipeDirection(collision);

            rb.linearVelocity = pipeDirection * pipeSpeed;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe") && isInPipe)
        {
            pipeDirection = GetPipeDirection(collision);
            rb.linearVelocity = pipeDirection * pipeSpeed;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            Debug.Log("💧 Вышла из трубы!");
            isInPipe = false;
            rb.gravityScale = 2f;
        }
    }

    /// <summary>
    /// Определяем направление движения вдоль трубы на основе нормали столкновения.
    /// </summary>
    Vector2 GetPipeDirection(Collision2D collision)
    {
        // нормаль столкновения (перпендикуляр к поверхности)
        Vector2 normal = collision.contacts[0].normal;

        // если труба горизонтальная → нормаль будет (0, ±1)
        if (Mathf.Abs(normal.y) > Mathf.Abs(normal.x))
        {
            // движемся влево или вправо вдоль трубы
            return new Vector2(Mathf.Sign(-transform.right.x), 0).normalized;
        }
        else
        {
            // если вертикальная труба → нормаль (±1, 0)
            // движемся вниз по трубе
            return new Vector2(0, -1);
        }
    }
}