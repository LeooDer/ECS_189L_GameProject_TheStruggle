using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

namespace Player.Command
{
    public class ShootCommand : MonoBehaviour //ScriptableObject, IPlayerCommand
    {
        
        void Start()
        {
            Destroy(gameObject, 1.0f);
        }

        //Function will be called when this object hits an object with a collider
        void OnCollisionEnter(Collision collision)
        {
            //Destroy this gameobject
            Destroy(gameObject);
        }

    }
}

