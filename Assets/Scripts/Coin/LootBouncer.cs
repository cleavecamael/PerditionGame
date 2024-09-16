using System.Collections;
using System.Linq;
using UnityEngine;

public class LootBouncer : MonoBehaviour
{
    public float horizontalForce = 3;
    public float velocity = 15;
    public float yDrag = 1.5f;
    public float xDrag = 1.5f;
    public float gravity = -30;
    public int numberOfBounces = 3;
    public string[] colliderTags;
    private Vector2 groundVelocity;
    private float verticalVelocity, afterVelocity;
    private int bounces = 0;
    bool isGrounded = true;
    private bool collide;
    private Transform transformParent;
    private Transform transformBody;
    private Transform transformShadow;

    void Initialize(Vector2 groundvelocity)
    {
        isGrounded = false;
        afterVelocity /= yDrag;
        groundVelocity = groundvelocity;
        verticalVelocity = afterVelocity;
        bounces++;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (colliderTags.Contains(col.tag))
            collide = true;
    }

    void Awake()
    {
        CreateParent();
        CreateShadow();
        SimulateDrop();
    }
    void Update()
    {
        UpdatePosition();
    }
    private void UpdatePosition()
    {
        if (!isGrounded)
        {
            verticalVelocity += gravity * Time.deltaTime;

            if (!collide)
            {
                transformParent.position += (Vector3)groundVelocity * Time.deltaTime;
            }
            transformBody.position += new Vector3(0, verticalVelocity, 0) * Time.deltaTime;


            CheckGroundHit();
        }
    }
    public void SimulateDrop()
    {
        StartCoroutine(Simulate());
    }
    private void CheckGroundHit()
    {
        if (transformBody.position.y < transformShadow.position.y)
        {
            transformBody.position = transformShadow.position;

            if (bounces < numberOfBounces)
            {
                Initialize(new Vector2(groundVelocity.x / xDrag, groundVelocity.y / xDrag));
            }
            else
            {
                transformShadow.gameObject.SetActive(false);
                isGrounded = true;
            }
        }
    }

    private IEnumerator Simulate()
    {
        groundVelocity = new Vector2(Random.Range(-horizontalForce, horizontalForce), Random.Range(-horizontalForce, horizontalForce));
        verticalVelocity = Random.Range(velocity - 1, velocity);
        afterVelocity = verticalVelocity;
        Initialize(groundVelocity);

        yield return null;
    }

    void CreateParent()
    {
        transformBody = transform;
        transformParent = new GameObject().transform;
        transformParent.SetPositionAndRotation(transform.position, Quaternion.identity);
        transformParent.gameObject.name = $"{transform.gameObject.name}Parent";
        transformBody.parent = transformParent;
    }

    void CreateShadow()
    {
        transformShadow = new GameObject().transform;
        transformShadow.parent = transformParent;
        transformShadow.gameObject.name = "Shadow";
        transformShadow.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

}
