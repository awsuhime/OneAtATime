using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stats : MonoBehaviour
{
    private Dictionary<string, int> buffs = new Dictionary<string, int>();
    private Dictionary<string, float> percents = new Dictionary<string, float>();

    public float health = 100;
    public float baseAttack = 5;
    public float attack = 5;
    public float defense = 3;
    
    public void buff(string name, float percent, int duration)
    {
        if (!buffs.ContainsKey(name))
        {
            attack *= percent;
            buffs[name] = duration;
            percents[name] = percent;
        }
        else
        {
            buffs[name] = duration;
        }
    }
    public void countdown()
    {
        foreach (string i in buffs.Keys.ToList())
        {
            if (buffs[i] > 1)
            {
                buffs[i]--;
            }
            else
            {
                attack /= percents[i];
                buffs.Remove(i);
                percents.Remove(i);
            }
        }
    }
}
