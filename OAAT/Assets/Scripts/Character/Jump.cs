using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Jump : MonoBehaviour
{
    public GameObject UI;
    public bool active = false;
    public float jumpTime = 1f;
    public float moveRange;
    public float horiJumpRange_;
    public float vertJumpRange_;
    public GameObject jumpRangeIndicator;
    public GameObject hoverIndicator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Camera cam;
    public Vector3 origin;
    private float vertVel;
    public bool interupt = true;
    private Stats stats;

    public void Start()
    {
        stats = GetComponent<Stats>();
    }
    public void Activate()
    {
        UI.SetActive(false);
        active = true;
        jumpRangeIndicator.SetActive(true);
        hoverIndicator.SetActive(true);
        jumpRangeIndicator.transform.position = origin;




    }

    public void Update()
    {
        if (active)
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            
                //Find Angle between origin and mouse
                float adj = mousePos.x - origin.x;
                float opp = mousePos.y - origin.y;
                float tangAngle = Mathf.Atan(Mathf.Abs(opp) / Mathf.Abs(adj));
                float distance = Mathf.Abs(opp) / Mathf.Sin(tangAngle);
            if (distance > moveRange)
            {
                //Find sine and cosine for the unit circle
                float siny = moveRange * Mathf.Sin(tangAngle);
                float cosx = moveRange * Mathf.Cos(tangAngle);

                mousePos = new Vector3(origin.x + cosx * Mathf.Sign(adj), origin.y + siny * Mathf.Sign(opp), 0);
            }
                


            


            hoverIndicator.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
           

            if (Input.GetMouseButtonDown(0))
            {
                interupt = false;
                transform.position = new Vector3(mousePos.x, mousePos.y, 0);
                active = false;
                jumpRangeIndicator.SetActive(false);
                hoverIndicator.SetActive(false);
                hoverIndicator.transform.position = transform.position;
                endJump();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                cancelMove();
            }
        }
    }


    public void cancelMove()
    {
        active = false;
        jumpRangeIndicator.SetActive(false);
        hoverIndicator.SetActive(false);
        UI.SetActive(true);
    }
    

    private void endJump()
    {
        interupt = true;
        UI.SetActive(true);
        
    }
}
