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
    private float ProjectileSpeed;
    [SerializeField]
    private double Health;
    private HealthManager healthManager;
    private float Attack1Time = 2;
    private float Attack2Time = 4;
    private float AttackCycle = 4;
    private float AttackTime = 0;
    private bool Attack1Executed = false;
    private bool Attack2Executed = false;
    private int key;

    void Awake()
    {
        this.healthManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<HealthManager>();
        key = this.healthManager.Add(Health);
    }

    // Update is called once per frame
    void Update()
    {
        if (AttackTime > 2 && !Attack1Executed)
        {
            Attack1();
            Attack1Executed = true;
        }
        if (AttackTime > 4)
        {
            Attack2();
            Attack2Executed = true;
        }
        if (AttackTime > AttackCycle)
        {
            Attack1Executed = false;
            Attack2Executed = false;
            AttackTime = 0;
            return;
        }
        AttackTime += Time.deltaTime;
    }

    void Attack1()
    {
        for (double i = -10; i < 10; i += 2)
        {
            var projectile = (GameObject)Instantiate(ProjectilePrefab1, gameObject.transform.localPosition + new Vector3(0, 1, 0), gameObject.transform.rotation);
            var rigidbody = projectile.GetComponent<Rigidbody2D>();
            rigidbody.velocity = new Vector2(-1 * ProjectileSpeed, (float)i);
        }
    }

    void Attack2()
    {
        var projectile = (GameObject)Instantiate(ProjectilePrefab2, gameObject.transform.localPosition + new Vector3(-2, -2, 0), gameObject.transform.rotation);
        var rigidbody = projectile.GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(-1 * ProjectileSpeed, 0);
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
