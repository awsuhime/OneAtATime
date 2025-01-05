using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public bool trajectory;
    public GameObject[] traj;
    public void findToggles()
    {
        var trajFind = new List<GameObject>();
        if (trajectory)
        {
            traj = GameObject.FindGameObjectsWithTag("Trajectory");
        }
        else
        {
            trajFind = GameObject.FindGameObjectsWithTag("Trajectory").ToList();
            foreach(GameObject i in trajFind)
            {
                i.SetActive(false);
            }
            trajFind.AddRange(traj.ToList());
            traj = trajFind.ToArray();
        }

    }

    
    public void toggleTrj(bool toggle)
    {
        var removeTraj = new List<GameObject>();
        foreach(GameObject i in traj)
        {
            if (i == null)
            {
                removeTraj.Add(i);
            }
        }
        traj = traj.ToList().Except(removeTraj).ToArray();
        

        trajectory = toggle;
        Debug.Log("Toggle: " + toggle);
        foreach (GameObject i in traj)
        {
            i.SetActive(toggle);
        }
    }
}
