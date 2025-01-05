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
    public int maxJumpsAvailable = 3;
    public int jumpsAvailable;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Camera cam;
    public TextMeshProUGUI jumpText;

    private float vertVel;

    public void Start()
    {
        jumpsAvailable = maxJumpsAvailable;
    }
    public void Activate()
    {
        if (jumpsAvailable > 0)
        {
            UI.SetActive(false);
            active = true;
            jumpRangeIndicator.SetActive(true);
            hoverIndicator.SetActive(true);

        }
        else
        {
            Debug.Log("No jumps available");
        }
        
    }

    public void Update()
    {
        if (active)
        {
            float horiMin = transform.position.x - horiJumpRange_;
            float horiMax = transform.position.x + horiJumpRange_;
            float vertMin = transform.position.y - vertJumpRange_;
            float vertMax = transform.position.y + vertJumpRange_;



            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector3(Mathf.Clamp(mousePos.x, horiMin, horiMax), Mathf.Clamp(mousePos.y, vertMin, vertMax), 0);
            
            hoverIndicator.transform.position = mousePos;
            float g = Physics2D.gravity.y * rb.gravityScale;
            //vertVel = -g * Time.fixedDeltaTime / 2f + Mathf.Sqrt(-2f * g * vertJumpRange_);

            if (Input.GetMouseButtonDown(0))
            {
                jumpsAvailable--;
                transform.position = mousePos;
                rb.isKinematic = false;
                //rb.AddForce(new(0, vertVel), ForceMode2D.Impulse);
                active = false;
                jumpRangeIndicator.SetActive(false);
                hoverIndicator.SetActive(false);
                endJump();
            }
        }
    }

    Vector2 PointPos(float t, float hori, float vert)
    {
        Vector2 currentPointPos;
        currentPointPos = new Vector2(transform.position.x, transform.position.y + 1) + new Vector2(hori, vert) * t + 0.5f * Physics2D.gravity * (t * t);
        return currentPointPos;
    }

    private void endJump()
    {
        jumpText.text = "Moves: " + jumpsAvailable;
        UI.SetActive(true);
        rb.isKinematic = true;
        
    }
}
