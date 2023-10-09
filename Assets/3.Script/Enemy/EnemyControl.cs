using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float speed;
    public float HP;
    public float MaxHP;
    public RuntimeAnimatorController[] animatorCon;     //몬스터 종류에 따라 애니메이션 바꾸기
    public Rigidbody2D target;      //물리적인 타겟

    private bool isLive;           //생존여부
    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer spriteR;
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()      //물리적
    {
        if(!isLive)
        {
            return;
        }

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;      //몬스터가 실시간 움직이는방향
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;                  //물리속도가 이동에 영향 안가게
    }

    private void LateUpdate()
    {
        if (!isLive)
        {
            return;
        }
        spriteR.flipX = target.position.x < rigid.position.x;
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();       //타겟을 초기화
        isLive = true;
        HP = MaxHP;         //부활시 풀체력
    }

    public void Init(SpawnData data)      //데이터 받기
    {
        anim.runtimeAnimatorController = animatorCon[data.spriteType];
        speed = data.speed;
        MaxHP = data.HP;
        HP = data.HP;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Weapon"))
        {
            return;
        }

        HP -= collision.GetComponent<AsuraSkill>().damage;

        if(HP>0)
        {

        }
        else
        {
            Dead();
        }
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }
}
