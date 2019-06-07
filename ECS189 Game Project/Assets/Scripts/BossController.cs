using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private Rigidbody2D Rigidbody;
    private Transform playerTransform;
    private Animator BossAnimator;

    void Start()
    {
        this.Rigidbody = GetComponent<Rigidbody2D>();
        this.playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        this.BossAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private bool IsPlayerInFront()
    {
        if (this.playerTransform.position.x < this.transform.position.x)
            return true;
        else
            return false;
    }
}
