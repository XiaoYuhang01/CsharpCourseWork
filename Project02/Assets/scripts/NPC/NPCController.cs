using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dialogImage;

    public float showTime = 4;
    public float showTimer;
    public bool isActive = false;

    void Start()
    {
        dialogImage.SetActive(false);
        showTimer = -1;
    }

    // Update is called once per frame
    void Update()
    {
        showTimer -= Time.deltaTime;
        if (showTimer < 0)
        {
            dialogImage.SetActive(false);
            isActive = false;
        }
    }

    public void ShowDialog()
    {
        showTimer = showTime;
        dialogImage.SetActive(true);
        isActive = true;
    }
    public void CloseDialog()
    {
        if (isActive == true)
        {
            showTimer = -1;
            dialogImage.SetActive(false);
        }
        else return;
    }
}
