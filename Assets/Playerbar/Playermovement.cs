using UnityEngine;

public class Playermovement : MonoBehaviour
{
    private Vector2 mousePos;
    private float clampedMouseY;
    private float startX;
    private Vector2 worldDimensions;

    void Start()
    {
        worldDimensions = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        startX = transform.position.x;
    }

    void Update()
    {
        //Debug.Log("World Dimensions " + worldDimensions);
        mousePos = Input.mousePosition;
        clampedMouseY = Mathf.Clamp(mousePos.y, 0, Screen.height);
        float normalizedMouseY = clampedMouseY / Screen.height;
        transform.position = new Vector2(startX, Mathf.Lerp(-1, 1, normalizedMouseY) * (worldDimensions.y));
    }
}
