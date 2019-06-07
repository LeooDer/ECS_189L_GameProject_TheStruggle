using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private Dictionary<int, double> Health = new Dictionary<int, double>();
    private int itrKey = 2;

    // Start is called before the first frame update
    public int Add(double health)
    {
        if(!Health.ContainsKey(itrKey))
        {
            Health.Add(itrKey,health);
            int returnKey = itrKey;
            itrKey++;
            return returnKey;
        }
        else
        {
            return -1;
        }
        
    }
    
    public double Damaged(int Key,double damage)
    {
        if(Health.ContainsKey(Key))
        {
            double currentHealth = Health[Key];
            if ((currentHealth - damage) > 0)
            {
                Health[Key] = currentHealth - damage;
                return Health[Key];
            }
            else
            {
                Remove(Key);
                return 0;
            }
        }
        return 0;
    }

    public void Remove(int Key)
    {
        Health.Remove(Key); 
    }

}
