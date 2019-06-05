using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

namespace Player.Command
{
    public class ShootCommand : ScriptableObject, IPlayerCommand
    {
        private static readonly Object ProjectilePrefab;
        private static float SpeedFactor = 50.0f;

        static ShootCommand()
        {
            ProjectilePrefab = Resources.Load("prefabs/Projectile");
        }

        public void Execute(GameObject gameObject)
        {
            var projectile = (GameObject)Instantiate(ProjectilePrefab, gameObject.transform.localPosition, gameObject.transform.rotation);
            var projectileRigidBody = projectile.GetComponent<Rigidbody>();
            projectileRigidBody.velocity = projectile.transform.right * SpeedFactor;

        }
    }
}

