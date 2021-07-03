using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eagleEnemy : MonoBehaviour
{
    public float speed = 15;//�ƶ�
    public float changeDirectionTime = 2f; //�ı䷽���ʱ��

    public bool isVertical; //�Ƿ�ֱ�����ƶ�
    private float changeTimer;

    private Vector2 moveDirection;//�ƶ�����

    private Rigidbody2D rbody;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        moveDirection = isVertical ? Vector2.up : Vector2.right;//�����ֱ�ƶ� ����ͳ��� ��������
        changeTimer = changeDirectionTime;
    }

    // Update is called once per frame
    void Update()
    {
        changeTimer -= Time.deltaTime;
        if (changeTimer < 0)
        {
            moveDirection *= -1;
            changeTimer = changeDirectionTime;
            transform.localScale = new Vector2(- moveDirection.x * 7, 7);
        }
        Vector2 position = rbody.position;
        position.x += moveDirection.x * speed * Time.deltaTime;
        position.y += moveDirection.y * speed * Time.deltaTime;
        rbody.MovePosition(position);
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
            else if(pc.transform.position.x < this.transform.position.x)
            {
                pc.ChangeHealth(-1);
                pc.rb.velocity = new Vector2(-10, pc.rb.velocity.y);
                pc.isHurt = true;
            }
            else if (pc.transform.position.x > this.transform.position.x)
            {
                pc.ChangeHealth(-1);
                pc.rb.velocity = new Vector2(10, pc.rb.velocity.y);
                pc.isHurt = true;
            }
        }
    }

    public void dead()
    {
        Destroy(this.gameObject);
    }
}
