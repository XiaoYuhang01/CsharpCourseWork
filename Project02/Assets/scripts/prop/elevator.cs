using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour
{
    public Rigidbody2D rb;

    public Transform downBound;
    public Transform upBound;

    public float up, down;

    public float speed;

    private bool faceDown = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        down = downBound.position.y;
        up = upBound.position.y;
        Destroy(downBound.gameObject);
        Destroy(upBound.gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (faceDown)
        {
            rb.velocity = new Vector2(0, -speed);
            if(transform.position.y < down)
            {
                //transform.localScale = new Vector2(15, -10);
                faceDown = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(0, speed);
            if (transform.position.y > up)
            {
                //transform.localScale = new Vector2();
                faceDown = true;
            }
        }
    }
}
