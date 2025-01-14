using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float health = 100;
    public float baseAttack = 5;
    public float attack = 5;
    public float defense = 3;
    private float buffs;
    public void buff(float percent, int duration)
    {
        attack *= percent;
    }

}
