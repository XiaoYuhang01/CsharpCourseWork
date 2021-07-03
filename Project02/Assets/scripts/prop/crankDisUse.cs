using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crankDisUse : MonoBehaviour
{
    public GameObject crank_up;
    public GameObject crank_down;
    public GameObject trap;

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            crank_up.SetActive(false);
            crank_down.SetActive(true);
            trap.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.F))
        {
            crank_up.SetActive(true);
            crank_down.SetActive(false);
            trap.SetActive(true);
        }
    }
}
