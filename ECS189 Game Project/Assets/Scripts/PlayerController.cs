using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

public class PlayerController : MonoBehaviour
{
    private IPlayerCommand Right;
    private IPlayerCommand Left;
    private IPlayerCommand Jump;
    private IPlayerCommand Fire1;

    // To keep track of the different states the player can be in
    private enum State { Grounded, Jumping };
    private State currentState;
    
    // Start is called before the first frame update
    void Start()
    {
        this.Right = ScriptableObject.CreateInstance<MovePlayerRightMovement>();
        this.Left = ScriptableObject.CreateInstance<MovePlayerLeftMovement>();
        this.Jump = ScriptableObject.CreateInstance<MovePlayerJumpMovement>();
        this.Fire1 = new ShootCommand();
        this.currentState = State.Grounded;
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
        }
    }

    private void GetInput()
    {
        if (Input.GetAxis("Horizontal") > 0.01)
        {
            this.Right.Execute(this.gameObject);
        }
        if (Input.GetAxis("Horizontal") < -0.01)
        {
            this.Left.Execute(this.gameObject);
        }
        if (Input.GetButton("Fire1"))
        {
            this.Fire1.Execute(this.gameObject);
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
    }
}
