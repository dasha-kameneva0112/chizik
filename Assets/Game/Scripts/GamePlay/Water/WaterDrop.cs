using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class WaterDrop : MonoBehaviour
{
    private float speed = 3f;

    private Pipe currentPipe;
    private Vector2 direction;

    private Rigidbody2D rb;

    public bool touchedLink = false;

    private float flowforce = 1f;
    private float gravityForce = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        currentPipe = getPipeUnder();
        direction = Vector2.down;
    }

    private void FixedUpdate()
    {
        Vector2 force = Vector2.zero;

        force += Vector2.down * gravityForce;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f);
        if (hit.collider != null)
        {
            Pipe pipe = hit.collider.GetComponent<Pipe>();
            if (pipe != null)
            {
                Vector3 outDir = pipe.getOutputDirection(transform.position, direction);
                if (outDir == Vector3.zero)
                {
                    Destroy(gameObject);
                    return;
                }

                direction = outDir.normalized;

                force += (Vector2)direction * flowforce;
            }
        }

        rb.AddForce(force, ForceMode2D.Force);
    }

    Pipe getPipeUnder()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.1f);
        if (hit)
        {
            return hit.GetComponent<Pipe>();
        }
        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("KillZone"))
        {
            die();
            return;
        }

        if (collision.CompareTag("LeakZone"))
        {
            Destroy(gameObject);
            return;
        }

        Pipe pipe = collision.GetComponent<Pipe>();
        if (pipe == null)
        {
            die();
            return;
        }

        currentPipe = pipe;

        if (pipe.pipeType == pipeType.Dirty)
        {
            die();
            return;
        }

        if (pipe.pipeType == pipeType.Leak)
        {
            FinishPipe.Instance.leakedPipes.Add(pipe);
        }

        Vector3 newDir = pipe.getOutputDirection(transform.position, direction);

        if (newDir == Vector3.zero)
        {
            die();
            return;
        }
        direction = newDir.normalized;
    }

    private void die()
    {
        FinishPipe.Instance.notifyDropDestroyed();
        Destroy(gameObject);
    }
}
