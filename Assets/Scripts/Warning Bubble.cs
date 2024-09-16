using UnityEngine;

public class WarningBubble : MonoBehaviour
{
    public Vector3Position playerPosition;
    public GameObject enemy;
    public GameObject pointer;
    private Vector2 pointerDirection;
    private void Start()
    {
        //because unfortunately being a child inherits parent's transform, scale by inverse of parent
        Vector3 parentScale = transform.parent.localScale;
        Vector3 inverseScale = new Vector3(1 / parentScale.x, 1 / parentScale.y, 1);
        transform.localScale = inverseScale;

    }
    private void LateUpdate()
    {
        moveCircle();
        rotateToDirection(enemy.transform.position - playerPosition.pos);

    }

    private void changeAlpha(float value)
    {
        SpriteRenderer bubbleRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer pointerRenderer = pointer.GetComponent<SpriteRenderer>();
        Color temp = bubbleRenderer.color;

        float newAlpha = temp.a + value;
        newAlpha = Mathf.Min(newAlpha, 1);
        newAlpha = Mathf.Max(newAlpha, 0);
        temp.a = newAlpha;
        bubbleRenderer.color = temp;
        pointerRenderer.color = temp;
    }
    private void moveCircle()
    {
        RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, playerPosition.pos - enemy.transform.position, 100, 1 << LayerMask.NameToLayer("CameraBox"),
            -100, 100);
        Vector2 hitlocation = hit.point;
        if ((hitlocation - (Vector2) enemy.transform.position).magnitude < 0.1f)
        {
            changeAlpha(-Time.deltaTime * 3);
 
        }
        else
        {
            
            changeAlpha(Time.deltaTime * 3);
         
         
        }
   
        this.transform.position = hitlocation;
        
    }
    float AngleBetween(Vector2 a, Vector2 b)
    {
        // Get a vector rotated 90 degrees from a.
        Vector2 perpendicular = new Vector2(a.y, -a.x);

        // Compute a scaled projection of b onto the original and rotated version.
        float x = Vector2.Dot(a, b);
        float y = Vector2.Dot(perpendicular, b);

        // Treat these as a point in a coordinate system where a is the x axis,
        // and perpendicular is the y axis, and get the polar angle of that point.
        return Mathf.Rad2Deg * (Mathf.Atan2(y, x));
    }
    public void rotateToDirection(Vector2 newDirection)
    {

        checkDirection();
        pointer.transform.RotateAround(this.transform.position, Vector3.forward, AngleBetween(newDirection, pointerDirection));
        //this.transform.position = Quaternion.FromToRotation(direction, newDirection) * this.transform.position;
    }
    void checkDirection()
    {
        pointerDirection = pointer.transform.localPosition.normalized;
    }
}
