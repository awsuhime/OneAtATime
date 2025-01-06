using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Jump : MonoBehaviour
{
    public GameObject UI;
    private bool active = false;
    public float jumpTime = 1f;
    public float horiJumpRange_;
    public float vertJumpRange_;
    public GameObject jumpRangeIndicator;
    public GameObject hoverIndicator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Camera cam;
    public Vector3 origin;
    private float vertVel;

    public void Start()
    {
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
            float horiMin = origin.x - horiJumpRange_;
            float horiMax = origin.x + horiJumpRange_;
            float vertMin = origin.y - vertJumpRange_;
            float vertMax = origin.y + vertJumpRange_;



            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector3(Mathf.Clamp(mousePos.x, horiMin, horiMax), Mathf.Clamp(mousePos.y, vertMin, vertMax), 0);
            
            hoverIndicator.transform.position = mousePos;
            float g = Physics2D.gravity.y * rb.gravityScale;
            //vertVel = -g * Time.fixedDeltaTime / 2f + Mathf.Sqrt(-2f * g * vertJumpRange_);

            if (Input.GetMouseButtonDown(0))
            {
                transform.position = mousePos;
                //rb.AddForce(new(0, vertVel), ForceMode2D.Impulse);
                active = false;
                jumpRangeIndicator.SetActive(false);
                hoverIndicator.SetActive(false);
                hoverIndicator.transform.position = transform.position;
                endJump();
            }
        }
    }

    

    private void endJump()
    {
        UI.SetActive(true);
        
    }
}
