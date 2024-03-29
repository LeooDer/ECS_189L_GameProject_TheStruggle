﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player.Command;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject ProjectilePrefab;
    [SerializeField]
    private double Health = 100;

    private Animator playerAnimator;
    private GameObject projectile;
    private Rigidbody2D projectileRigidBody;

    private IPlayerCommand Right;
    private IPlayerCommand Left;
    private IPlayerCommand Jump;
    private IPlayerCommand KnockbackRight;
    private IPlayerCommand KnockbackLeft;
    private float SpeedFactor = 20.0f; //50.0f;
    private HealthManager healthManager;
    private int key;
    private HUDManager healthBar;

    // To keep track of the different states the player can be in
    private enum State { Grounded, Jumping, Hurt };
    private State currentState;
    

    // To keep track of direction the player can 
    private enum Direction { Left, Right };
    private Direction currentDirection;

    // Start is called before the first frame update
    void Start()
    {
        this.healthBar = GameObject.FindGameObjectWithTag("Manager").GetComponent<HUDManager>();
        this.healthManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<HealthManager>();
        key = this.healthManager.Add(Health);
        this.Right = ScriptableObject.CreateInstance<MovePlayerRightMovement>();
        this.Left = ScriptableObject.CreateInstance<MovePlayerLeftMovement>();
        this.Jump = ScriptableObject.CreateInstance<MovePlayerJumpMovement>();
        this.KnockbackRight = ScriptableObject.CreateInstance<MovePlayerKnockbackRight>();
        this.KnockbackLeft = ScriptableObject.CreateInstance<MovePlayerKnockbackLeft>();
        this.playerAnimator = GetComponent<Animator>();
        this.currentState = State.Grounded;
        this.currentDirection = Direction.Right;
    }

    // Update is called once per frame
    void Update()
    {
        switch (this.currentState)
        {
            case State.Grounded:
                GetInput();
                GetJumpInput();
                break;

            case State.Jumping:
                GetInput();
                break;
            case State.Hurt:
                
                break;
        }

        // Update animations
        SetAnimatorProperties();
    }

    private void GetInput()
    {
        if (Input.GetAxis("Horizontal") > 0f) 
        {
            this.currentDirection = Direction.Right;
            this.Right.Execute(this.gameObject);

        }
        if (Input.GetAxis("Horizontal") < 0f)
        {
            this.currentDirection = Direction.Left;
            this.Left.Execute(this.gameObject);
        }

        if (Input.GetButtonDown("Fire1"))
        {

            switch (this.currentDirection)
            {
                case Direction.Left:
                    projectile = (GameObject)Instantiate(ProjectilePrefab, gameObject.transform.localPosition + new Vector3(-1,0,0) , gameObject.transform.rotation);
                    projectileRigidBody = projectile.GetComponent<Rigidbody2D>();
                    projectileRigidBody.velocity = -1 * projectile.transform.right * SpeedFactor;
                    break;
                case Direction.Right:
                    projectile = (GameObject)Instantiate(ProjectilePrefab, gameObject.transform.localPosition + new Vector3(1, 0, 0), gameObject.transform.rotation);
                    projectileRigidBody = projectile.GetComponent<Rigidbody2D>();
                    projectileRigidBody.velocity = projectile.transform.right * SpeedFactor;
                    break;
            }

            // Play Projectile sound effect
            AudioManager.instance.Play("ProjectileSound");
        }
    }

    private void SetAnimatorProperties()
    {
        var velocity = this.gameObject.GetComponent<Rigidbody2D>().velocity;
        playerAnimator.SetFloat("VelocityX", Mathf.Abs(velocity.x));
        playerAnimator.SetFloat("VelocityY", velocity.y);
    }

    private void GetJumpInput()
    {
        if (Input.GetButtonDown("Jump") )
        {
            this.Jump.Execute(this.gameObject);
            this.currentState = State.Jumping;
        }
    }

    private void PlayWalkSound()
    {
        AudioManager.instance.Play("FootstepSound");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "EnemyProjectile" || collider.gameObject.tag == "Ground")
        {
            double currentHealth = this.healthManager.Damaged(key,10);
            if(currentHealth <= 0)
            {
                healthBar.UpdateHealth(currentHealth);
                Destroy(gameObject);
                GameManager.Instance.ChangeScene("Lose");
            }
            else
            {
                healthBar.UpdateHealth(currentHealth);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If we touch the ground, we're grounded
        if (collision.gameObject.tag == "Ground")
        {
            if (this.currentState == State.Hurt)
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            this.currentState = State.Grounded;
        }

        // Apply knockback if we touch an enemy
        if (collision.gameObject.tag == "Enemy" && this.currentState != State.Hurt)
        {
            // Determine which direction to apply knockback in, depending on position of enemy
            if (collision.gameObject.transform.position.x < this.transform.position.x)
                this.KnockbackRight.Execute(this.gameObject);
            else
                this.KnockbackLeft.Execute(this.gameObject);
            double currentHealth = this.healthManager.Damaged(key,10);
            if(currentHealth <= 0)
            {
                healthBar.UpdateHealth(currentHealth);
                Destroy(gameObject);
                GameManager.Instance.ChangeScene("Lose");
            }
            else
            {
                healthBar.UpdateHealth(currentHealth);
            }
            this.currentState = State.Hurt;
        }
    }

    public double getHealth()
    {
        return Health;
    }
}
