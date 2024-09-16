using UnityEngine;
using UnityEngine.Events;

public class Hitbox : MonoBehaviour
{

    [SerializeField] private UnityEvent<float> damagePlayer;
    public bool hitBoxEnabled;
    public float damage;
    public bool damaged;
    public bool canDamage;
    public Vector2 direction;
    void OnTriggerEnter2D(Collider2D col) {
       
        if (col.CompareTag("Player") && !damaged)
        {
            canDamage = true;
            dealDamagePlayer();
            damaged = true;

        }
    }
    void OnTriggerExit2D(Collider2D col)
    {

        if (col.CompareTag("Player"))
        { 
            enableHitbox(false);
            canDamage = false;
  
        }
    }
    public void dealDamagePlayer()
    {
        if (canDamage)
        {
            damagePlayer.Invoke(damage);
            enableHitbox(false);
        }

    }     
    public void enableHitbox(bool enabledhitBox)
    {
  
            GetComponent<Collider2D>().enabled = enabledhitBox;
        
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
    void checkDirection()
    {
        direction = this.transform.localPosition.normalized;
    }

   public void rotateToDirection(Vector2 newDirection)
    {
        
        checkDirection();
        transform.RotateAround(this.transform.parent.position, Vector3.forward, AngleBetween(newDirection, direction));
        //this.transform.position = Quaternion.FromToRotation(direction, newDirection) * this.transform.position;
    }
}
