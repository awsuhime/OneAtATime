using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SubTurnLogic : MonoBehaviour
{
    [SerializeField] private UnityEvent startPhaseChild;
    [SerializeField] private UnityEvent initiateChild;
    private TurnManager turnManager;
    public TextMeshProUGUI IDText;
    private bool active;
    public int ID;
    public bool assigned = false;

    public void Initiate(int textID)
    {
        turnManager = FindObjectOfType<TurnManager>();
        ID = textID;
        textID++;
        IDText.text = textID.ToString();
        initiateChild.Invoke();
    }

    public void updateID()
    {
        IDText.text = ID.ToString();
        ID--;


    }

    public void startPhase()
    {
        startPhaseChild.Invoke();
    }
}
