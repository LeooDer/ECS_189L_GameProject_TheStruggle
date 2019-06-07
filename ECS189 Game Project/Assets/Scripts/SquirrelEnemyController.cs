using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelEnemyController : MonoBehaviour
{
    private Rigidbody2D Rigidbody;
    private Transform playerTransform;
    private Animator SquirrelAnimator;

    public float JumpForce;
    public float TimeToJump;
    private float JumpTimer;

    void Start()
    {
        this.Rigidbody = GetComponent<Rigidbody2D>();
        this.playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        this.SquirrelAnimator = GetComponent<Animator>();
        this.JumpTimer = this.TimeToJump;
    }

    void Update()
    {
        this.JumpTimer -= Time.deltaTime;

        if (this.JumpTimer <= 0f)
        {
            this.JumpTimer = this.TimeToJump;

            // Decide which way to make squirrel jump
            var jumpDir = new Vector2(Vector2.left.x, Vector2.up.y);
            if ( !IsPlayerInFront() )
            {
                jumpDir.x = -jumpDir.x;
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().flipX = false;
            }
            this.Rigidbody.AddForce(jumpDir * this.JumpForce);
        }

        this.SquirrelAnimator.SetFloat("VelocityY", this.Rigidbody.velocity.y);
    }

    private bool IsPlayerInFront()
    {
        if (this.playerTransform.position.x < this.transform.position.x)
            return true;
        else
            return false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            this.Rigidbody.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "FriendlyProjectile" || collider.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
