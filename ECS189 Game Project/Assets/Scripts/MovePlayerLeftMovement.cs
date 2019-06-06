using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

namespace Player.Command
{
    public class MovePlayerLeftMovement : ScriptableObject, IPlayerCommand
    {
        private float Speed = 6.0f;
        private float SprintMultiplier = 1.5f;

        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                // Get Input value
                float x = Input.GetAxis("Horizontal");

                // If holding down sprint button, go faster
                if (Input.GetButton("Fire3"))
                    rigidBody.velocity = new Vector2(x * this.Speed * SprintMultiplier, rigidBody.velocity.y);
                else
                    rigidBody.velocity = new Vector2(x * this.Speed, rigidBody.velocity.y);

                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                gameObject.GetComponent<Animator>().Play("Player-Walking");
            }
        }
    }
}

