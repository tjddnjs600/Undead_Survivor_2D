using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //플레이어 이동관련 변수
    public Vector2 inputVec;
    public float speed;

    private Rigidbody2D rigid;
    private SpriteRenderer spriter;
    private Animator anim;

    private void Awake()
    {
        this.rigid = GetComponent<Rigidbody2D>();
        this.spriter = GetComponent<SpriteRenderer>();
        this.anim = GetComponent<Animator>();
    }
    
    //플레이어 움직임 컨트롤 함수
    void FixedUpdate()
    {
        //움직임 변수 설정
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        //물리움직임 사용
        rigid.MovePosition(rigid.position + nextVec);
    }
    
    //update가 끝나고 후처리가 필요할때 lateupdate함수에서 처리한다.
    private void LateUpdate()
    {
        //움직이는 애니메이션 설정         vector의 길이(크기 (x,y축 상관없이))값을 가져온다.
        anim.SetFloat("Speed", inputVec.magnitude);
        
        //죄우 움직이는 키를 눌렸을때
        if (inputVec.x != 0)
        {
            //누르는 좌 위 방향에 따라 플레이어 방향도 바꿔주기
            spriter.flipX = inputVec.x < 0;
        }
    }

    //움직임을 간편하게 할 inputsystem 라이브러리 사용시
    //움직임값을 받아올 내장함수
    void OnMove(InputValue value)
    {
        //타입이 맞지 않기 때문에 Vector2로 타입을 맞춰 가져온디.
        inputVec = value.Get<Vector2>();
    }
}
