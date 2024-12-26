using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLogic : MonoBehaviour
{
    public bool active = false;
    public bool ally = false;
    private SpriteRenderer spriteRenderer;
    private TurnManager manager;
    public Material def;
    public Material grey;

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
    }

    void Update()
    {
        
        if (ally && active)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                endTurn();
            }
        }
        
    }

    void endTurn()
    {
        active = false;
        spriteRenderer.material = grey;
        manager.endTurn();
    }
}
