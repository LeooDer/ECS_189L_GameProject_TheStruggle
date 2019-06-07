using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject ProjectilePrefab;
    [SerializeField]
    private double Health = 100;

    private Animator playerAnimator;

    private IPlayerCommand Right;
    private IPlayerCommand Left;
    private IPlayerCommand Jump;
    private IPlayerCommand KnockbackRight;
    private IPlayerCommand KnockbackLeft;
    private float SpeedFactor = 50.0f;
    private HealthManager healthManager;
    private int key;

    // To keep track of the different states the player can be in
    private enum State { Grounded, Jumping, Hurt };
    private State currentState;
    

    // To keep track of direction the player can 
    private enum Direction { Left, Right };
    private Direction currentDirection;

    // Start is called before the first frame update
    void Start()
    {
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
            var projectile = (GameObject)Instantiate(ProjectilePrefab, gameObject.transform.localPosition, gameObject.transform.rotation);
            var projectileRigidBody = projectile.GetComponent<Rigidbody2D>();
            switch (this.currentDirection)
            {
                case Direction.Left:
                    projectileRigidBody.velocity = -1 * projectile.transform.right * SpeedFactor;
                    break;
                case Direction.Right:
                    projectileRigidBody.velocity = projectile.transform.right * SpeedFactor;
                    break;
            }
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
            Debug.Log("Move Jump");
            this.Jump.Execute(this.gameObject);
            this.currentState = State.Jumping;
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
            if(!(this.healthManager.Damaged(key,10)))
            {
                Debug.Log("Dead");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Damaged");
            }
            this.currentState = State.Hurt;
        }
    }
}
