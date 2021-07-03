using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character1 : MonoBehaviour
{
    //=============组件==================
    public Rigidbody2D rb;
    public Animator anim;
    public LayerMask ground;
    public Collider2D coll;
    public static AudioSource bgm;
    public GameObject deadDialog;
    public Text gem;
    public Text Health;
    //=============参数==================

    //======运动参数======
    private float horizontalmove;
    public float speed;
    public float jumpforce;
    private bool isGround, isJump;
    private bool jumpPressed;
    public int jumpCount;
    private bool isClimb;
    public bool isHurt;

    //======方向======
    private Vector2 direction;
    public Vector2 lookDirection { get => transform.localScale; }//玩家朝向

    //======生命参数======
    public int maxHealth = 10;
    private int currentHealth;
    public int MyMaxHealth { get { return maxHealth; } }
    public int MyCurrentHealth { get { return currentHealth; } }

    //======道具=======
    private static int gemNum;
    public int GetgemNum { get => gemNum; }

    void Start()
    {
        //isGround = Physics2D.OverlapCircle();
        currentHealth = maxHealth;
        gemNum = 0;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        bgm = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isHurt)
        {
            Movement();
        }
        SwitchAnim();
        NPC();
    }

    //与梯子的碰撞检测
    void OnTriggerEnter2D(Collider2D other)
    {
        ladder LExample = other.GetComponent<ladder>();
        if (LExample != null)
        {
            isClimb = true;
        }
        else
            isClimb = false;
    }

    //移动
    void Movement()
    {
        horizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");
        direction = new Vector2(facedirection, 0);
        if (horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * speed, rb.velocity.y);
            anim.SetFloat("run", Mathf.Abs(horizontalmove));
        }
        if (facedirection != 0)
        {
            transform.localScale = new Vector2(facedirection * 7, 7);
        }
        if (Input.GetButton("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            anim.SetBool("jump", true);
        }
    }

    //动画控制
    public void SwitchAnim()
    {
        anim.SetBool("idle", false);
        if (anim.GetBool("jump"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jump", false);
                anim.SetBool("fall", true);
            }
        }
        else if (isHurt)
        {
            anim.SetBool("hurt", true);
            if (Mathf.Abs(rb.velocity.y) < 1f)
            {
                anim.SetBool("hurt", false);
                anim.SetBool("idle", true);
                isHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("fall", false);
            anim.SetBool("idle", true);
        }
    }

    //道具相关方法
    public void ChangeGemNum(int gemget)
    {
        gemNum += gemget;
        gem.text = gemNum.ToString();
    }

    //玩家生命值改变
    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Health.text = MyCurrentHealth + "/" + MyMaxHealth;
        if(currentHealth == 0)
        {
            dead();
        }
    }

    //与NPC交互
    public void NPC()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, 2f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                Debug.Log("hit NPC");
                NPCController npc = hit.collider.GetComponent<NPCController>();
                if (npc != null)
                {
                    if (!npc.isActive) npc.ShowDialog();
                    else npc.CloseDialog();
                }
            }
        }
    }

    //重新开始
    public void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //死亡
    public void dead()
    {
        deadDialog.SetActive(true);
        bgm.enabled = false;
        rb.velocity = new Vector2(0, 0);
        rb.gravityScale = 0;
        Invoke("reload", 1f);
    }
}


