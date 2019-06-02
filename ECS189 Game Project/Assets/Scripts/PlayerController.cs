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

    
    // Start is called before the first frame update
    void Start()
    {
        this.Right = ScriptableObject.CreateInstance<MovePlayerRightMovement>();
        this.Left = ScriptableObject.CreateInstance<MovePlayerLeftMovement>();
        this.Jump = ScriptableObject.CreateInstance<MovePlayerJumpMovement>();
        this.Fire1 = new ShootCommand();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0.01)
        {
            Debug.Log("Move Right");
            this.Right.Execute(this.gameObject);
        }
        if (Input.GetAxis("Horizontal") < -0.01)
        {
            Debug.Log("Move Left");
            this.Left.Execute(this.gameObject);
        }

        if (Input.GetButton("Jump"))
        {
            Debug.Log("Move Jump");
            this.Jump.Execute(this.gameObject);
        }
        if (Input.GetButton("Fire1"))
        {
            this.Fire1.Execute(this.gameObject);
        }
    }
}
