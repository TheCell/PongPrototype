using UnityEngine;

public class BallWall : MonoBehaviour
{
    [SerializeField]
    private Color color1;
    [SerializeField]
    private Color color2;
    [SerializeField]
    private bool showDebug;

    private Vector2 velocity;
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private TextMesh text;

    private bool invertColor = false;
    private bool ignoreCollisions = false;
    private int activeCollision = 0;
    private bool enableMovementTransfer = false;

    void Start()
    {
        rigidbody = transform.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        text = gameObject.GetComponentInChildren<TextMesh>();

        velocity = new Vector2(20f, Random.Range(40f, 30f));
        rigidbody.AddForce(velocity);
    }

    void Update()
    {
    }

    public void FixedUpdate()
    {
        text.text = activeCollision.ToString();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BallWall>() != null)
        {
            activeCollision++;
            if (showDebug)
            {
                Debug.Log(activeCollision);
            }
            //if (ignoreCollisions)
            //{
            //    return;
            //}

            //ignoreCollisions = true;
            invertColor = !invertColor;
            if (invertColor)
            {
                spriteRenderer.color = color1;
            }
            else
            {
                spriteRenderer.color = color2;
            }

            if (enableMovementTransfer)
            {
                SwapMovement(collision.gameObject.GetComponent<Rigidbody2D>());
            }
        }

        if (collision.gameObject.tag == "EnableMovementTransfer")
        {
            enableMovementTransfer = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BallWall>() != null)
        {
            enableMovementTransfer = false;
            activeCollision--;
            if (showDebug)
            {
                Debug.Log(activeCollision);
            }
            if (activeCollision == 0)
            {
                ignoreCollisions = false;
            }
        }
    }

    private void SwapMovement(Rigidbody2D otherBall)
    {
        otherBall.constraints = RigidbodyConstraints2D.FreezeRotation;
        otherBall.velocity = rigidbody.velocity;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
