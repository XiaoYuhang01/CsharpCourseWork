using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cherry : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        Character1 cExample = other.GetComponent<Character1>();
        if (cExample != null)
        {
            if (cExample.MyCurrentHealth < cExample.MyMaxHealth)
            {
                cExample.ChangeHealth(1);
                Destroy(this.gameObject);
            }
        }
    }
}
