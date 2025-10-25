using UnityEngine;

public class NPCPatrol : MonoBehaviour
{
    public float speed = 1f;
    public Transform[] points;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private int i;

    private bool interacting = false;
    private Transform player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!interacting)
        {
            patrol();
        }
        
    }

    private void patrol()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.25f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }

        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        spriteRenderer.flipX = (transform.position.x - points[i].position.x) > 0f;
    }

    public void StartInteraction()
    {
        interacting = true;
        rb.linearVelocity = Vector2.zero;

        if(player !=null)
        {
            bool playerOnLeft = player.position.x < transform.position.x;
            spriteRenderer.flipX = playerOnLeft;
        }
       
    }

    public void EndInteraction()
    {
        interacting = false;
        
    }
}


