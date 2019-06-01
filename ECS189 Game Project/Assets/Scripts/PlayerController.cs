using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

public class PlayerController : MonoBehaviour
{
    private IPlayerCommand Right;
    private IPlayerCommand Left;
    private IPlayerCommand Jump;

    
    // Start is called before the first frame update
    void Start()
    {
        this.Right = ScriptableObject.CreateInstance<MovePlayerRightMovement>();
        this.Left = ScriptableObject.CreateInstance<MovePlayerLeftMovement>();
        this.Jump = ScriptableObject.CreateInstance<MovePlayerJumpMovement>();
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
    }
}
