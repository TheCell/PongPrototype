using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    private GameObject ballPrefab;
    private Color color1 = Color.white;
    private Color color2 = Color.black;
    private bool isWhiteColor;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.color == Color.white)
        {
            isWhiteColor = true;
        }
        else
        {
            spriteRenderer.color = color2;
        }
    }

    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collisionEnter");

        //gameObject.layer = gameObject.layer == 6 ? 7 : 6;
        if (collision.gameObject.GetComponent<Ball>() == null)
        {
            return;
        }

        ToggleColor();
        if (isWhiteColor)
        {
            gameObject.layer = 7;
        }
        else
        {
            gameObject.layer = 6;
        }

        Vector2 velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
        SpawnBall(velocity);
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log("collisionExit");
        //toggleColor();
    }

    private void ToggleColor()
    {
        isWhiteColor = !isWhiteColor;
        spriteRenderer.color = isWhiteColor ? color1 : color2;
    }

    private void SpawnBall(Vector2 velocity)
    {
        Debug.Log("SpawnBall by collision");
        var ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        Debug.Log("SpawnBall StartLogic by collision");
        ball.GetComponent<Ball>().StartLogic(velocity, isWhiteColor);
    }
}
