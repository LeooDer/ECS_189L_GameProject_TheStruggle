using UnityEngine;

namespace Player.Command
{
    public class MovePlayerKnockbackMovement : ScriptableObject, IPlayerCommand
    {
        private float KnockbackStrength = 800.0f;
        private Vector2 KnockbackRight = new Vector2(Vector2.right.x, Vector2.up.y);
        private Vector2 KnockbackLeft = new Vector2(Vector2.left.x, Vector2.up.y); 

        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                var KnockbackDirection = KnockbackStrength * KnockbackLeft;
                Debug.Log(KnockbackDirection);
                rigidBody.AddForce(KnockbackDirection);
            }
        }

    }
}
