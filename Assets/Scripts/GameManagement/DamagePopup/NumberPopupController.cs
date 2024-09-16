using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class NumberPopupController : MonoBehaviour
{
    [System.Serializable]
    public struct DamageMovement
    {
        public Vector3 finalPosition;
        public float maxSpeed;
        public float deceleration;
    }

    [SerializeField, Tooltip("How fast the text moves up")] private DamageMovement damageMovement;
    [SerializeField, Tooltip("How long the text lasts for")] private float lifetime;
    [SerializeField, Tooltip("How fast the text disappears")] private float fadeSpeed;
    [SerializeField, Tooltip("How much to scale the text")] private float scale;

    private float currentLifetime;
    private Vector3 initialPosition;
    private TextMeshPro damageText;
    private bool dead;

    void Awake()
    {
        dead = false;
        initialPosition = transform.position;
        currentLifetime = lifetime;
        damageText = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        if (currentLifetime > 0)
        {
            Move();
            Scale();

            currentLifetime -= Time.deltaTime;
        }
        else
        {
            if (!dead)
                StartCoroutine(Disappear());
        }
    }

    void Move()
    {
        transform.position += damageMovement.finalPosition * Time.deltaTime;
        damageMovement.finalPosition -= damageMovement.finalPosition * damageMovement.deceleration * Time.deltaTime;
    }

    void Scale()
    {
        if (currentLifetime > lifetime * 0.5f)
        {
            transform.localScale += scale * Time.deltaTime * Vector3.one;
        }
        else
        {
            transform.localScale -= scale * Time.deltaTime * Vector3.one;
        }
    }

    IEnumerator Disappear()
    {
        dead = true;
        Color currColor = damageText.color;
        while (fadeSpeed > 0)
        {
            currColor.a -= fadeSpeed * Time.deltaTime;
            damageText.color = currColor;
            fadeSpeed -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    public void Setup(int damage, int sortingOrder)
    {
        damageText.text = damage.ToString();
        damageText.sortingOrder = sortingOrder;
    }

}