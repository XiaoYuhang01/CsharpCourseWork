using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crank : MonoBehaviour
{
    public GameObject crankdialog;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character1 pc = collision.GetComponent<Character1>();
        if(pc != null)
        {
            crankdialog.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        crankdialog.SetActive(false);
    }
}
