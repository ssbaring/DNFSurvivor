using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;           //캐릭터 속도
    [SerializeField] private GameObject Player;         //캐릭터 오브젝트

    private Animator anim;
    private SpriteRenderer sprite;
    public Vector2 InputVector;
    private Rigidbody2D rigid;
    public Scanner scan;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        scan = GetComponent<Scanner>();
    }

    private void Update()
    {
        InputVector.x = Input.GetAxisRaw("Horizontal");        //수평이동  
        InputVector.y = Input.GetAxisRaw("Vertical");          //수직이동
    }

    private void FixedUpdate()
    {
        Vector2 nextVector = InputVector.normalized * MoveSpeed * Time.fixedDeltaTime;          //물리프레임 하나가 소모한 시간만큼
        rigid.MovePosition(rigid.position + nextVector);        //위치 이동
    }

    private void LateUpdate()
    {
        TurnCharacter();
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }

    private void TurnCharacter()    //캐릭터 좌우 돌기
    {
        if(InputVector.x < 0)
        {
            sprite.flipX = true;
        }
        else if(InputVector.x > 0)
        {
            sprite.flipX = false;
        }
    }
}
