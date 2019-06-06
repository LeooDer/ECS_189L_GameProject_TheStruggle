using UnityEngine;

namespace Player.Command
{
    public class MovePlayerKnockbackRight : ScriptableObject, IPlayerCommand
    {
        private float KnockbackStrength = 400.0f;
        private Vector2 KnockbackRight = new Vector2(Vector2.right.x, Vector2.up.y);

        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                var KnockbackDirection = KnockbackStrength * KnockbackRight;
                Debug.Log(KnockbackDirection);
                rigidBody.AddForce(KnockbackDirection);
            }
        }

    }
}
