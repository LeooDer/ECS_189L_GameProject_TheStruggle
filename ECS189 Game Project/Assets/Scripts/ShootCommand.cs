using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

namespace Player.Command
{
    public class ShootCommand : MonoBehaviour //ScriptableObject, IPlayerCommand
    {
        [SerializeField] private Rigidbody2D RigidBody;

        void Start()
        {
            Destroy(gameObject, 1.0f);
            this.RigidBody = GetComponent<Rigidbody2D>();
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
        }
    }
}

