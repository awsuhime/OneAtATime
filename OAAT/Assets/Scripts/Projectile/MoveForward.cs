using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using TMPro;

public class MoveForward : MonoBehaviour
{
    public float lifeTime;
    public float speed;
    public int phases = 2;
    public int startUpPhases = 1;
    public SpriteRenderer spriteRenderer;
    public Material def;
    public Material grey;
    private bool active;
    public GameObject range;
    public TurnManager turnManager;
    public int subTurnID;
    public GameObject self;
    public GameObject UI;
    private Attack attack;
    public bool assigned = false;
    public TextMeshProUGUI phaseText;
    public TextMeshProUGUI IDText;
    public int ID;


    void FixedUpdate()
    {
        if (active)
        {
            transform.Translate(Vector2.right * speed);

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
            //attack.activateUI();
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

        Invoke(nameof(endPhase), lifeTime);
            active = true;
            spriteRenderer.material = def;
        
        

    }

    public void Initiate(int textID)
    {
        turnManager = FindObjectOfType<TurnManager>();
        Invoke(nameof(endPhase), lifeTime);
        active = true;
        range.transform.Rotate(0, 0, -90);
        phaseText.text = "Phases: " + phases.ToString();
        ID = textID;
        textID++;
        IDText.text = textID.ToString();
    }

    public void updateID()
    {
        IDText.text = ID.ToString();
        ID--;
        

    }

}
