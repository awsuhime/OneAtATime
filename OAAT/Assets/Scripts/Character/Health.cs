using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool ally = false;
    public float maxHealth = 100;
    private float health;
    public void Start()
    {
        health = maxHealth;
    }
    public void takeDamage(float damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " health: " + health);
    }
}
