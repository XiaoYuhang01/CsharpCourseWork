using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sign : MonoBehaviour
{
    public GameObject enterDialog;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Character1 cExample = other.GetComponent<Character1>();
        if(cExample != null )
        {
            enterDialog.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Character1 cExample = collision.GetComponent<Character1>();
        enterDialog.SetActive(false);
    }
}
