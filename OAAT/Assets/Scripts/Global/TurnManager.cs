using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TurnManager : MonoBehaviour
{
    private TurnLogic[] logic;
    private TurnLogic[] order;
    private int turn = 0;
    public GameObject _camera;
    private int subTurn = 0;
    private GameObject[] subTurns;
    private int subCount = 0;
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
        order[turn].startTurn();
        
    }

    public void endTurn()
    {
        
        turn++;
        Debug.Log("Turn " + turn);
        if (turn >= order.Length)
        {
            
            turn = 0;
            Debug.Log("Turns reset, turn " + turn);
        }
        
        order[turn].startTurn();
    }

    public void startSubTurn()
    {
        /*if (subTurn < subCount)
        {
            MoveForward moveForward = subTurns[subTurn].GetComponent<MoveForward>();
            moveForward.startPhase();
            subTurn++;
        }
        else
        {
            subTurn = 0;
            endTurn();
        }*/
        if (subTurn >= 1)
        {
            subTurn = 0;
            endTurn();
        }
        else
        {
            subTurn++;

            MoveForward[] mvF = FindObjectsOfType<MoveForward>();
            for (int i = 0; i < mvF.Length; i++)
            {
                mvF[i].startPhase();
            }
        }
    }

    public void addSubTurn(GameObject ID)
    {
        Debug.Log("Added " + ID.name);
        subTurns[subCount] = ID;
        subCount++;
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
}
