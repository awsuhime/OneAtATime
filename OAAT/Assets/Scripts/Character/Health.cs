using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool ally = false;
    public float maxHealth = 100;
    private float health;
    private bool kb = false;
    private float kbStart;
    private float kbEnd;
    public float comboBonusMultiplier;
    public float comboBonus;

    private float immuneStart;
    public float immunity = 0.2f;
    private bool immune = false;

    public GameObject damagePopup;
    private DamPopScript damPop;
    public TurnManager turnManager;
    public void Start()
    {
        health = maxHealth;
        turnManager = FindObjectOfType<TurnManager>();
    }

    public void Update()
    {
        if (immune)
        {
            float immuneLeft = Time.time - immuneStart;
            if (immuneLeft > immunity)
            {
                immune = false;
            }
        }
        if (kb)
        {
            float kbLeft = Time.time - kbStart;
            float speed = Mathf.Log(kbLeft * -1 + (kbEnd + 1), 1.4f);
            transform.Translate(4 * speed * Vector2.right * Time.deltaTime);

            if (kbLeft > kbEnd)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                kb = false;
                turnManager.removeKnockback();
            }

        }
    }
    public void takeDamage(float damage)
    {
        if (!immune)
        {
            float damageTaken = Mathf.Round(Random.Range(0.85f, 1.15f) * damage + comboBonus);
            health -= damageTaken;
            Debug.Log(gameObject.name + " health: " + health);
            GameObject popup = Instantiate(damagePopup, transform.position, Quaternion.identity);
            damPop = popup.GetComponent<DamPopScript>();
            damPop.damageNumber(damageTaken);
            comboBonus += damage / 5 * comboBonusMultiplier;
            
        }
        
    }

    public void takeKnockback(Vector3 dir, float knockback)
    {
        if (!immune)
        {
            if (kb)
            {
                turnManager.removeKnockback();
            }
            Vector3 rotation = transform.position - dir;
            float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotz);
            kbEnd = knockback;
            kb = true;
            kbStart = Time.time;
        }
        else
        {
            turnManager.removeKnockback();
        }


    }

    public void simpleKnockback(Vector3 simpleRot, float knockback)
    {
        if (!immune)
        {
            if (kb)
            {
                turnManager.removeKnockback();
            }
            transform.rotation = Quaternion.Euler(simpleRot);
            kbEnd = knockback;
            kb = true;
            kbStart = Time.time;
        }
        else
        {
            turnManager.removeKnockback();

        }
    }

    public void Immunity()
    {
        immune = true;
        immuneStart = Time.time;
    }

    public void resetCombo()
    {
        comboBonus = 0;
    }
}
