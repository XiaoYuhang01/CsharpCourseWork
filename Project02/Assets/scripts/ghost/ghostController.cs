using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ghostController : MonoBehaviour
{
    public Text Health;
    public GameObject frog;
    public GameObject frog1;
    public GameObject door;

    public float speed = 3f;
    public float changeDirectionTime = 5f;
    private float changeDirectionTimer;

    public float skillCircleReleaseTime = 10f;
    private float skillCircleReleaseTimer;
    public float skillFanshapedReleaseTime = 5f;
    private float skillFanshapedReleaseTimer;

    public float skillFireSpeed = 2;

    private Vector2 moveDirection;
    public int maxHealth = 5;
    public int currentHealth;
    private Animator anim;

    private bool isDefeated;
    private Rigidbody2D rbody;

    public GameObject skillBallPrefab; //技能预制体

    void Start()
    {
        //获取 boss的刚体
        rbody = GetComponent<Rigidbody2D>();
        //获取 boss的动画器
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        
        moveDirection = Vector2.left;
        changeDirectionTimer = changeDirectionTime;
        skillCircleReleaseTimer = skillCircleReleaseTime;
        isDefeated = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (isDefeated) return;//如果被击败了，就不再执行
        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer < 0)
        {
            moveDirection *= -1;
            changeDirectionTimer = changeDirectionTime;
            if (moveDirection == Vector2.left)
                anim.SetTrigger("changeLeft");
            if (moveDirection == Vector2.right)
                anim.SetTrigger("changeRight");
        }
        Vector2 position = rbody.position;    //每帧都改变位置
        position.x += moveDirection.x * speed * Time.deltaTime;
        position.y += moveDirection.y * speed * Time.deltaTime;
        rbody.MovePosition(position);

        //anim.SetFloat("moveX", moveDirection.x);

        //=================Boss技能：每10s释放一次圆形弹幕///每5s释放一次扇形弹幕=============

        skillCircleReleaseTimer -= Time.deltaTime;
       // if (skillCircleReleaseTimer < 2)
           // anim.SetTrigger("disappear");

        if (skillCircleReleaseTimer < 0)
        {
            SkillCircleRelease();
            Debug.Log("释放一次圆形弹幕");
        }

        skillFanshapedReleaseTimer -= Time.deltaTime;
        if (skillFanshapedReleaseTimer < 0)
        {
            SkillFanshapedRelease(moveDirection);
            Debug.Log("释放一次扇形弹幕");
        }

    }
    //=====================扇形弹幕攻击=======================
    public void SkillFanshapedRelease(Vector2 moveDirection)
    {
        skillFanshapedReleaseTimer = skillFanshapedReleaseTime;
        
            Vector2 startAngle = moveDirection;

            GameObject skillBall1 = (GameObject)Instantiate(skillBallPrefab, rbody.position, Quaternion.identity);
            if(skillBall1!=null)
            Debug.Log("生成");

            skillBallController sbc1 = skillBall1.GetComponent<skillBallController>();
            GameObject skillBall2 = (GameObject)Instantiate(skillBallPrefab, rbody.position, Quaternion.identity);
            skillBallController sbc2 = skillBall2.GetComponent<skillBallController>();
            GameObject skillBall3 = (GameObject)Instantiate(skillBallPrefab, rbody.position, Quaternion.identity);
            skillBallController sbc3 = skillBall3.GetComponent<skillBallController>();

        if (sbc1 != null && sbc2 != null && sbc3 != null)
        {
            sbc1.Move(startAngle, skillFireSpeed);
            Vector2 fireAngle2 = RotateVector(startAngle, 20);
            Vector2 fireAngle3 = RotateVector(startAngle, 340);
            sbc2.Move(fireAngle2, skillFireSpeed);
            sbc3.Move(fireAngle3, skillFireSpeed);
        }
    }

    //======================圆形弹幕攻击=======================
    public void SkillCircleRelease()
    {
        skillCircleReleaseTimer = skillCircleReleaseTime;

        Vector2 startAngle = new Vector2(0,1);
        Vector2 fireAngle = startAngle;
        //Quaternion changeAngle = Quaternion.Euler(0,0,20);//18个子弹，相邻两个相隔20度


            for (int i = 0; i < 12; i++)
            {
                GameObject skillBall = Instantiate(skillBallPrefab, rbody.position, Quaternion.identity);
                skillBallController sbc = skillBall.GetComponent<skillBallController>();
                //Vector2 fireAngle2D = fireAngle;

                if (sbc != null)
                {
                    sbc.Move(fireAngle, skillFireSpeed);
                    //fireAngle = fireAngle * Quaternion.Euler(0, 0, 20);
                    fireAngle = RotateVector(fireAngle, 30);
                }
            }
    }
    //=====================================================
    public Vector2 RotateVector(Vector2 oriVec, float angle)
    {
        float x = oriVec.x;
        float y = oriVec.y;
        float newX = x * Mathf.Cos(angle*Mathf.Deg2Rad) - y * Mathf.Sin(angle*Mathf.Deg2Rad);
        float newY = x * Mathf.Sin(angle*Mathf.Deg2Rad) + y * Mathf.Cos(angle*Mathf.Deg2Rad);
        Vector2 newVector = new Vector2(newX,newY);
        return newVector;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        acorn acn = collision.gameObject.GetComponent<acorn>();
        if (acn != null)
        { ChangeHealth(-1);
            //Destroy(acn.gameObject);
        }
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log("掉血");
        if (currentHealth == 2)
        {
            frog.SetActive(true);
            frog.SetActive(true);
        }
        Health.text = currentHealth.ToString();
        if (currentHealth == 0) Defeated();

    }
    public void Defeated()
    {
        isDefeated = true;
        anim.SetTrigger("disappear");
        door.SetActive(true);
        //Destroy(this.gameObject);
    }
}
