using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlameSword : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject UI;
    public GameObject rangeVisualizer;
    public GameObject projectile;

    [Header("Assignables")]
    public TurnManager turnManager;
    public TextMeshProUGUI attackText;
    public Camera cam;
    public Stats stats;

    //Assigned mid script
    private AttackLogic attackLogic;
    private UIManager uiManager;
    private FlameWave moveForward;

    [Header("Technical")]
    public bool active;
    public bool interupt = true;
    public int currentID = 0;

    //Mouse vars
    private Vector3 rotation;
    private Vector3 mousePos;
    private float rotz;

    public void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        uiManager = FindObjectOfType<UIManager>();
        attackLogic = GetComponent<AttackLogic>();
        stats = GetComponent<Stats>();

    }
    public void Update()
    {
        if (active)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            rotation = mousePos - transform.position;
            rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            rangeVisualizer.transform.rotation = Quaternion.Euler(0f, 0f, rotz - 90f);
            //Shooting
            if (Input.GetMouseButtonDown(0))
            {
                active = false;
                attackLogic.interruptible = false;

                GameObject proj = Instantiate(projectile, transform.position, Quaternion.Euler(0f, 0f, rotz));
                moveForward = proj.GetComponent<FlameWave>();
                moveForward.attack = this;
                moveForward.attackStat = stats.attack;
                uiManager.findToggles();
                //UI.SetActive(true);
                rangeVisualizer.SetActive(false);
                turnManager.addSubTurn();
                attackLogic.attackUse();

            }
            //Cancel Attack
            if (Input.GetKeyDown(KeyCode.Space))
            {
                cancelAttack();

            }

        }


    }
    public void Activate()
    {
        if (attackLogic.attacksLeft > 0)
        {
            UI.SetActive(false);
            active = true;
            rangeVisualizer.SetActive(true);
            attackLogic.interruptible = false;


        }
        else
        {
            Debug.Log("No attacks available");
        }
    }

    public void activateUI()
    {
        active = false;
        attackLogic.interruptible = true;
        UI.SetActive(true);
    }

    public void cancelAttack()
    {
        active = false;
        UI.SetActive(true);
        rangeVisualizer.SetActive(false);
        attackLogic.interruptible = true;

    }
}
