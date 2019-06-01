using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

namespace Player.Command
{
    public class MovePlayerJumpMovement : ScriptableObject, IPlayerCommand
    {
        private float VerticalSpeed = 1.0f;

        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, VerticalSpeed);
            }
        }

    }
}

