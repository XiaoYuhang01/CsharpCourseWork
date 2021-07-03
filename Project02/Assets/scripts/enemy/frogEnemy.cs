using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frogEnemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;

    public Transform leftBound;
    public Transform rightBound;

    public float left, right;

    public float speed;

    private bool faceLeft;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        left = leftBound.position.x;
        right = rightBound.position.x;
        Destroy(leftBound.gameObject);
        Destroy(rightBound.gameObject);
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (faceLeft)
        {
            rb.velocity = new Vector2(-speed, 0);
            if(transform.position.x < left)
            {
                transform.localScale = new Vector2(-7, 7);
                faceLeft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(speed, 0);
            if (transform.position.x > right)
            {
                transform.localScale = new Vector2(7, 7);
                faceLeft = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Character1 pc = other.gameObject.GetComponent<Character1>();
        if (pc != null)
        {
            if (pc.anim.GetBool("fall"))
            {
                anim.SetTrigger("dead");
                pc.rb.velocity = new Vector2(pc.rb.velocity.x, pc.jumpforce);
                pc.anim.SetBool("jump", true);
            }
            else if (pc.transform.position.x < this.transform.position.x)
            {
                pc.ChangeHealth(-1);
                pc.rb.velocity = new Vector2(-5, 7);
                pc.isHurt = true;
            }
            else if (pc.transform.position.x > this.transform.position.x)
            {
                pc.ChangeHealth(-1);
                pc.rb.velocity = new Vector2(5, 7);
                pc.isHurt = true;
            }
        }
    }

    public void dead()
    {
        Destroy(this.gameObject);
    }
}
