using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private bool isStartBall = false;
    private Vector2 velocity;
    private Rigidbody2D rigidbody;
    private bool isBlackBall;

    void Awake()
    {
        Debug.Log("ball Awake");
        rigidbody = transform.GetComponent<Rigidbody2D>();
        isBlackBall = gameObject.GetComponent<SpriteRenderer>().color == Color.white ? false : true;

        if (isStartBall)
        {
            float xSpeed = gameObject.layer == 6 ? 40f : -40f;
            velocity = new Vector2(xSpeed, Random.Range(40f, 30f));
            //rigidbody.AddRelativeForce(velocity);
            gameObject.layer = isBlackBall ? 6 : 7;
            StartLogic(velocity, isBlackBall);
        }
    }

    public void StartLogic(Vector2 velocity, bool startAsBlack)
    {
        Debug.Log("ball StartLogic");
        isBlackBall = startAsBlack;

        if (isBlackBall)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }

        rigidbody.velocity = velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.layer != 8)
        {
            isBlackBall = !isBlackBall;
        }
        
        gameObject.layer = isBlackBall ? 6 : 7;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
    }
}
