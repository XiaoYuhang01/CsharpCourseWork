using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acorn : MonoBehaviour
{

    bool onClicked = false;
    public Transform Center;//中心点
    public float MaxDistance = 2f;//拉动最大距离
    public float ReleaseTime = 0.1f;

    //[HideInInspector]
    public SpringJoint2D sj;
    Rigidbody2D rg;

    public LineRenderer lineFront;
    public LineRenderer lineBack;
    public Transform bindFront;
    public Transform bindBack;


    // Start is called before the first frame update
    void Start()
    {
        sj = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        //sj.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (onClicked)
        {
            Debug.Log("clicked");
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            //transform.position = new Vector3(0, 0, -Camera.main.transform.position.z);

            LineRender();

            if (Vector3.Distance(transform.position, Center.position) > MaxDistance)
            {
                Vector3 Direction = (transform.position - Center.position).normalized * MaxDistance;//只能改变方向
                transform.position = Direction + Center.position;

                LineRender();

            }
        }

    }
    private void LineRender()//皮带画线
    {
        lineFront.SetPosition(0, bindFront.position);
        lineFront.SetPosition(1, transform.position);

        lineBack.SetPosition(0, bindBack.position);
        lineBack.SetPosition(1, transform.position);
    }

    public void OnMouseDown()
    {
        onClicked = true;
        //rg.isKinematic = false ;
        rg.bodyType = RigidbodyType2D.Dynamic;
    }

    public void OnMouseUp()
    {
        onClicked = false;
        //rg.isKinematic = true;
        StartCoroutine(ReleaseBird());
        //Invoke("Fly", 0.1f);
    }

    /*void Fly()
    {
        sj.enabled = false;
        Invoke("Next", 5);
    }*/

    IEnumerator ReleaseBird()
    {
        yield return new WaitForSeconds(ReleaseTime);
        sj.enabled = false;
        lineFront.enabled = false;
        lineBack.enabled = false;

        Invoke("Next", 3);//3s后从界面消失
    }

    void Next()
    {
        acornManager._instance.acorns.Remove(this);//从List中移除
        Destroy(gameObject);

        acornManager._instance.NextAcorn();
    }
   /* private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("击中敌人");
        ghostController ghstCtrl = other.gameObject.GetComponent<ghostController>();
        if (ghstCtrl != null)
        {
            Destroy(this.gameObject);
            Debug.Log("击中了敌人");
            ghstCtrl.ChangeHealth(-1);
        }
    }*/
}
