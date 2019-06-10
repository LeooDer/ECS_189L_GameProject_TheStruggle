using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetection : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            Destroy(collider.gameObject);
            GameManager.Instance.ChangeScene("Lose");
        }
        else if(collider.gameObject.tag == "Enemy")
        {
            Destroy(collider.gameObject);
        }
    }
}
