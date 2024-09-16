using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Vector3Position playerPosition;
    [SerializeField] private Collider2DSO playerCollider;

    private AudioSource playerSource;
    private Rigidbody2D rb;
    private Vector2 moveTarget;
    public Animator bodyAnimator;
    private bool inWater;
    private bool isShooting;
    private bool moving;
    private float waterDrag = 0.5f;
    public ParticleSystem dust;
    string currentSceneName;
    private bool onLavaCheck = false;
    public UnityEvent<float> onLavaDamage;
    public UnityEvent<bool> onDodgeEvent;
    private bool canDodge = true;
    private bool dodgeState = false;
    private bool stunned = false;
    private Vector2 knockbackDirection;

    void Awake()
    {
        playerSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        inWater = false;
        playerCollider.col = GetComponent<Collider2D>();
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    void FixedUpdate()
    {
        if (!stunned)
        {
            if (dodgeState)
                UpdateDodge();
            else
                UpdateMovement();
        }
        else
        {
            updateKnockback();
        }

    }

    public void playDamagedClip()
    {
        AudioManager.playClip(playerSource, "PlayerHit");
    }

    IEnumerator OnDamageFromLava()
    {
        if (!onLavaCheck)
        {
            onLavaCheck = true;
            onLavaDamage.Invoke(3);
            yield return new WaitForSecondsRealtime(0.333f);
            onLavaCheck = false;
        }
    }

    void updateKnockback()
    {
        rb.velocity = knockbackDirection;
        playerPosition.pos = transform.position;
    }
    void UpdateMovement()
    {
        float effSpeed = inWater ? playerStats.movementSpeed * waterDrag : playerStats.movementSpeed;
        effSpeed = isShooting ? effSpeed * playerStats.shootDrag : effSpeed;
        rb.MovePosition(rb.position + moveTarget * effSpeed * Time.fixedDeltaTime);

        playerPosition.pos = transform.position;
        if (moving)
        {
            AudioManager.playRandomClip(playerSource, new string[] { "Moving", "Moving2", "Moving3" });
            CreateDust();
        }
    }
    public void knockBack(Vector2 force)
    {
        Debug.Log("stunned'");
        Debug.Log(force);
        stunned = true;
        StartCoroutine(stunKnockback());
        knockbackDirection = force;
        playerPosition.pos = transform.position;
    }
    IEnumerator stunKnockback()
    {

        yield return new WaitForSeconds(0.2f);
        stunned = false;
    }
    void UpdateDodge()
    {
        float effSpeed = inWater ? playerStats.dodgeSpeed * waterDrag : playerStats.dodgeSpeed;
        effSpeed = isShooting ? effSpeed * playerStats.shootDrag : effSpeed;
        // Debug.Log(moveTarget * effSpeed);
        rb.velocity += moveTarget * effSpeed;
        playerPosition.pos = transform.position;
    }

    IEnumerator Dodge()
    {
        dodgeState = true;
        // gameObject.layer = 17;
        gameObject.tag = "PlayerDodge";
        bodyAnimator.SetBool("Dodge", dodgeState);
        yield return new WaitForSecondsRealtime(playerStats.dodgeDuration);
        dodgeState = false;
        bodyAnimator.SetBool("Dodge", dodgeState);
        // gameObject.layer = 7;
        gameObject.tag = "Player";
        rb.velocity = Vector2.zero;
    }

    IEnumerator DodgeCoolDown()
    {
        canDodge = false;
        onDodgeEvent.Invoke(canDodge);
        StartCoroutine(Dodge());
        yield return new WaitForSecondsRealtime(playerStats.dodgeCooldown);
        canDodge = true;
        onDodgeEvent.Invoke(canDodge);
    }
    public void DodgeMove()
    {
        if (canDodge)
        {
            AudioManager.playClip(playerSource, "Dodge");
            StartCoroutine(DodgeCoolDown());
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (currentSceneName == "World-2" && col.CompareTag("Water"))
            StartCoroutine(OnDamageFromLava());
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Water")) inWater = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Water")) inWater = false;
    }

    public void ToggleShootingDrag(bool trig)
    {
        isShooting = trig;
    }

    public void SetShootDrag(float drag)
    {
        playerStats.SetShootDrag(drag);
    }

    public void ResetShootDrag()
    {
        playerStats.ResetShootDrag();
    }

    public void OnMove(Vector2 pos)
    {
        moveTarget = pos;
        if (moveTarget.magnitude > 0)
        {
            bodyAnimator.SetBool("Moving", true);
            moving = true;

        }
        else
        {
            bodyAnimator.SetBool("Moving", false);
            moving = false;
        }
    }

    void CreateDust()
    {
        dust.Play();
    }
}