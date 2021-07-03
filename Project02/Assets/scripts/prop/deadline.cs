using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadline : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character1 pc = collision.gameObject.GetComponent<Character1>();
        if(pc != null)
        {
            pc.dead();
        }
    }
}
