using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float lifeTime;
    public float speed;
    public int phases = 2;
    public int startUpPhases = 1;
    public SpriteRenderer spriteRenderer;
    public Material def;
    public Material grey;
    private bool active;
    public GameObject rangeVisualizer;
    private GameObject range;
    public TurnManager turnManager;
    public int subTurnID;
    public GameObject self;
    public GameObject UI;

    


    void FixedUpdate()
    {
        if (active)
        {
            transform.Translate(Vector2.right * speed);

        }
    }
    void endPhase()
    {
        if (startUpPhases == 0)
        {
            turnManager.startSubTurn();

        }
        else
        {
            startUpPhases--;
        }
        Destroy(range);
        range = Instantiate(rangeVisualizer, transform.position, transform.rotation);
        range.transform.Rotate(0, 0, -90);
        phases--;
        active = false;
        if (phases < 0)
        {
            Destroy(range);
            Destroy(gameObject);
            gameObject.SetActive(false);

        }
        else
        {
            spriteRenderer.material = grey;
        }
        
        
        

    }

    public void startPhase()
    {
        
            Invoke(nameof(endPhase), lifeTime);
            active = true;
            Destroy(range);
            spriteRenderer.material = def;
        
        

    }

    public void Initiate()
    {
        turnManager = FindObjectOfType<TurnManager>();
        Invoke(nameof(endPhase), lifeTime);
        active = true;
    }

}
