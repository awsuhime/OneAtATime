using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class TurnManager : MonoBehaviour
{
    public TurnLogic[] logic;
    public static TurnLogic[] order;
    public static int turn = 0;
    public GameObject _camera;
    private int subTurn = 0;
    //List<MoveForward> subTurns;
    public MoveForward[] subTurns;
    private int subCount = 0;
    public GameObject UI;

    public static GameObject activePlayer;

    public int knockbacks = 0;
    void Start()
    {

        BeginCombat();

    }
    void BeginCombat()
    {
        logic = FindObjectsOfType<TurnLogic>();
        //Sort allies and enemies
        var firstOrder = new List<TurnLogic>();

        for(int i = 0; i < logic.Length; i++)
        {
            if (logic[i].ally)
            {
                Debug.Log(logic[i].name + " is an ally");
                firstOrder.Add(logic[i]);
                
            }
        }
        for(int i = 0; i < logic.Length; i++)
        {
            if (!logic[i].ally)
            {
                firstOrder.Add(logic[i]);
            }
        }
        order = firstOrder.ToArray();
        turn = 0;
        activePlayer = order[turn].gameObject;
        order[turn].startTurn();
        
    }

    public void endTurn()
    {
        //Reset combos
        Health[] health = FindObjectsOfType<Health>();
        foreach (Health i in health)
        {
            i.resetCombo();
        }

        turn++;
        Debug.Log("Turn " + turn);
        if (turn >= order.Length)
        {
            
            turn = 0;
            Debug.Log("Turns reset, turn " + turn);
        }
        subTurn = 0;
        activePlayer = order[turn].gameObject;
        order[turn].startTurn();
    }

    public void startSubTurn()
    {
        if (subTurn < subCount)
        {
            var newSub = new List<MoveForward>();
            newSub = subTurns.ToList();
            Debug.Log("SubTurn: " + subTurn);
            for (int i = 0; i < newSub.Count; i++)
            {
                if (subTurns[i].ID == subTurn)
                {
                    Debug.Log(subTurns[i].ID + " matched, starting phase. GameObject name: " + subTurns[i].gameObject.name);
                    subTurns[i].startPhase();
                }
            }
            subTurn++;


        }
        else
        {
            subTurn = 0;
            endTurn();
        }
        
    }

    public void removeSubTurn(int removeID)
    {
        subCount--;
        var newSubs = new List<MoveForward>();
        MoveForward culprit = subTurns[0];
        //Find Dead ID
        newSubs = subTurns.ToList();
        foreach (MoveForward i in newSubs)
        {
            if (i.ID == removeID)
            {
                culprit = i;
            }
        }

        //Remove Dead ID
        newSubs.Remove(culprit);
        subTurns = newSubs.ToArray();

        //Update IDs
        var changeSubs = new List<MoveForward>();
        foreach (MoveForward i in subTurns)
        {
            if (i.ID > removeID)
            {
                changeSubs.Add(i);
            }
        }
        for (int i = 0; i < changeSubs.Count; i++)
        {
            changeSubs[i].updateID();
        }

        changeSubs = subTurns.ToList();
        if (subTurn > 0)
        {
            if (knockbacks == 0)
            {
                subTurn--;
                Invoke(nameof(startSubTurn), 0.25f);
            }
            
            
        }
        
    }

    public void addSubTurn()
    {
        MoveForward[] moveForward = FindObjectsOfType<MoveForward>();
        subTurns = moveForward;
        foreach (MoveForward i in moveForward)
        {
            if (!i.assigned)
            {
                Debug.Log("Added " + i.gameObject.name);

                i.assigned = true;
                i.Initiate(subCount);
                //subTurns.Add(i);
                //subTurns[subCount] = i;
                subCount++;
            }
        }
    }
    public void callJump()
    {
        order[turn].Jump();
    }

    public void callEndTurn()
    {
        order[turn].endTurn();
    }

    public void callAttack()
    {
        order[turn].Attack();
    }

    public void removeKnockback()
    {
        knockbacks--;
        if (knockbacks == 0)
        {
            if (subTurn > 0)
            {
                subTurn--;
                Invoke(nameof(startSubTurn), 0.25f);
            }
            else
            {
                activePlayer.GetComponent<Attack>().activateUI();
            }
        }
    }

    
}
