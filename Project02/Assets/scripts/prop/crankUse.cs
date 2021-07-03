using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crankUse : MonoBehaviour
{
    public GameObject crank_up;
    public GameObject crank_down;
    public GameObject block;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            crank_up.SetActive(false);
            crank_down.SetActive(true);
            block.SetActive(true);
        }
    }
}
