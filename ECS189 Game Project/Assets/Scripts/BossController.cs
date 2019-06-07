using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField]
    private GameObject ProjectilePrefab1;
    [SerializeField]
    private GameObject ProjectilePrefab2;
    [SerializeField]
    private GameObject ProjectilePrefab3;
    [SerializeField]
    private float ProjectileSpeed;
    [SerializeField]
    private double Health;
    private HealthManager healthManager;
    private Animator BossAnimator;
    private float Attack1Time = 3;
    private float Attack2Time = 5;
    private float Attack3Time = 1;
    private float Attack1TimeCounter = 0;
    private float Attack2TimeCounter = 0;
    private float Attack3TimeCounter = 0;
    private float AttackCycle = 4;
    private float AttackTime = 0;
    private int key;

    void Awake()
    {
        this.healthManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<HealthManager>();
        key = this.healthManager.Add(Health);
        this.BossAnimator = GetComponent<Animator>();
        this.BossAnimator.Play("Boss-Idle");
    }

    // Update is called once per frame
    void Update()
    {
        if (Attack1TimeCounter > Attack1Time)
        {
            Attack1();
            Attack1TimeCounter = 0;
        }
        if (Attack2TimeCounter > Attack2Time)
        {
            Attack2();
            Attack2TimeCounter = 0;
        }
        if (Attack3TimeCounter > Attack3Time)
        {
            Attack3();
            Attack3TimeCounter = 0;
        }
        Attack1TimeCounter += Time.deltaTime;
        Attack2TimeCounter += Time.deltaTime;
        Attack3TimeCounter += Time.deltaTime;
}

    void Attack1()
    {
        this.BossAnimator.Play("Boss-Attack2");
        for (double i = -10; i < 10; i += 2)
        {
            var projectile = (GameObject)Instantiate(ProjectilePrefab1, gameObject.transform.localPosition + new Vector3(0, 1, 0), gameObject.transform.rotation);
            var rigidbody = projectile.GetComponent<Rigidbody2D>();
            rigidbody.velocity = new Vector2(-1 * ProjectileSpeed, (float)i);
        }
    }

    void Attack2()
    {
        this.BossAnimator.Play("Boss-Attack");
        var projectile = (GameObject)Instantiate(ProjectilePrefab2, gameObject.transform.localPosition + new Vector3(0, 1, 0), gameObject.transform.rotation);
        var rigidbody = projectile.GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(-1 * ProjectileSpeed, 0);
    }

    void Attack3()
    {

        var projectile = (GameObject)Instantiate(ProjectilePrefab3, gameObject.transform.localPosition + new Vector3(Random.Range(-10,0), 7, 0), ProjectilePrefab3.transform.rotation);
        var rigidbody = projectile.GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(0, -2);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "FriendlyProjectile" || collider.gameObject.tag == "Ground")
        {
            double currentHealth = this.healthManager.Damaged(key, 5.0);
            Debug.Log(currentHealth);
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
