using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float maxSpeed = 3f;
    private float jumpSpeed = 15f;
    //private bool isJump = false;
    Rigidbody2D rigid;
    SpriteRenderer render;
    Animator animator;
   
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (Input.GetButtonUp("Horizontal")) //손 떼면 속도 줄임
        {
            // 벡터값이 방향뿐아니라 크기도 가지고 있기 때문에 정규화 필요(방향구할때쓰는거)
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
    }
    
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); //좌우로만 움직일거기 때문에 horizontal키값으로 받음
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
        if(Input.GetButtonDown("Jump") && animator.GetBool("isJumping") == false)
        {
            rigid.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
        }

        //Animator 
        if(Mathf.Abs(rigid.velocity.x) < 0.3) //횡이동 속도 0.3보다 작으면 멈춘 상태로 간주, Idle애니메이션으로 전환
        {
            animator.SetBool("isWalking", false);
        }
        else //움직이고 있는 상태
        {
            animator.SetBool("isWalking", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Land")
        {
            animator.SetBool("isJumping", false);
        }
    }
}
