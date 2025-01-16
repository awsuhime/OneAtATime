using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class AttackLogic : MonoBehaviour
{
    [Header("Column 1")]
    [SerializeField] private UnityEvent R;
    [SerializeField] private UnityEvent F;
    [SerializeField] private UnityEvent V;
    [Header("Column 2")]
    [SerializeField] private UnityEvent T;
    [SerializeField] private UnityEvent G;
    [SerializeField] private UnityEvent B;
    [Header("Column 3")]
    [SerializeField] private UnityEvent Y;
    [SerializeField] private UnityEvent H;
    [SerializeField] private UnityEvent N;

    [Header("Spell Attributes")]
    public string[] spellNames;

    public int attackCount = 1;
    public bool interruptible = true;

    public int attacksLeft;
    public int maxAttacks;

    public TextMeshProUGUI spText;

    public bool active;


    public void attackUse()
    {
        attacksLeft--;
        spText.text = "SP: " + attacksLeft;
    }
    public void useR()
    {
        R.Invoke();
    }

    public void useF()
    {
        F.Invoke();
    }

    public void useV()
    {
        V.Invoke();
    }

    public void useT()
    {
        T.Invoke();
    }
    public void useG()
    {
        G.Invoke();
    }
    public void useB()
    {
        B.Invoke();
    }
    public void useY()
    {
        Y.Invoke();
    }
    public void useH()
    {
        H.Invoke();
    }
    public void useN()
    {
        N.Invoke();
    }
}
