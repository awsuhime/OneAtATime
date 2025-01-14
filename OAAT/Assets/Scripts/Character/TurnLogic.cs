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
    [SerializeField] private Stats stats;
    public Material def;
    public Material grey;
    public GameObject UI;
    public TextMeshProUGUI attackText;
    private AttackLogic attackLogic;
    private AttackManager attackManager;

    public float turnTime = 0.5f;
    void Start()
    {
        
        
    }
    public void startTurn()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        attackLogic = GetComponent<AttackLogic>();
        manager = FindObjectOfType<TurnManager>();
        attackManager = FindObjectOfType<AttackManager>();

        spriteRenderer.material = def;
        active = true;
        if (!ally)
        {
            Invoke(nameof(endTurn), turnTime);
        }
        else
        {
            attackManager.configureSpells();
            jump.origin = transform.position;
            UI.SetActive(true);
            attackText.text = "SP: " + attack.maxAttacks;
            stats.countdown();

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
