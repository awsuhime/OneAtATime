using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using TMPro;

public class MoveForward : MonoBehaviour
{
    public float damage = 5;
    public float knockback = 1;
    public float lifeTime;
    public float speed;
    public int phases = 2;
    public int startUpPhases = 1;
    public float explosiveRange = 3;
    public SpriteRenderer spriteRenderer;
    public Material def;
    public Material grey;
    private bool active;
    public GameObject range;
    public TurnManager turnManager;
    public int subTurnID;
    public GameObject self;
    public GameObject UI;
    public Attack attack;
    public bool assigned = false;
    public TextMeshProUGUI phaseText;
    public TextMeshProUGUI IDText;
    public int ID;

    private float startTime;
    private Health health;
    private int hits;
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
            phaseText.text = "Phases: " + phases.ToString();

            active = false;
            if (phases <= 0)
            {
                turnManager.removeSubTurn(ID);

                Destroy(gameObject);

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

    public void Initiate(int textID)
    {
        turnManager = FindObjectOfType<TurnManager>();
        //Invoke(nameof(endPhase), lifeTime);
        startTime = Time.time;
        active = true;
        range.transform.Rotate(0, 0, -90);
        phaseText.text = "Phases: " + phases.ToString();
        ID = textID;
        textID++;
        IDText.text = textID.ToString();
        range.SetActive(false);
    }

    public void updateID()
    {
        IDText.text = ID.ToString();
        ID--;
        

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (active)
        {
            
            if (collision.gameObject.CompareTag("Terrain") || collision.gameObject.CompareTag("Enemy"))
            {
                active = false;
                Health[] hit = FindObjectsOfType<Health>();
                foreach(Health i in hit)
                {
                    if (Vector2.Distance(i.gameObject.transform.position, transform.position) < explosiveRange)
                    {
                        i.takeDamage(damage);
                        hits++;
                    }
                }
                turnManager.removeSubTurn(ID);

                if (startUpPhases > 0)
                {
                    attack.activateUI();
                }
                Destroy(gameObject);
            }
            
                
            
        }
        
    }

}
