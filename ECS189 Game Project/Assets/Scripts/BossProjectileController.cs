using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileController : MonoBehaviour
{
    private double lifeTime;
    private double currentLife;

    void Awake()
    {
        lifeTime = 10.0;
        currentLife = 0.0;
    }

    void Update()
    {
        if (currentLife < lifeTime)
            currentLife += Time.deltaTime;
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
