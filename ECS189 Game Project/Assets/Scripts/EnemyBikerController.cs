using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBikerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D RigidBody;
    public float Speed;

    void Start()
    {
        this.RigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Move the 
        this.RigidBody.velocity = new Vector2(Speed * Vector2.right.x, this.RigidBody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If we touch a "wall", enemy changes direction
        if (collision.gameObject.tag == "Ground")
        {
            this.Speed = -this.Speed;
        }
    }
}
