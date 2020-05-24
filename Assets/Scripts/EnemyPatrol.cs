using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField]
    private GameObject leftBorder;
    [SerializeField]
    private GameObject rightBorder;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private GroundDetection groundDetection;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float speed = 2f;
    public float Speed
    {
        get { return speed; }
        set 
        {
            if (value > 0.5f)
                speed = value;
        }
    }

    private bool isRightDirection;
    private bool stop;


    private void FixedUpdate()
    {
        if (!stop)
        {
            if (isRightDirection && groundDetection.IsGrounded)
            {
                rb.velocity = Vector2.right * speed;
                spriteRenderer.flipX = true;
                if (transform.position.x > rightBorder.transform.position.x)
                    isRightDirection = !isRightDirection;
            }
            else if (groundDetection.IsGrounded)
            {
                spriteRenderer.flipX = false;
                rb.velocity = Vector2.left * speed;
                if (transform.position.x < leftBorder.transform.position.x)
                    isRightDirection = !isRightDirection;
            }
        }
            

    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAtack"))
            {
                animator.SetBool("SetDamageBool", true);

                stop = true;

                if (transform.position.x < player.transform.position.x && !isRightDirection)
                    spriteRenderer.flipX = true;
                else if (transform.position.x > player.transform.position.x && isRightDirection)
                    spriteRenderer.flipX = false;

                if (transform.position.x > player.transform.position.x)
                    rb.AddForce(Vector2.left * 6, ForceMode2D.Impulse);
                else if (transform.position.x < player.transform.position.x)
                    rb.AddForce(Vector2.right * 6, ForceMode2D.Impulse);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAtack"))
        {
            animator.SetBool("SetDamageBool", false);
            stop = false;
        }
            
    }

}
