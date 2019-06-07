using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private Dictionary<int, double> Health = new Dictionary<int, double>();
    private int key = 0;

    // Start is called before the first frame update
    public int Add(double health)
    {
        if(Health.ContainsKey(key) == false)
        {
            key++;
            Health.Add(key,health);
            return key;
        }
        return -1;
    }
    
    public double Damaged(int key,double damage)
    {
        double currentHealth = Health[key];
        if ((currentHealth - damage) > 0)
        {
            Health[key] = currentHealth - damage;
            return Health[key];
        }
        else
        {
            Remove(key);
            return 0;
        }
    }

    public void Remove(int key)
    {
        Health.Remove(key); 
    }

}
