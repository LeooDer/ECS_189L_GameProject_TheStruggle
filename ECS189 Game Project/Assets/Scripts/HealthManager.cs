using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private Dictionary<int, double> Health = new Dictionary<int, double>();

    // Start is called before the first frame update
    public void Add(int key,double health)
    {
        if(Health.ContainsKey(key) == false)
        {
            Health.Add(key, health);
        }
    }
    
    public bool Damaged(int key,double damage)
    {
        if (Health.ContainsKey(key))
        {
            double currentHealth = Health[key];
            Debug.Log(currentHealth);
            if ((currentHealth - damage) > 0)
            {
                Health[key] = currentHealth - damage;
                return true;
            }
            else
            {
                Remove(key);
                return false;
            }
        }
        return true;
    }

    public void Remove(int key)
    {
        Health.Remove(key); 
    }

}
