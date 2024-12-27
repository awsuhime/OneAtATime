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
    public TextMeshProUGUI jumpText;

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
            Invoke(nameof(endTurn), 2f);
        }
        else
        {
            UI.SetActive(true);
            jumpText.text = "Jumps: " + jump.jumpsAvailable;
        }
    }

    

    public void endTurn()
    {
        if (ally)
        {
            UI.SetActive(false);
            if (jump.jumpsAvailable < jump.maxJumpsAvailable)
            {
                jump.jumpsAvailable++;
            }
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
