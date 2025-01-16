using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stats : MonoBehaviour
{
    //Attack
    private Dictionary<string, int> attackBuffs = new Dictionary<string, int>();
    private Dictionary<string, float> attackPercents = new Dictionary<string, float>();

    //Defense

    //DoT
    private Dictionary<string, int> DoT = new Dictionary<string, int>();
    private Dictionary<string, float> DoTDamage = new Dictionary<string, float>();

    private Health health;
    public float baseAttack = 5;
    public float attack = 5;
    public float baseDefense = 3;
    public float defense = 3;
    private void Start()
    {
        if (GetComponent<Health>() != null)
        {
            health = GetComponent<Health>();

        }
        attack = baseAttack;
        defense = baseDefense;
    }
    public void buff(string name, float percent, int duration)
    {
        if (!attackBuffs.ContainsKey(name))
        {
            attack *= percent;
            attackBuffs[name] = duration;
            attackPercents[name] = percent;
        }
        else
        {
            attackBuffs[name] = duration;
        }
    }
    public void dealDoT(string name, float dam, int duration)
    {
        DoT[name] = duration;
        DoTDamage[name] = dam;
    }
    public void countdown()
    {
        //Attack Buffs
        foreach (string i in attackBuffs.Keys.ToList())
        {
            if (attackBuffs[i] > 1)
            {
                attackBuffs[i]--;
            }
            else
            {
                attack /= attackPercents[i];
                attackBuffs.Remove(i);
                attackPercents.Remove(i);
            }
        }
        //DoT
        foreach (string i in DoT.Keys.ToList())
        {
            if (DoT[i] > 1)
            {
                DoT[i]--;
                health.takeDamage(DoTDamage[i]);
            }
            else
            {
                DoT.Remove(i);
                DoTDamage.Remove(i);
            }
        }

    }
}
