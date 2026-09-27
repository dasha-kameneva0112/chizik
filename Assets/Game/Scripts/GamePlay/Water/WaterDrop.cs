using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class WaterDrop : MonoBehaviour
{
    public float flowforce = 1f;
    public float gravityForce = 1f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void FixedUpdate()
    {
        Vector2 force = Vector2.zero;

        force += Vector2.down * gravityForce;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f);
        if (hit.collider != null)
        {
            PipeSegment pipe = hit.collider.GetComponent<PipeSegment>();
            if (pipe != null)
            {
                Vector2Int dir = pipe.mainFlowDirection;
                force += (Vector2)dir * flowforce;
            }
        }

        rb.AddForce(force, ForceMode2D.Force);
    }
}
