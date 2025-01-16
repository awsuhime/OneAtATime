using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttackManager : MonoBehaviour
{
    public GameObject[] attackButtons;
    private TextMeshProUGUI spellText;
    private AttackLogic logic;
    public int count;
    private Jump move;
    private TurnManager manager;
    public void Start()
    {
        manager = GetComponent<TurnManager>();
    }
    public void configureSpells()
    {
        logic = TurnManager.activePlayer.GetComponent<AttackLogic>();
        move = TurnManager.activePlayer.GetComponent<Jump>();
        count = logic.attackCount;
        for (int i = 0; i < attackButtons.Length; i++)
        {
            if (i < count)
            {
                attackButtons[i].SetActive(true);
                spellText = attackButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                spellText.text = logic.spellNames[i];
            }
            else
            {
                attackButtons[i].SetActive(false);
            }
        }

    }

    private void Update()
    {
        if (TurnManager.order[TurnManager.turn].ally)
        {
            if (move.interupt && logic.interruptible)
            {
                if (Input.GetKeyDown(KeyCode.R) && count >= 1)
                {
                    logic.useR();
                }
                else if (Input.GetKeyDown(KeyCode.F) && count >= 2)
                {
                    logic.useF();
                }
                else if (Input.GetKeyDown(KeyCode.V) && count >= 3)
                {
                    logic.useV();
                }
                else if (Input.GetKeyDown(KeyCode.T) && count >= 4)
                {
                    logic.useT();
                }
                else if (Input.GetKeyDown(KeyCode.G) && count >= 5)
                {
                    logic.useG();
                }
                else if (Input.GetKeyDown(KeyCode.B) && count >= 6)
                {
                    logic.useB();
                }
                else if (Input.GetKeyDown(KeyCode.Y) && count >= 7)
                {
                    logic.useY();
                }
                else if (Input.GetKeyDown(KeyCode.H) && count >= 8)
                {
                    logic.useH();
                }
                else if (Input.GetKeyDown(KeyCode.N) && count >= 9)
                {
                    logic.useN();
                }
            }

        }
    }

    public void R()
    {
        logic.useR();
    }

    public void F()
    {
        logic.useF();
    }

    public void V()
    {
        logic.useV();
    }

    public void T()
    {
        logic.useT();
    }

    public void G()
    {
        logic.useG();
    }

    public void B()
    {
        logic.useB();
    }

    public void Y()
    {
        logic.useY();
    }
    public void H()
    {
        logic.useH();
    }
    public void N()
    {
        logic.useN();
    }
}
