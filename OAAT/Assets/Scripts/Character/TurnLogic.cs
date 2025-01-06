using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnLogic : MonoBehaviour
{
    public bool active = false;
    public bool ally = false;
    private SpriteRenderer spriteRenderer;
    private TurnManager manager;
    [SerializeField] private Jump jump;
    [SerializeField] private Attack attack;
    public Material def;
    public Material grey;
    public GameObject UI;
    public TextMeshProUGUI attackText;

    public float turnTime = 0.5f;
    void Start()
    {
    }
    public void startTurn()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

        }
        if (manager == null)
        {
            manager = FindObjectOfType<TurnManager>();
        }
        spriteRenderer.material = def;
        active = true;
        if (!ally)
        {
            Invoke(nameof(endTurn), turnTime);
        }
        else
        {
            jump.origin = transform.position;
            UI.SetActive(true);
            attackText.text = "SP: " + attack.maxAttacks;
        }
    }

    

    public void endTurn()
    {
        if (ally)
        {
            UI.SetActive(false);
            
            attack.attacksLeft = attack.maxAttacks;
        }
        
        active = false;
        spriteRenderer.material = grey;
        manager.startSubTurn();
    }

    public void Jump()
    {
        jump.Activate();
    }
    public void Attack()
    {
        attack.Activate();
    }
    
}
