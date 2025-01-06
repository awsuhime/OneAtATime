using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class DamPopScript : MonoBehaviour
{
    public float speedBonus = 1.5f;
    public float lifeTime;
    private float startTime;
    private float offset;
    public TextMeshPro damageText;
    private float damagebonus;
    private void Start()
    {
        startTime = Time.time;
        offset = Random.Range(-0.3f, 0.3f);
    }

    void Update()
    {
        float timeLeft = Time.time - startTime;
        if (timeLeft > lifeTime)
        {
            Destroy(gameObject);
        }
        //Formula for a quadratic speed thingy
        float speed = Mathf.Pow(timeLeft, 2) * -1 + (lifeTime * timeLeft);
        transform.Translate(offset * Time.deltaTime, speedBonus * speed * Time.deltaTime, 0);
        transform.localScale = new Vector3(Mathf.Clamp(speed, 0.1f, 1.5f) + damagebonus, Mathf.Clamp(speed, 0.1f, 1.5f) + damagebonus, 1);
    }

    public void damageNumber(float damage)
    {
        damageText.text = damage.ToString();
        damagebonus = (Mathf.Log(damage + 1, 6f) / 2 ) + 1;
    }
}
