using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLogic : MonoBehaviour
{
    public bool active = false;
    public bool ally = false;
    private SpriteRenderer spriteRenderer;
    private TurnManager manager;
    [SerializeField] private Jump jump;
    public Material def;
    public Material grey;
    public GameObject UI;

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
        }
    }

    

    public void endTurn()
    {
        if (ally)
        {
            UI.SetActive(false);
        }
        active = false;
        spriteRenderer.material = grey;
        manager.endTurn();
    }

    public void Jump()
    {
        UI.SetActive(false);
        jump.Activate();
    }

    
}
