using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject ProjectilePrefab;

    private IPlayerCommand Right;
    private IPlayerCommand Left;
    private IPlayerCommand Jump;
    private IPlayerCommand Knockback;
    private float SpeedFactor = 50.0f;

    // To keep track of the different states the player can be in
    private enum State { Grounded, Jumping, Hurt };
    private State currentState;
    

    // To keep track of direction the player can 
    private enum Direction { Left, Right };
    private Direction currentDirection;

    // Start is called before the first frame update
    void Start()
    {
        this.Right = ScriptableObject.CreateInstance<MovePlayerRightMovement>();
        this.Left = ScriptableObject.CreateInstance<MovePlayerLeftMovement>();
        this.Jump = ScriptableObject.CreateInstance<MovePlayerJumpMovement>();
        this.Knockback = ScriptableObject.CreateInstance<MovePlayerKnockbackMovement>();
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
        if (Input.GetButton("Fire1"))
        {
            var projectile = (GameObject)Instantiate(ProjectilePrefab, gameObject.transform.localPosition, gameObject.transform.rotation);
            var projectileRigidBody = projectile.GetComponent<Rigidbody>();
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
            this.currentState = State.Grounded;
        }

        // Apply knockback if we touch an enemy
        if (collision.gameObject.tag == "Enemy")
        {
            this.Knockback.Execute(this.gameObject);
            this.currentState = State.Hurt;
        }
    }
}
