
using UnityEngine;

namespace Player.Command
{
    public class MovePlayerJumpMovement : ScriptableObject, IPlayerCommand
    {
        private float VerticalSpeed = 11.0f;
        private AudioManager AudioMan;

        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, VerticalSpeed);

                // Play jump sound
                if (AudioMan == null)
                    AudioMan = FindObjectOfType<AudioManager>();
                AudioMan.Play("JumpSound");
            }
        }

    }
}

