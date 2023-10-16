using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float speed;
    public float HP;
    public float MaxHP;
    public int Damage;
    public RuntimeAnimatorController[] animatorCon;     //몬스터 종류에 따라 애니메이션 바꾸기
    public Rigidbody2D target;      //물리적인 타겟

    private bool isLive;           //생존여부
    private Rigidbody2D rigid;
    private Collider2D coll;

    private Animator anim;
    private SpriteRenderer spriteR;

    public AudioClip Hit;
    private AudioSource audio;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        audio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()      //물리적
    {
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return;
        }
        //anim.SetBool("IsDead", false);
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
        coll.enabled = true;       //콜라이더 활성화
        spriteR.sortingOrder = 2;
        rigid.simulated = true;    //리지드바디 활성화
        anim.SetBool("IsDead", false);
        HP = MaxHP;         //부활시 풀체력
    }

    public void Init(SpawnData data)      //데이터 받기
    {
        anim.runtimeAnimatorController = animatorCon[data.spriteType];
        speed = data.speed;
        MaxHP = data.HP;
        HP = data.HP;
        Damage = data.damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Weapon") || !isLive)
        {
            return;
        }


        HP -= collision.GetComponent<SkillManager>().damage;
        StartCoroutine(KnockBack());
        audio.clip = Hit;
        audio.Play();

        if (HP > 0)
        {
            anim.SetTrigger("Hit");
        }
        else        //몹이 죽을 시
        {
            isLive = false;
            coll.enabled = false;       //콜라이더 비활성화
            spriteR.sortingOrder = 1;
            rigid.simulated = false;    //리지드바디 비활성화
            anim.SetBool("IsDead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    private IEnumerator KnockBack()
    {
        WaitForFixedUpdate wfu = new WaitForFixedUpdate();
        yield return wfu;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }
}
