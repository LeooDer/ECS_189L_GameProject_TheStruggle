using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;

    public void Start()
    {
        double Health = GameObject.FindGameObjectWithTag("Player").GetComponent <PlayerController>().getHealth();
        healthBar.maxValue = (float)Health;
        healthBar.value = (float)Health;
    }

    public void UpdateHealth(double health)
    {
        healthBar.value = (float) health;
    }
}
