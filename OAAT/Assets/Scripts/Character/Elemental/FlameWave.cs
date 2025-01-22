using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlameWave : MonoBehaviour
{
    [Header("Stats")]
    public float attackStat = 0;
    public float damageRatio = 0.5f;
    public float dotRatio = 0.1f;
    public float phaseBoost = 0.2f;
    public float knockback = 1;
    public bool explosive = false;
    public float lifeTime;
    public float speed;
    public int phases = 2;
    public int startUpPhases = 1;
    public float explosiveRange = 3;

    [Header("Assignable Scripts")]
    public GameObject range;
    public SpriteRenderer spriteRenderer;
    public Material def;
    public Material grey;
    public TurnManager turnManager;
    public FlameSword attack;
    private Health health;
    private SubTurnLogic STLogic;

    [Header("Game objects")]
    public GameObject particles;

    [Header("Technical")]
    private bool active;
    private int hits;
    private float startTime;


    void FixedUpdate()
    {
        if (active)
        {
            transform.Translate(Vector2.right * speed);
            float timeLeft = Time.time - startTime;
            if (timeLeft > lifeTime)
            {
                endPhase();
            }
        }
    }
    void endPhase()
    {
        if (startUpPhases == 0)
        {
            phases--;

            active = false;
            if (phases <= 0)
            {
                if (explosive)
                {
                    Explode();

                }
                else
                {
                    turnManager.removeSubTurn(STLogic.ID);

                    Destroy(gameObject);
                }


            }
            else
            {
                spriteRenderer.material = grey;
                turnManager.startSubTurn();

            }
        }
        else
        {
            attack.activateUI();
            //UI.SetActive(true);
            startUpPhases--;
            active = false;
            spriteRenderer.material = grey;

        }
        //range = Instantiate(rangeVisualizer, transform.position, transform.rotation);
        damageRatio += phaseBoost;
        range.SetActive(true);






    }

    public void startPhase()
    {
        range.SetActive(false);
        startTime = Time.time;
        //Invoke(nameof(endPhase), lifeTime);
        active = true;
        spriteRenderer.material = def;



    }

    public void Initiate()
    {
        turnManager = FindObjectOfType<TurnManager>();
        STLogic = GetComponent<SubTurnLogic>();
        startTime = Time.time;
        active = true;
        range.transform.Rotate(0, 0, -90);
        range.SetActive(false);
    }



    public void OnTriggerStay2D(Collider2D collision)
    {
        if (active)
        {

            if (collision.gameObject.CompareTag("Enemy"))
            {
                Health hit = collision.GetComponent<Health>();
                Stats stat = collision.GetComponent<Stats>();
                stat.dealDoT("FlameWave", Mathf.Round(dotRatio * attackStat) + 1, 3);
                hit.takeDamage(damageRatio * attackStat);
                hits++;
                turnManager.knockbacks++;
                hit.simpleKnockback(transform.eulerAngles, knockback);
                hit.Immunity();
            }



        }

    }

    public void Explode()
    {
        active = false;
        Health[] hit = FindObjectsOfType<Health>();
        foreach (Health i in hit)
        {
            if (Vector2.Distance(i.gameObject.transform.position, transform.position) < explosiveRange)
            {
                i.takeDamage(damageRatio * attackStat);
                hits++;
                if (knockback > 0)
                {
                    i.takeKnockback(transform.position, knockback);
                    turnManager.knockbacks++;
                }
            }
        }
        Instantiate(particles, transform.position, transform.rotation);
        turnManager.removeSubTurn(STLogic.ID);

        if (startUpPhases > 0 && turnManager.knockbacks == 0)
        {
            attack.activateUI();
        }
        Destroy(gameObject);
    }
}
