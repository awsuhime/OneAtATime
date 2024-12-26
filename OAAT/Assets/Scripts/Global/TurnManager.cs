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
        _camera.transform.position = new Vector3(order[turn].gameObject.transform.position.x, order[turn].gameObject.transform.position.y, _camera.transform.position.z);
    }
}
