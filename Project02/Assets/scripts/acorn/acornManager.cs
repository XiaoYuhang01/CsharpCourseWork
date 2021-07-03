using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acornManager : MonoBehaviour
{
    public List<acorn> acorns;
    public List<eagleEnemy> enemy;
    public static acornManager _instance;
    private Vector3 originPos;

    void Start()
    {
        Debug.Log("Initialized");
        Initialized();//第一个弹簧组件可用，其他sj.enabled=false
    }

    private void Awake()
    {
        _instance = this;
        //acorns = new List<acorn>();
        if (acorns.Count > 0)
        {
            originPos = acorns[0].transform.position;
        }

    }

    private void Initialized()
    {
        for(int i = 0; i < acorns.Count; i++)
        {
            if (i==0)
            {
                Debug.Log("true " + acorns[i]);
                acorns[i].transform.position = originPos;
                acorns[i].sj.enabled = true;
                acorns[i].enabled = true;
            }
            else
            {
                Debug.Log("false " + acorns[i]);
                acorns[i].sj.enabled = false;
                acorns[i].enabled = false;
            }
        }
    }

    private void deg()
    {
        for (int i = 0; i < acorns.Count; i++)
        {
            Debug.Log(i + " " + acorns[i]);
        }
    }

    public void NextAcorn()
    {
        if (acorns.Count > 0)
        {
            deg();
            Initialized();
        }
    }
}
