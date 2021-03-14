using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float maxSpeed = 3f;
    private float jumpSpeed = 15f;
    private bool isJump = false;
    Vector2 pos = new Vector2(0f, 1f);
    Rigidbody2D rigid;
    SpriteRenderer render;
   
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
 
    }
    
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal"); //좌우로만 움직일거기 때문에 horizontal키값으로 받음
        rigid.AddForce(Vector2.right * horizontal, ForceMode2D.Impulse);
        if(rigid.velocity.x > maxSpeed) // 오른쪽 움직이는경우
        {
            render.flipX = false;
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y); //최고속도 넘으면 자동으로 최고속도로 조절해줌
        }
        else if (rigid.velocity.x < maxSpeed *(-1)) //왼쪽움직이는 경우(음수처리 해줘야함)
        {
            render.flipX = true;
            rigid.velocity = new Vector2(maxSpeed *(-1), rigid.velocity.y);
        }
        //rigid.velocity.y를 해두는 이유는 얘를 0으로 주면 후에 점프하다 멈춤..
        if(Input.GetButtonDown("Jump") && isJump == false)
        {
            rigid.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            isJump = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Land")
        {
            isJump = false;
        }
    }
}
