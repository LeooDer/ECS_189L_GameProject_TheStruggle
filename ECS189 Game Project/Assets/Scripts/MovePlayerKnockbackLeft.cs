using UnityEngine;

namespace Player.Command
{
    public class MovePlayerKnockbackLeft : ScriptableObject, IPlayerCommand
    {
        private float KnockbackStrength = 400.0f;
        private Vector2 KnockbackLeft = new Vector2(Vector2.left.x, Vector2.up.y);

        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                var KnockbackDirection = KnockbackStrength * KnockbackLeft;
                rigidBody.velocity = Vector2.zero;
                rigidBody.AddForce(KnockbackDirection);
            }
        }

    }
}
