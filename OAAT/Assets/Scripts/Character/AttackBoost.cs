using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoost : MonoBehaviour
{
    public float buff = 1.5f;
    public int duration = 3;
    private AttackLogic attackLogic;
    private Stats stats;
    private void Start()
    {
        attackLogic = GetComponent<AttackLogic>();
        stats = GetComponent<Stats>();
    }
    public void Activate()
    {
        if (attackLogic.attacksLeft > 0)
        {

            stats.buff("Boost", buff, duration);
            attackLogic.attackUse();

        }
        else
        {
            Debug.Log("No attacks available");
        }
    }
}
