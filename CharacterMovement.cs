using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    private Rigidbody2D rb2D;
    private float moveSpeed;
    private float jumpForce;
    private bool isJumping;
    private float moveHorizontal;
    private float moveVertical;
    public Animator animator;
    bool lookingRight = true;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        rb2D = gameObject.GetComponent<Rigidbody2D>();
        moveSpeed = 7f;
        jumpForce = 25f;
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (!isJumping && (moveHorizontal > 0.1f || moveHorizontal < -0.1f))
        {
            if (moveHorizontal < -0.1f && lookingRight) flip();
            else if (moveHorizontal > 0.1f && !lookingRight) flip();
            rb2D.velocity = new Vector2(moveHorizontal * moveSpeed, 0f);
            animator.SetFloat("Speed", Mathf.Abs(moveHorizontal * moveSpeed));
        }

        if (!isJumping && moveVertical > 0.1f)
        {
            animator.SetBool("IsJumping", true);
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        }

        if (!isJumping && (Mathf.Abs(moveHorizontal) < 0.01f))
        {
            animator.SetFloat("Speed", 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Crate"))
        {
            animator.SetBool("IsJumping", false);
            isJumping = false;
        }
        if(other.gameObject.CompareTag("Cherry"))
        {
            healthBar.SetHealth(maxHealth);
            currentHealth = maxHealth;
        }
        

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spike"))
        {
            animator.SetBool("Hurts", true);
            TakeDamage(1);
            if (currentHealth == 0) Destroy(gameObject);
        }

        if(other.gameObject.CompareTag("Stairs"))
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("Climbs", true);
            rb2D.velocity = new Vector2(0f, moveVertical * 5);

        }
        if (other.gameObject.CompareTag("Hop"))
        {
            isJumping = true;
            animator.SetBool("IsJumping", true);
            rb2D.AddForce(new Vector2(0f, jumpForce * 1.1f), ForceMode2D.Impulse);
        }
        if (other.gameObject.CompareTag("Crate"))
        {
            isJumping = false;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            isJumping = true;
        }
        if(other.gameObject.CompareTag("Spike"))
        {
            animator.SetBool("Hurts", false);
        }
        if (other.gameObject.CompareTag("Stairs"))
        {
            animator.SetBool("Climbs", false);
            if (moveVertical > 0.1f)
            {
                animator.SetBool("IsJumping", true);
                rb2D.AddForce(new Vector2(moveHorizontal*moveSpeed, moveVertical * jumpForce), ForceMode2D.Impulse);
            }
        }
        if (other.gameObject.CompareTag("Crate"))
        {
            if (moveVertical > 0.01f)
            {
                isJumping = true;
                rb2D.AddForce(new Vector2(0f, moveVertical * 8f), ForceMode2D.Impulse);
            }
            else if(moveVertical < 0.01f && moveHorizontal > 0.01f)
            {
                rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 10f), ForceMode2D.Impulse);
            }
        }
    }

    void flip()
    {
        lookingRight = !lookingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

}
