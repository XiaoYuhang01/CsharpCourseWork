using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillBallController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rbody;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 10f);
    }
    void Start()
    {
      
    }
    public void Move(Vector2 moveDirection, float moveForce)
    {
        rbody.velocity = moveDirection * moveForce;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("123");
        Character1 playerController = other.gameObject.GetComponent<Character1>();
        if (playerController != null)
        {
            Destroy(this.gameObject);
            Debug.Log("击中了玩家");
            //============玩家掉血======
            playerController.rb.velocity = new Vector2(10, 0);
            playerController.ChangeHealth(-1);
        }
        
    }
}
