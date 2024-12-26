using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private bool active = false;

    public float horiJumpRange_;
    public float vertJumpRange_;
    public GameObject jumpRangeIndicator;
    public GameObject hoverIndicator;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Camera cam;
    public void Activate()
    {
        active = true;
        jumpRangeIndicator.SetActive(true);
        hoverIndicator.SetActive(true);
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
        }
    }
}
