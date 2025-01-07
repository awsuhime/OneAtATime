using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public bool trajectory;
    public GameObject[] traj;
    private TurnManager manager;
    private Jump jump;
    private Attack attack;

    public GameObject actionUI;
    public GameObject attackUI;

    private TurnLogic logic;
    private void Start()
    {
        manager = FindObjectOfType<TurnManager>();
    }
    private void Update()
    {
        logic = TurnManager.order[TurnManager.turn];
        if (logic.ally)
        {
            jump = logic.GetComponent<Jump>();
            attack = logic.GetComponent<Attack>();
            if (jump.interupt && attack.interupt)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    disableActions();
                    manager.callJump();
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    disableActions();
                    actionUI.SetActive(false);
                    attackUI.SetActive(true);
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    disableActions();
                    actionUI.SetActive(true);
                    attackUI.SetActive(false);
                    manager.callEndTurn();
                    
                }
            }
            
        }
        
    }

    private void disableActions()
    {
        if (attack.active)
        {
            attack.cancelAttack();
        }
        else if (jump.active)
        {
            jump.cancelMove();
        }
    }
    public void findToggles()
    {
        var trajFind = new List<GameObject>();
        if (trajectory)
        {
            traj = GameObject.FindGameObjectsWithTag("Trajectory");
        }
        else
        {
            trajFind = GameObject.FindGameObjectsWithTag("Trajectory").ToList();
            foreach(GameObject i in trajFind)
            {
                i.SetActive(false);
            }
            trajFind.AddRange(traj.ToList());
            traj = trajFind.ToArray();
        }

    }

    
    public void toggleTrj(bool toggle)
    {
        var removeTraj = new List<GameObject>();
        foreach(GameObject i in traj)
        {
            if (i == null)
            {
                removeTraj.Add(i);
            }
        }
        traj = traj.ToList().Except(removeTraj).ToArray();
        

        trajectory = toggle;
        Debug.Log("Toggle: " + toggle);
        foreach (GameObject i in traj)
        {
            i.SetActive(toggle);
        }
    }
}
