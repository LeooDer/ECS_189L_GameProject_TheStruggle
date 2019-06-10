using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [SerializeField]
    private GameObject ProjectilePrefab1;
    [SerializeField]
    private GameObject ProjectilePrefab2;
    [SerializeField]
    private GameObject ProjectilePrefab3;
    [SerializeField]
    private float Projectile1Speed;
    [SerializeField]
    private float Projectile2Speed;
    [SerializeField]
    private double Health;
    [SerializeField]
    private GameObject BossRoom;
    private HealthManager healthManager;
    private Animator BossAnimator;
    private float Attack1Time = 3;
    private float Attack2Time = 5;
    private float Attack3Time = 1;
    private float Attack1TimeCounter = 0;
    private float Attack2TimeCounter = 0;
    private float Attack3TimeCounter = 0;
    private float Attack3LastPosition;
    private int key;
    private bool Active;

    public Image healthBar;

    void Awake()
    {
        Active = false;
        this.healthManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<HealthManager>();
        key = this.healthManager.Add(Health);
        this.BossAnimator = GetComponent<Animator>();
        this.BossAnimator.Play("Boss-Idle");
    }

    // Update is called once per frame
    void Update()
    {
        Active = BossRoom.GetComponent<BossRoomController>().getState();
        if (Active)
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
            if(Attack3TimeCounter == 0)
            {
                Attack3LastPosition = GameObject.FindGameObjectWithTag("Player").transform.position.x;
            }
            Attack1TimeCounter += Time.deltaTime;
            Attack2TimeCounter += Time.deltaTime;
            Attack3TimeCounter += Time.deltaTime;
        }
}

    void Attack1()
    {
        this.BossAnimator.Play("Boss-Attack2");
        for (double i = -10; i < 10; i += 2)
        {
            var projectile = (GameObject)Instantiate(ProjectilePrefab1, gameObject.transform.localPosition + new Vector3(0, 1, 0), gameObject.transform.rotation);
            var rigidbody = projectile.GetComponent<Rigidbody2D>();
            rigidbody.velocity = new Vector2(-1 * Projectile1Speed, (float)i);
        }
    }

    void Attack2()
    {
        AudioManager.instance.Play("BossAttack2");
        this.BossAnimator.Play("Boss-Attack");
        var projectile = (GameObject)Instantiate(ProjectilePrefab2, gameObject.transform.localPosition + new Vector3(0, 1, 0), gameObject.transform.rotation);
        var rigidbody = projectile.GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(-1 * Projectile2Speed, 0);
    }

    void Attack3()
    {
        Vector3 pos = new Vector3(Attack3LastPosition,5,0);
        var projectile = (GameObject)Instantiate(ProjectilePrefab3, pos, ProjectilePrefab3.transform.rotation);
        var rigidbody = projectile.GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(0, -3);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "FriendlyProjectile" || collider.gameObject.tag == "Ground")
        {
            double currentHealth = this.healthManager.Damaged(key, 5.0);
            Debug.Log(currentHealth);

            healthBar.fillAmount = (float)currentHealth / 100;

            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "MainCamera")
        {
            Active = false;
        }
    }
}
