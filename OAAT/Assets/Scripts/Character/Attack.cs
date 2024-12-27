using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int maxAttacks = 2;
    public int attacksLeft;
    public GameObject UI;
    private bool active;
    public GameObject rangeVisualizer;
    public Camera cam;
    public GameObject projectile;
    public TurnManager turnManager;
    private MoveForward moveForward;
    //Mouse vars
    private Vector3 rotation;
    private Vector3 mousePos;
    private float rotz;
    public void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();

        attacksLeft = maxAttacks;
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
                projectile.SetActive(true);
                projectile.transform.position = transform.position;
                projectile.transform.rotation = Quaternion.Euler(0f, 0f, rotz);
                moveForward = projectile.GetComponent<MoveForward>();
                moveForward.Initiate();
                //GameObject proj = Instantiate(projectile, transform.position, Quaternion.Euler(0f, 0f, rotz));
                active = false;
                UI.SetActive(false);
                rangeVisualizer.SetActive(false);
                //turnManager.addSubTurn(projectile);
                attacksLeft--;
            }

        }

        
    }
    public void Activate()
    {
        if (attacksLeft > 0)
        {
            UI.SetActive(false);
            active = true;
            rangeVisualizer.SetActive(true);
            

        }
        else
        {
            Debug.Log("No attacks available");
        }
    }

    
}
